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

//Inputs for the constructor are as follows,
//  A Texture2d that is the display texture
//  A Float that is the x co ord
//  A float that is the y co ord
//  A float that is the scale
//  A float that is the layerdepth

// When calling Draw, you must input the spritebatch to be used

namespace Ebay
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Rectangle? SourceRect = null;
        public Color Color = Color.White;
        public float Rotation = 0f;
        public Vector2 Origin = Vector2.Zero;
        public float Scale = 1f;
        public SpriteEffects Effects = SpriteEffects.None;
        public float LayerDepth = 0;



        public void Initialise(Texture2D Inputtedtexture, float Inputtedpositionx, float Inputtedpositiony, float Inputtedscale, float Inputtedlayerdepth)
        {
            Texture = Inputtedtexture;
            Position.X = Inputtedpositionx;
            Position.Y = Inputtedpositiony;
            Scale = Inputtedscale;
            LayerDepth = Inputtedlayerdepth;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null)
                return;

            spriteBatch.Draw(
                Texture,
                Position,
                SourceRect,
                Color,
                Rotation,
                Origin,
                Scale,
                Effects,
                LayerDepth);
        }
    }
}
