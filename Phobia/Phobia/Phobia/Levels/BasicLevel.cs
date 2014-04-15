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
    class BasicLevel : Level
    {

        #region Fields

        new Rectangle[] Door_Rectangle;
        public bool isPlaying;

        #endregion

        #region Functions

        #region Load level objects

        // Constructs the level.
        override public void LoadContent()
        {
            player = new Player(new Vector2(100, 400));
            Door_Rectangle = new Rectangle[4];
            map = new Map();

            InitializeTiles("0");
            map.Generate(tiles, 64);
            back = Game1.content.Load<Texture2D>("Backgrounds/new_cave");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle[0] = new Rectangle(600, 480, Door.Width, Door.Height);
            Door_Rectangle[1] = new Rectangle(800, 480, Door.Width, Door.Height);
            Door_Rectangle[2] = new Rectangle(1000, 480, Door.Width, Door.Height);
            Door_Rectangle[3] = new Rectangle(1380, 290, Door.Width, Door.Height);
            song = Game1.content.Load<Song>("Sounds/basicLvl");
            isPlaying = true;

        }
        #endregion

        #region Update and draw

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);
            if (isPlaying)
            {
                MediaPlayer.Play(song);
                isPlaying = false;
            }
            if (!Level.level1_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[0]))
                {
                    Level.level = Levels.Level1;
                }
            }
            if (!Level.level2_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[1]))
                {
                    Level.level = Levels.Level2;
                }
            }
            if (!Level.level3_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[2]))
                {
                    Level.level = Levels.Level3;
                }
            }
            if (Level.level1_finished && Level.level2_finished && Level.level3_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[3]))
                {
                    Level.level = Levels.BasicLevel2;
                }
            }
        }

        // Draws the level and its objects.
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            if (!Level.level3_finished)
                spriteBatch.Draw(Door, Door_Rectangle[2], Color.White);
            if (!Level.level2_finished)
                spriteBatch.Draw(Door, Door_Rectangle[1], Color.White);
            if (!Level.level1_finished)
                spriteBatch.Draw(Door, Door_Rectangle[0], Color.White);
            if (Level.level1_finished && Level.level2_finished && Level.level3_finished)
                spriteBatch.Draw(Door, Door_Rectangle[3], Color.White);
            map.Draw(spriteBatch);
            player.Draw(gametime, spriteBatch);
        }
        #endregion

        #endregion
    }
}
