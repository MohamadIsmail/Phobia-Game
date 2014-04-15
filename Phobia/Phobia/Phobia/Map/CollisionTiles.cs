using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobia
{
    class CollisionTiles : Tile
    {
        #region Constructor
        public CollisionTiles(char i, Rectangle newRectangle)
        {
            //loading map tiles and create rectangle around it.
            texture = Content.Load<Texture2D>("Tiles/Tile" + i);
            this.Rectangle = newRectangle;

        }
        #endregion 
    }
}
