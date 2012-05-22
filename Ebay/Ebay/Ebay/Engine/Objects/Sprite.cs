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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ebay.Engine.Objects
{
    public class Sprite
    {
        //************************
        // PROPERTIES
        //************************
        public int width;
        public int height;
        protected Texture2D texture;
        protected Vector2 position = Vector2.Zero;
        protected Rectangle? sourceRect = null; // What does this mean!?
        protected Color colour = Color.White;
        protected float rotation = 0f;
        protected Vector2 origin = Vector2.Zero;
        protected float scale;
        protected SpriteEffects effects = SpriteEffects.None;
        protected float layerDepth;

        //************************
        // INITIALISER
        //************************
        protected void InitialiseSprite(Texture2D texture, float x = 0, float y = 0, float scale = 1f, float layerDepth = 0)
        {
            this.texture = texture;
            this.width = texture.Bounds.Width;
            this.height = texture.Bounds.Height;
            this.position.X = x;
            this.position.Y = y;
            this.scale = scale;
            this.layerDepth = layerDepth;
        }

        //************************
        // PUBLIC METHODS
        //************************
        // Draws the sprite to a specified SpriteBatch.
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture == null)
                return;

            spriteBatch.Draw(texture, position, sourceRect, colour, rotation, origin, scale, effects, layerDepth);
        }
    }
}
