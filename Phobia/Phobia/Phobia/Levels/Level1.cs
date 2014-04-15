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
    class Level1 : Level
    {

        #region Fields

        Texture2D[] items;
        Rectangle[] items_rec;
        bool[] item_taken;
        int items_num;
        Enemy[] enemies;

        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            enemies_killed = 0;
            enemies_num = 5;
            enemies = new Enemy[enemies_num];
            items_num = 2;
            items = new Texture2D[items_num];
            item_taken = new bool[items_num];
            items_rec = new Rectangle[items_num];
            player = new Player(new Vector2(10, 800));
            enemies[0] = new Enemy(new Vector2(210, 250), new Vector2(3, 0), 230, "MonsterB", 10);
            enemies[1] = new Enemy(new Vector2(800, 320), new Vector2(2, 0), 200, "MonsterC", 7);
            enemies[2] = new Enemy(new Vector2(1900, 450), new Vector2(2, 0), 150, "MonsterB", 10);
            enemies[3] = new Enemy(new Vector2(2250, 190), new Vector2(2.7f, 0), 110, "MonsterC", 7);
            enemies[4] = new Enemy(new Vector2(2600, 380), new Vector2(3.2f, 0), 150, "MonsterA", 6);
            map = new Map();
            InitializeTiles("Monster Level 1");
            map.Generate(tiles, 64);

            back = Game1.content.Load<Texture2D>("Backgrounds/HallWay");
            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            items[0] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_knife");
            items_rec[0] = new Rectangle(70, 100, items[0].Width, items[0].Height);
            items[1] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_life");
            items_rec[1] = new Rectangle(2600, 70, items[1].Width, items[1].Height);

            Door_Rectangle = new Rectangle(3325, 170, Door.Width, Door.Height);
            
            MediaPlayer.Play(Game1.content.Load<Song>("Sounds/lvl1"));
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
                    Level.level1_finished = true;
                }
            }
            for (int i = 0; i < items_num; i++)
                if (!item_taken[i])
                {
                    if (player.BoundingRectangle.Intersects(items_rec[i]))
                    {
                        if (i == 1)
                            Player.lives++;
                        else
                            Player.knives_available++;
                        item_taken[i] = true;
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
            for (int i = 0; i < enemies_num; i++)
                enemies[i].Draw(gametime, spriteBatch);

            for (int i = 0; i < 2; i++)
                if (!item_taken[i])
                {
                    spriteBatch.Draw(items[i], items_rec[i], Color.White);
                }

            player.Draw(gametime, spriteBatch);
        }

        #endregion
    }
}
