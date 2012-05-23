/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Main player class.
//          Responds to user input.
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
using Ebay.Engine.Objects;

namespace Ebay.Objects
{
    class Player : Sprite
    {
        //************************
        // PROPERTIES
        //************************
        private int yVelocity;
        private int xVelocity;
        private const int MAX_X_VELOCITY = 5;
        private const int MAX_Y_VELOCITY = 5;
        private const int ACCELERATION = 1;
        KeyboardState keyboardState;

        //************************
        // INITIALISER
        //************************
        public void Initialise(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/SquareGuy");
            InitialiseSprite(texture);
            Spriterect.Width = 91; //why the hell is this square 91x91??
            Spriterect.Height = 91;
        }

        //************************
        // MAIN LOOP
        //************************
        public void Update()
        {
            HandleInput();
            UpdatePosition();
        }

        //************************
        // PRIVATE METHODS
        //************************
        private void HandleInput()
        {
            // Standard WASD movement.
            if (keyboardState.IsKeyDown(Keys.W))
            {
                if (yVelocity > -MAX_Y_VELOCITY)
                    yVelocity-=ACCELERATION;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                if (yVelocity < MAX_Y_VELOCITY)
                    yVelocity++;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                if(xVelocity > -MAX_X_VELOCITY)
                    xVelocity--;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                if(xVelocity < MAX_X_VELOCITY)
                    xVelocity++;
            }

            // Decays xVelocity if A/D are not being pressed.
            if (keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.D))
            {
                if (xVelocity != 0)
                {
                    if (xVelocity > 0)
                        xVelocity--;
                    else
                        xVelocity++;
                }
            }

            // Decays yVelocity if W/S are not being pressed.
            if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.S))
            {
                if (yVelocity != 0)
                {
                    if (yVelocity > 0)
                        yVelocity--;
                    else
                        yVelocity++;
                }
            }

            // Captures the state of the keyboard.
            keyboardState = Keyboard.GetState();
        }

        /* Prevents player from going past the screen bounds.
         * Repositions player to the bound edge if necessary.
         */
        private void UpdatePosition()
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
    }
}
