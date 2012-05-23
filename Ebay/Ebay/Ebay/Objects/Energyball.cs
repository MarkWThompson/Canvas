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
    class Energyball : Sprite
    {
        int leftorright = 0;

          public Energyball()
        {
            Spriterect.X = 495;
            Spriterect.Y = 690;
            Spriterect.Width = 70;
            Spriterect.Height = 70;
            
        }

        public void Initialise(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/Goku");
            InitialiseSprite(texture);
            position.X = 0;
            position.Y = 800;
        }

        public void Update()
        {
            if (leftorright == 1)
            {
                position.X = position.X + 14;
            }
            else if (leftorright == 0){
                position.X = position.X - 14;
            }
        }

        public void setvalues(float x, float y, int goingleft)
        {
            if (goingleft == 0)
            {
                position.X = x - 50;
                position.Y = y;
                leftorright = 0;
            }
            else if (goingleft == 1)
            {
                position.X = x + 50;
                position.Y = y;
                leftorright = 1;
            }
        }
    }
}
