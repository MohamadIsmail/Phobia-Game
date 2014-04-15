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
using System.IO;

namespace Phobia
{
    public enum Levels { BasicLevel, Level1, Level2, Level3, BasicLevel2, Level4, Level5, Level6, BossLevel }
    abstract class Level
    {
        #region Fields

        //Booleans to determine if a level is done
        public static bool level1_finished ;
        public static bool level2_finished ;
        public static bool level3_finished ;
        public static bool level4_finished ;
        public static bool level5_finished ;
        public static bool level6_finished ;

        public Player player;
        public Map map;
        protected Texture2D Door;
        protected Texture2D back;
        protected char[,] tiles;
        public static Levels level;
        public int enemies_killed;
        public int enemies_num;
        protected Song song;
        protected Rectangle Door_Rectangle;
        // Gets tiles positions from stream containing the tile data and inserts them in an array to allocate the tile grid.
        protected void InitializeTiles(string txtname)
        {
            int width;
            List<string> lines = new List<string>();
            string Path = string.Format("Content/Levels/" + txtname + ".txt");
            using (Stream fileStream = TitleContainer.OpenStream(Path))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            tiles = new char[lines.Count, width];
            // Loop over every tile position,
            for (int x = 0; x < lines.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    // to load each tile.
                    tiles[x, y] = (char)lines[x][y] ;
                }
            }
        }
        #endregion

        #region Functions
        // Constructs the level.
        abstract public void LoadContent();
        // Updates all objects in the world and performs collision between them.
        abstract public void Update(GameTime gameTime);
        // Draws the level and its objects
        abstract public void Draw(GameTime gametime, SpriteBatch spriteBatch);

        #endregion

    }
}
