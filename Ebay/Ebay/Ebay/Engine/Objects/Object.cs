/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Base class for all game objects.
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
using Microsoft.Xna.Framework.Media;

namespace Ebay.Engine.Objects
{
    public class Object
    {
        //************************
        // PROPERTIES
        //************************
        public int width = 0;
        public int height = 0;
        public Vector2 position = Vector2.Zero;
        public float rotation = 0f;
        public float scale = 1f;
        public bool visible = true;
    }
}
