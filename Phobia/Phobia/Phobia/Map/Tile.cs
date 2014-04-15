using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Phobia
{
    class Tile
    {
        #region Fields
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;
        #endregion

        #region Properties
        //Get the bounding rectangle around the tile.
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }
        //get the XNA built-in loader for loading the tile. 
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        #endregion

        #region Draw

        //draw the tile on the screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        #endregion
    }
}
