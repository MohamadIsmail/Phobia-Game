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
    class Level2 : Level
    {

        #region Fields

        Texture2D[] items;
        Enemy[] enemies;

        int items_num;
        Rectangle[] item_rec;
        bool[] item_taken;

        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            enemies_killed = 0;
            enemies_num = 5;
            items_num = 2;
            player = new Player(new Vector2(10, 800));
            enemies = new Enemy[enemies_num];
            items = new Texture2D[items_num];
            item_rec = new Rectangle[items_num];
            item_taken = new bool[items_num];
            enemies[0] = new Enemy(new Vector2(60, 320), new Vector2(2, 0), 50, "MonsterC", 7);
            enemies[1] = new Enemy(new Vector2(1000, 960), new Vector2(2, 0), 100, "MonsterC", 7);
            enemies[2] = new Enemy(new Vector2(1600, 575), new Vector2(2, 0), 150, "MonsterC", 7);
            enemies[3] = new Enemy(new Vector2(2250, 575), new Vector2(2, 0), 110, "MonsterB", 10);
            enemies[4] = new Enemy(new Vector2(1600, 1280), new Vector2(3, 0), 110, "MonsterB", 10);

            map = new Map();
            InitializeTiles("Monster Level 2");
            map.Generate(tiles, 64);

            back = Game1.content.Load<Texture2D>("Backgrounds/mWPba");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            items[0] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_knife");
            items[1] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_life");
            item_rec[0] = new Rectangle(10, 220, items[0].Width, items[0].Height);
            item_rec[1] = new Rectangle(600, 1370, items[0].Width, items[1].Height);
            Door_Rectangle = new Rectangle(3700, 1120, Door.Width, Door.Height);
            
            MediaPlayer.Play(Game1.content.Load<Song>("Sounds/lvl2"));
        }

        // Updates all objects in the world and performs collision between them.
        override public void Update(GameTime gameTime)
        {
            player.Update(gameTime, map);
            for (int i = 0; i < enemies_num; i++)
                enemies[i].Update(gameTime, map, ref player, this);

            if (enemies_killed == enemies_num)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && player.BoundingRectangle.Intersects(Door_Rectangle))
                {
                    Level.level = Levels.BasicLevel;
                    Level.level2_finished = true;
                }
            }
            for (int i = 0; i < items_num; i++)
                if (player.BoundingRectangle.Intersects(item_rec[i]) && !item_taken[i])
                {
                    if (i == 1)
                        Player.lives++;
                    else
                        Player.knives_available++;
                    item_taken[i] = true;
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
            for (int i = 0; i < items_num; i++)
                if (!item_taken[i])
                    spriteBatch.Draw(items[i], item_rec[i], Color.White);
            map.Draw(spriteBatch);
            for (int i = 0; i < enemies_num; i++)
                enemies[i].Draw(gametime, spriteBatch);

            player.Draw(gametime, spriteBatch);
        }

        #endregion

    }
}
