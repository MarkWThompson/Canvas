/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Program's main entry point.
//
*/
using System;

namespace Ebay
{
#if WINDOWS || XBOX
    static class Program
    {
        //************************
        // PROPERTIES
        //************************
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}
