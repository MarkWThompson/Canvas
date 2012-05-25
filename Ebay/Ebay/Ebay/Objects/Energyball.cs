/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Base class for all game sprites.
//
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Ebay.Engine.Objects;
using Ebay;

namespace Ebay.Objects
{
    class Energyball : Sprite
    {
        //************************
        // PROPERTIES
        //************************
        private bool facingRight = true;
        private int velocity = 14;

        //************************
        // INITIALISER
        //************************
        public void Initialise(ContentManager content)
        {
            // Sets Goku spritesheet.
            texture = content.Load<Texture2D>("Sprites/Goku");
            InitialiseSprite(texture);
            // Alters sprite dimensions.
            ChangeSpriteRect(495, 690, 70, 70);
            visible = false;
        }

        //************************
        // MAIN LOOP
        //************************
        public void Update()
        {
            if (visible)
            {
                if (facingRight)
                    position.X += velocity;
                else
                    position.X -= velocity;
            }
        }
    }
}
