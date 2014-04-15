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
    class Level6 : Level
    {

        #region Fields

        Enemy[] enemies;

        Texture2D[] items;
        Rectangle[] items_rec;
        bool[] item_taken;
        int items_num;

        #endregion

        #region Functions

        // Constructs the level.
        override public void LoadContent()
        {
            enemies_killed = 0;
            enemies_num = 9;
            player = new Player(new Vector2(741, 1300));
            enemies = new Enemy[enemies_num];
            items_num = 1;
            items = new Texture2D[items_num];
            item_taken = new bool[items_num];
            items_rec = new Rectangle[items_num];

            back = Game1.content.Load<Texture2D>("Backgrounds/level6");



            enemies[0] = new Enemy(new Vector2(236, 960), new Vector2(3, 0), 150, "MonsterA", 6);
            enemies[1] = new Enemy(new Vector2(1050, 960), new Vector2(2.5f, 0), 110, "MonsterC", 7);
            enemies[2] = new Enemy(new Vector2(1350, 830), new Vector2(2.7f, 0), 110, "MonsterB", 10);
            enemies[3] = new Enemy(new Vector2(350, 705), new Vector2(2.5f, 0), 180, "MonsterC", 7);
            enemies[4] = new Enemy(new Vector2(982, 705), new Vector2(3.3f, 0), 60, "MonsterA", 6);
            enemies[5] = new Enemy(new Vector2(873, 380), new Vector2(2.9f, 0), 70, "MonsterC", 7);
            enemies[6] = new Enemy(new Vector2(2065, 640), new Vector2(3.4f, 0), 90, "MonsterB", 10);
            enemies[7] = new Enemy(new Vector2(2495, 1022), new Vector2(3.2f, 0), 70, "MonsterA", 6);
            enemies[8] = new Enemy(new Vector2(3530, 382), new Vector2(3.5f, 0), 100, "MonsterC", 7);

            map = new Map();
            InitializeTiles("Monster Level 4");
            map.Generate(tiles, 64);

            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            items[0] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_knife");
            items_rec[0] = new Rectangle(2326, 790, items[0].Width, items[0].Height);
            Door_Rectangle = new Rectangle(3530, 200, Door.Width, Door.Height);
            song = Game1.content.Load<Song>("Sounds/Level6");
            MediaPlayer.Play(song);
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
                    Level.level = Levels.BasicLevel2;
                    Level.level6_finished = true;
                }
            }
            for (int i = 0; i < items_num; i++)
                if (!item_taken[i])
                {
                    if (player.BoundingRectangle.Intersects(items_rec[i]))
                    {
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
            player.Draw(gametime, spriteBatch);
            for (int i = 0; i < items_num; i++)
                if (!item_taken[i])
                {
                    spriteBatch.Draw(items[i], items_rec[i], Color.White);
                }
        }

        #endregion
    }
}
