/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Character base class.
//          Extend this class if creating any form of interactive object (e.g. PCC/NPC).
//
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ebay.Engine.Objects
{
    class Character : Sprite
    {
        //************************
        // PROPERTIES
        //************************
        protected int yVelocity = 1;
        protected int xVelocity = 1;
        protected int maxXVelocity = 1;
        protected int maxYVelocity = 1;
        protected int acceleration = 1;
        protected int velocityDecay = 1;
        protected bool keyboardActive = true;
        protected Keys[] movementKeys = { Keys.W, Keys.A, Keys.S, Keys.D };
        protected KeyboardState keyboardState;

        //************************
        // MAIN LOOP
        //************************
        public void Update()
        {
            HandleMovement();
            UpdatePosition();
        }

        //************************
        // PRIVATE METHODS
        //************************
        protected void HandleMovement()
        {
            if (keyboardActive)
            {
                // Standard WASD movement.
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (yVelocity > -maxYVelocity)
                        yVelocity -= acceleration;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (yVelocity < maxYVelocity)
                        yVelocity += acceleration;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    effects = SpriteEffects.FlipHorizontally;
                    if (xVelocity > -maxXVelocity)
                        xVelocity -= acceleration;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    effects = SpriteEffects.None;
                    if (xVelocity < maxXVelocity)
                        xVelocity += acceleration;
                }

                // Decays xVelocity if A/D are not being pressed.
                if (keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.D))
                {
                    if (xVelocity != 0)
                    {
                        if (xVelocity > 0)
                            xVelocity -= velocityDecay;
                        else
                            xVelocity += velocityDecay;
                    }
                }

                // Decays yVelocity if W/S are not being pressed.
                if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.S))
                {
                    if (yVelocity != 0)
                    {
                        if (yVelocity > 0)
                            yVelocity -= velocityDecay;
                        else
                            yVelocity += velocityDecay;
                    }
                }

            }

            // Captures the state of the keyboard.
            keyboardState = Keyboard.GetState();
        }

        /* Prevents player from going past the screen bounds.
         * Repositions player to the bound edge if necessary.
         */
        protected void UpdatePosition()
        {
            position.X += xVelocity;
            position.Y += yVelocity;

            if (position.X < 0)
                position.X = 0;
            else if (position.X > Program.SCREEN_WIDTH - width)
                position.X = Program.SCREEN_WIDTH - width;
                
            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > Program.SCREEN_HEIGHT - height)
                position.Y = Program.SCREEN_HEIGHT - height;   
        }

        // Determines if any of the movement keys (WASD) are down.
        protected bool MovementKeysUp()
        {
            bool keyUp = true;

            for (int i = 0; i < movementKeys.Length; i++)
            {
                if (keyboardState.IsKeyDown(movementKeys[i]))
                    keyUp = false;
            }

            return keyUp;
        }
    }
}
