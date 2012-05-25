/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Base class for all sprites.
//
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ebay.Engine.Objects
{
    public class Sprite : Object
    {
        //************************
        // PROPERTIES
        //************************
        public Texture2D texture;
        protected Rectangle sourceRect = new Rectangle();
        protected Color colour = Color.White;
        protected Vector2 origin = Vector2.Zero;
        protected SpriteEffects effects = SpriteEffects.None;
        protected float layerDepth;

        //************************
        // INITIALISER
        //************************
        protected void InitialiseSprite(Texture2D texture, float x = 0, float y = 0, float layerDepth = 0)
        {
            this.texture = texture;
            this.sourceRect = new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            this.width = this.sourceRect.Width;
            this.height = this.sourceRect.Height;
            this.position.X = x;
            this.position.Y = y;
            this.layerDepth = layerDepth;
        }

        //************************
        // PUBLIC METHODS
        //************************
        // Draws the sprite to a specified SpriteBatch.
        public void Draw(SpriteBatch spriteBatch)
        {
            // Does not draw if no texture loaded or invisible.
            if ((texture != null) && (visible))
                spriteBatch.Draw(texture, position, sourceRect, colour, rotation, origin, scale, effects, layerDepth);
        }

        // Updates sprite dimensions based on the sourceRect - for use with spritesheets.
        public void ChangeSpriteRect(int x, int y, int width, int height)
        {
            sourceRect.X = x;
            sourceRect.Y = y;
            sourceRect.Width = width;
            sourceRect.Height = height;
            this.width = width;
            this.height = height;
        }
    }
}
