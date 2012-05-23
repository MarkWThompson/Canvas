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
using Ebay;

namespace Ebay.Objects

{
    class Goku : Sprite
    {
        //************************
        // PROPERTIES
        //************************
        private int yVelocity;
        private int xVelocity;
        private const int MAX_X_VELOCITY = 12;
        private const int MAX_Y_VELOCITY = 12;
        private int ACCELERATION = 1;
        int counterto40 = 0;    //For hovering
        int counterto15 = 0;    //for kamehameha frame updates
        bool goingdown = true; //again, for hovering
        bool kamehameha = false;
        int[] kamehamehaxposright = new int[4];
        int[] kamehamehaxposleft = new int[4];
        int framecounter = 0;
        public int leftorright = 1;  //Left is 0, Right is 1
        KeyboardState keyboardState;
        public bool energyballactive = false;
        bool hasreset = true;

        public Goku()
        {
            Spriterect.X = 15;
            Spriterect.Y = 75;
            Spriterect.Width = 55;
            Spriterect.Height = 75;
            kamehamehaxposright[0] = 25;
            kamehamehaxposright[1] = 92;
            kamehamehaxposright[2] = 161;
            kamehamehaxposright[3] = 222;
            kamehamehaxposleft[0] = 1340;
            kamehamehaxposleft[1] = 1273;
            kamehamehaxposleft[2] = 1204;
            kamehamehaxposleft[3] = 1086;
            
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
            Kamehameha();
        }

        private void HandleInput()
        {
            // Standard WASD movement.
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (yVelocity > -MAX_Y_VELOCITY)
                {
                    yVelocity -= ACCELERATION;
                    if (leftorright == 1)
                    {
                        Spriterect.X = 315;
                        Spriterect.Y = 245;
                        Spriterect.Width = 55;
                        Spriterect.Height = 75;
                    }
                    else if (leftorright == 0)
                    {
                        Spriterect.X = 1060;
                        Spriterect.Y = 245;
                        Spriterect.Width = 55;
                        Spriterect.Height = 75;
                    }
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (yVelocity < MAX_Y_VELOCITY)
                {
                    yVelocity += ACCELERATION;
                    if (leftorright == 1)
                    {
                        Spriterect.X = 315;
                        Spriterect.Y = 245;
                        Spriterect.Width = 55;
                        Spriterect.Height = 75;
                    }
                    else if (leftorright == 0)
                    {
                        Spriterect.X = 1060;
                        Spriterect.Y = 245;
                        Spriterect.Width = 55;
                        Spriterect.Height = 75;
                    }
                }
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                leftorright = 0;
                if (xVelocity > -MAX_X_VELOCITY)
                xVelocity -= ACCELERATION;
                Spriterect.X = 1110;
                Spriterect.Y = 245;
                Spriterect.Width = 75;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                leftorright = 1;
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
                    if ((position.Y > (Program.SCREEN_HEIGHT - Spriterect.Height) - 2))  //Check if goku is on ground for standing sprite activation
                    {
                        if (leftorright == 1)
                        {
                            Spriterect.X = 15;
                            Spriterect.Y = 75;
                            Spriterect.Width = 55;
                            Spriterect.Height = 75;
                        }
                        else if (leftorright == 0)
                        {
                            Spriterect.X = 1350;
                            Spriterect.Y = 75;
                            Spriterect.Width = 55;
                            Spriterect.Height = 75;
                        }
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
                        if (leftorright == 1)
                        {
                            Spriterect.X = 315;
                            Spriterect.Y = 245;
                            Spriterect.Width = 55;
                            Spriterect.Height = 75;
                        }
                        else if (leftorright == 0)
                        {
                            Spriterect.X = 1060;
                            Spriterect.Y = 245;
                            Spriterect.Width = 55;
                            Spriterect.Height = 75;
                        }
                    }
                        
                }
            }

            if (keyboardState.IsKeyDown(Keys.Space)) //For kamehameha attack
            {
                kamehameha = true;
                Spriterect.Width = 55;
                energyballactive = false;
                hasreset = false;
                
            }

            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                if (hasreset == false)
                    if (leftorright == 0)
                    {
                        {
                            position.X = position.X + 50;
                            hasreset = true;
                        }
                    }
                framecounter = 0;
                kamehameha = false;
                counterto15 = 0;
                energyballactive = false;
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

            ACCELERATION = 1;

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
                        else if ((goingdown == false) && (counterto40 >= 30)) //if sprite is in upwards hover and is pretty much at the apex
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


        public void Kamehameha(){
            if(kamehameha == true){
                if (leftorright == 1)
                {
                    counterto15++;

                    if (counterto15 > 15)
                    {
                        counterto15 = 0;
                    }


                    if (counterto15 == 15)
                    {
                        framecounter++;
                    }
                    if (framecounter <= 3) // Makes sure kamehamehaxposright dosent overflow
                    {
                        Spriterect.X = kamehamehaxposright[framecounter];
                        Spriterect.Width = 55;

                    }
                    else if (framecounter > 3)
                    {
                        Spriterect.X = kamehamehaxposright[3];
                        Spriterect.Width = 113;
                        energyballactive = false;

                    }
                    if (framecounter > 2)
                    {
                        Spriterect.Width = 113;
                        energyballactive = false;
                        xVelocity = 0;
                        yVelocity = 0;
                        ACCELERATION = 0;
                    }
                    if ((framecounter == 3) && (counterto15 == 0)) // the instant he fires
                    {
                        energyballactive = true;
                    }
                    Spriterect.Y = 510;
                    Spriterect.Height = 75;
                }
                
                else if (leftorright == 0)  
                {
                    counterto15++;

                    if (counterto15 > 15)
                    {
                        counterto15 = 0;
                    }


                    if (counterto15 == 15)
                    {
                        framecounter++;
                    }
                    if (framecounter <= 3) // Makes sure kamehamehaxposright dosent overflow
                    {
                        Spriterect.X = kamehamehaxposleft[framecounter];
                        Spriterect.Width = 55;

                    }
                    else if (framecounter > 3)
                    {
                        Spriterect.X = kamehamehaxposleft[3];
                        Spriterect.Width = 113;
                        energyballactive = false;


                    }
                    if (framecounter > 2)
                    {
                        Spriterect.Width = 113;
                        energyballactive = false;
                        xVelocity = 0;
                        yVelocity = 0;
                        ACCELERATION = 0;

                    }
                    if ((framecounter == 3) && (counterto15 == 15)) // the instant he fires
                    {
                        position.X = position.X - 50;
                        energyballactive = true;
                    }
                    Spriterect.Y = 510;
                    Spriterect.Height = 75;
                }
            }
        }
    }
}