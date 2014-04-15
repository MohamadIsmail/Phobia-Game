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
    class Level3 : Level
    {
        #region Fields


        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            player = new Player(new Vector2(1300, 800));
            map = new Map();
            InitializeTiles("Jump Level 2");
            map.Generate(tiles, 64);

            back = Game1.content.Load<Texture2D>("Backgrounds/Level3");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle = new Rectangle(-20, 100, Door.Width, Door.Height);
            song = Game1.content.Load<Song>("Sounds/lvl3");
            MediaPlayer.Play(song);
        }

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle))
            {
                Level.level = Levels.BasicLevel;
                Level.level3_finished = true;
            }

        }

        // Draws the level and its objects
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Door, Door_Rectangle, Color.White);
            map.Draw(spriteBatch);
            player.Draw(gametime, spriteBatch);
        }

        #endregion

    }
}
