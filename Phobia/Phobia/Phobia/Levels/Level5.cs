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
    class Level5 : Level
    {

        #region Fields

        Texture2D extra_life;

        Rectangle extralife_Rectangle;

        bool life_taken = false;

        #endregion

        #region Function

        // Constructs the level.
        override public void LoadContent()
        {
            enemies_killed = 0;
            enemies_num = 0;
            player = new Player(new Vector2(10, 950));

            map = new Map();
            InitializeTiles("Jump Level 1");
            map.Generate(tiles, 64);

            back = Game1.content.Load<Texture2D>("Backgrounds/mWPba");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle = new Rectangle(1649, 740, Door.Width, Door.Height);
            extra_life = Game1.content.Load<Texture2D>("Sprites/Items/Extra_life");
            extralife_Rectangle = new Rectangle(110, 160, extra_life.Width, extra_life.Height);

            song = Game1.content.Load<Song>("Sounds/Level5");
         
            MediaPlayer.Play(song);
        }

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);

            if (enemies_killed == enemies_num)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle))
                {
                    Level.level = Levels.BasicLevel2;
                    Level.level5_finished = true;
                }
            }
            if (!life_taken)
            {
                if (player.BoundingRectangle.Intersects(extralife_Rectangle))
                {
                    Player.lives++;
                    life_taken = true;
                }
            }

        }

        // Draws the level and its objects
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            if (enemies_killed == enemies_num)
            {
                spriteBatch.Draw(Door, Door_Rectangle, Color.White);
            }
            map.Draw(spriteBatch);
            if (!life_taken)
            {
                spriteBatch.Draw(extra_life, extralife_Rectangle, Color.White);
            }
            player.Draw(gametime, spriteBatch);
        }

        #endregion
    }
}
