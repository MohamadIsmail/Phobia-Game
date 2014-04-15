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
    class BossLevel : Level
    {

        #region Fields

        Boss boss;

        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            player = new Player(new Vector2(100, 400));
            boss = new Boss(new Vector2(300, 580));
            map = new Map();
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle = new Rectangle(1380, 290, Door.Width, Door.Height);
            InitializeTiles("0");
            map.Generate(tiles, 64);
            back = Game1.content.Load<Texture2D>("Backgrounds/Bosslevel");
            MediaPlayer.Play(Game1.content.Load<Song>("Sounds/BossLevel"));
        }

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);
            boss.Update(gameTime, ref player, ref map);
            if (!boss.IsAlive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle))
                {
                    ScreenManager.gameState = GameState.Win;
                }
            }
        }

        // Draws the level and its objects.
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            map.Draw(spriteBatch);
            if (!boss.IsAlive)
                spriteBatch.Draw(Door, Door_Rectangle, Color.White);
            player.Draw(gametime, spriteBatch);
            boss.Draw(gametime, spriteBatch, player);

        }

        #endregion
    }
}

