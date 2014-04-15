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
    class BasicLevel2 : Level
    {

        #region Fields

        new Rectangle[] Door_Rectangle;
        public bool isPlaying;

        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            player = new Player(new Vector2(100, 500));
            Door_Rectangle = new Rectangle[4];
            map = new Map();

            InitializeTiles("0");
            map.Generate(tiles, 64);
            back = Game1.content.Load<Texture2D>("Backgrounds/BasicLevel2");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle[0] = new Rectangle(500, 480, Door.Width, Door.Height);
            Door_Rectangle[1] = new Rectangle(700, 480, Door.Width, Door.Height);
            Door_Rectangle[2] = new Rectangle(900, 480, Door.Width, Door.Height);
            Door_Rectangle[3] = new Rectangle(1370, 290, Door.Width, Door.Height);
            song = Game1.content.Load<Song>("Sounds/Basiclevel2");
            isPlaying = true;

        }

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);
            if (isPlaying)
            {
                MediaPlayer.Play(song);
                isPlaying = false;
            }
            if (!Level.level4_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[0]))
                {
                    Level.level = Levels.Level4;
                }
            }
            if (!Level.level5_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[1]))
                {
                    Level.level = Levels.Level5;
                }
            }
            if (!Level.level6_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[2]))
                {
                    Level.level = Levels.Level6;
                }
            }
            if (Level.level4_finished && Level.level5_finished && Level.level6_finished)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle[3]))
                {
                    Level.level = Levels.BossLevel;
                }
            }
        }

        // Draws the level and its objects.
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            if (!Level.level4_finished)
                spriteBatch.Draw(Door, Door_Rectangle[0], Color.White);
            if (!Level.level5_finished)
                spriteBatch.Draw(Door, Door_Rectangle[1], Color.White);
            if (!Level.level6_finished)
                spriteBatch.Draw(Door, Door_Rectangle[2], Color.White);
            if (Level.level4_finished && Level.level5_finished && Level.level6_finished)
                spriteBatch.Draw(Door, Door_Rectangle[3], Color.White);
            map.Draw(spriteBatch);
            player.Draw(gametime, spriteBatch);
        }

        #endregion
    }
}
