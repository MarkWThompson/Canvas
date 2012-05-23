using System;
using System.Collections.Generic;	 
using System.Linq;	
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Ebay.Engine.Objects;

namespace Ebay.Objects

{
    class Goku : Sprite
    {
        //************************
        // PROPERTIES
        //************************
        private int yVelocity;
        private int xVelocity;
        private const int MAX_X_VELOCITY = 15;
        private const int MAX_Y_VELOCITY = 15;
        private const int ACCELERATION = 1;
        int counterto40 = 0;    //For hovering
        bool goingdown = true; //again, for hovering
        KeyboardState keyboardState;


        public Goku()
        {
            Spriterect.X = 15;
            Spriterect.Y = 75;
            Spriterect.Width = 55;
            Spriterect.Height = 75;
        }

        public void Initialise(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/Goku");
            InitialiseSprite(texture);
            position.X = 0;
            position.Y = 530;
        }

        public void Update()
        {
            HandleInput();
            UpdatePosition();
            MidairHover();
        }

        private void HandleInput()
        {
            // Standard WASD movement.
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (yVelocity > -MAX_Y_VELOCITY)
                yVelocity -= ACCELERATION;
                Spriterect.X = 315;
                Spriterect.Y = 245;
                Spriterect.Width = 55;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (yVelocity < MAX_Y_VELOCITY)
                yVelocity += ACCELERATION;
                Spriterect.X = 315;
                Spriterect.Y = 245;
                Spriterect.Width = 55;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (xVelocity > -MAX_X_VELOCITY)
                xVelocity -= ACCELERATION;
                Spriterect.X = 180;
                Spriterect.Y = 245;
                Spriterect.Width = 55;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (xVelocity < MAX_X_VELOCITY)
                xVelocity += ACCELERATION;
                Spriterect.X = 240;
                Spriterect.Y = 245;
                Spriterect.Width = 75;
            }

            if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))

                // Decays xVelocity if A/D are not being pressed.
                if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                {
                    if ((position.Y > (Program.SCREEN_HEIGHT - Spriterect.Height) - 1))  //Check if goku is on ground for standing sprite activation
                    {
                        Spriterect.X = 15;
                        Spriterect.Y = 75;
                        Spriterect.Width = 55;
                        Spriterect.Height = 75;
                    }

                    if (xVelocity != 0)
                    {
                        if (xVelocity > 0)
                            xVelocity--;
                        else
                            xVelocity++;
                    }
                }

            // Decays yVelocity if W/S are not being pressed.
            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down))
            {
                if (yVelocity != 0)
                {
                    if (yVelocity > 0)
                        yVelocity--;
                    else
                        yVelocity++;
                }
            }


            if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right)) //check that left and right are not being pressed, important for that frame when changing from left to right or vice verca when speed = 0
            {
                if ((yVelocity == 0) && (xVelocity == 0))
                { //only trigger if still
                    if ((position.Y < (Program.SCREEN_HEIGHT - Spriterect.Height) - 1))//Check if goku is in the air for static flying animation
                    {
                        Spriterect.X = 315;
                        Spriterect.Y = 245;
                        Spriterect.Width = 55;
                    }
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
            else if (position.X > Program.SCREEN_WIDTH - Spriterect.Width)
                position.X = Program.SCREEN_WIDTH - Spriterect.Width;

            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > Program.SCREEN_HEIGHT - Spriterect.Height)
                position.Y = Program.SCREEN_HEIGHT - Spriterect.Height;

        }

        private void MidairHover()
        {
            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down)){
                if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right)){
                    if ((position.Y < (Program.SCREEN_HEIGHT - Spriterect.Height) - 1))//Check if goku is in the air for static flying animation
                    {
                       
                        //So yeah, i may have gone overboard with the nested ifs, really need to work on my code structuring :)

                        if ((goingdown == true) && (counterto40 < 20)) //if sprite is in downwards hover and not nearing the apex
                        {
                            position.Y = position.Y + 0.5f;
                        }
                        else if ((goingdown == true) && (counterto40 > 20 && counterto40 < 30)) //if sprite is in downwards hover and is nearing the apex
                        {
                            position.Y = position.Y + 0.25f;
                        }
                        else if ((goingdown == true) && (counterto40 >= 30)) //if sprite is in downwards hover and is pretty much at the apex
                        {
                            position.Y = position.Y + 0.1f;
                        }
                        else if ((goingdown == false) && (counterto40 < 20)) //if sprite is in upwards hover and not nearing the apex
                        {
                            position.Y = position.Y - 0.5f;
                        }
                        else if ((goingdown == false) && (counterto40 > 20 && counterto40 < 30)) //if sprite is in upwards hover and is nearing the apex
                        {
                            position.Y = position.Y - 0.25f;
                        }
                        else if ((goingdown == true) && (counterto40 >= 30)) //if sprite is in upwards hover and is pretty much at the apex
                        {
                            position.Y = position.Y - 0.1f;
                        }


                            counterto40++;

                        if(counterto40 > 40){ //flips the hover direction every 40 loops, as well as resetting the timer
                            counterto40 = 0;
                            goingdown = !goingdown;
                        }
                    }
                }
            }
        }


    }
}