/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Extends the Character class adding further player interactivity.
//
*/

using System;
using System.Collections.Generic;	 
using System.Linq;	
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ebay.Engine.Objects;
using Ebay;

namespace Ebay.Objects
{
    class Goku : Character
    {
        //************************
        // PROPERTIES
        //************************
        bool onGround = false;
        int[] hover = {1, 40};

        //************************
        // CONSTRUCTOR
        //************************
        public Goku()
        {
            // Sets maximum velocities.
            maxXVelocity = 12;
            maxYVelocity = 12;
        }

        //************************
        // INITIALISER
        //************************
        public void Initialise(ContentManager content)
        {
            // Sets Goku spritesheet.
            texture = content.Load<Texture2D>("Sprites/Goku");
            InitialiseSprite(texture);
            // Alters sprite dimensions.
            ChangeSpriteRect(15, 75, 55, 70);
            // Sets initial position.
            position.X = 0;
            position.Y = Program.SCREEN_HEIGHT - height; // Bottom of screen relative to Goku.
        }

        //************************
        // MAIN LOOP
        //************************
        // Overrides Character class method Update.
        new public void Update() 
        {
            HandleInput();
            HandleMovement();
            Hover();
            UpdatePosition();

            // Determines if Goku is on the ground.
            if (position.Y == Program.SCREEN_HEIGHT - height)
                onGround = true;
            else
                onGround = false;
        }

        //************************
        // PRIVATE METHODS
        //************************
        // If you want to detect user input, do so here.
        private void HandleInput()
        {
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                // Kamehameha!
            }
        }

        // When no keys are being pressed Goku will appear to hover.
        private void Hover()
        {
            if ((MovementKeysUp()) && (!onGround))
            {
                if (yVelocity == 0)
                {
                    // Sets yVelocity as vector (magnitude and direction).
                    // If you want to increase the velocity just do hover[0]*2 or something.
                    yVelocity = hover[0];
                }

                if (hover[1] < 20)
                    hover[1]++;
                else
                {
                    hover[1] = 1; // Reset count.
                    hover[0] *= -1; // Change direction.
                }
            }
            else
            {
                // Can reset hover values here if you want depending on the motion you prefer.
                hover[1] = 1;
                hover[0] = 1;
            }
        }
    }
}
