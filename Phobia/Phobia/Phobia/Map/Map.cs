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
    class Map
    {
        #region Fields 
        private List<CollisionTiles> tiles = new List<CollisionTiles>();
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private int height, width;

        #endregion

        #region Properties 
        //Get the list of tiles in the current map.
        public List<CollisionTiles> Tiles
        {
            get { return tiles; }
        }

        public List<CollisionTiles> CollissionTiles
        {
            get { return collisionTiles; }
        }
        //Get the height of the current map. 
        public int Height
        {
            get { return height; }
        }

        //Get the width of the current map. 
        public int Width
        {
            get { return width; }
        }
        #endregion 

        #region Functions
        //generates the current map by loading the tiles in it. 
        public void Generate(char[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    char number = map[y, x];
                    if (number != '0')
                    {
                        Tiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                        if (y == 0)
                        {
                            if (x == 0)
                            {
                                if (map[y + 1, x] == '0' || map[y, x + 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else if (x == map.GetLength(1) - 1)
                            {
                                if (map[y + 1, x] == '0' || map[y, x - 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else
                            {
                                if (map[y + 1, x] == '0' || map[y, x - 1] == '0' || map[y, x + 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                        }
                        else if (y == map.GetLength(0) - 1)
                        {
                            if (x == 0)
                            {
                                if (map[y - 1, x] == '0' || map[y, x + 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else if (x == map.GetLength(1) - 1)
                            {
                                if (map[y - 1, x] == '0' || map[y, x - 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else
                            {
                                if (map[y - 1, x] == '0' || map[y, x - 1] == '0' || map[y, x + 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                        }
                        else
                        {
                            if (x == 0)
                            {
                                if (map[y - 1, x] == '0' || map[y + 1, x] == '0' || map[y, x + 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else if (x == map.GetLength(1) - 1)
                            {
                                if (map[y - 1, x] == '0' || map[y + 1, x] == '0' || map[y, x - 1] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                            else
                            {
                                if (map[y - 1, x] == '0' || map[y, x - 1] == '0' || map[y, x + 1] == '0' || map[y + 1, x] == '0')
                                    collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                            }
                        }
                    }
                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }
        //Draw the current map.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in Tiles)
                tile.Draw(spriteBatch);

        }
        #endregion 
    }
}
