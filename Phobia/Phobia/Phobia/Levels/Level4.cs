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
    class Level4 : Level
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
            enemies_num = 8;
            items_num = 2;
            items = new Texture2D[items_num];
            item_taken = new bool[items_num];
            items_rec = new Rectangle[items_num];
            player = new Player(new Vector2(10, 1100));
            enemies = new Enemy[enemies_num];

            enemies[0] = new Enemy(new Vector2(650, 1020), new Vector2(2, 0), 200, "MonsterA", 6);
            enemies[1] = new Enemy(new Vector2(1150, 1085), new Vector2(2, 0), 120, "MonsterC", 7);
            enemies[2] = new Enemy(new Vector2(690, 695), new Vector2(2, 0), 70, "MonsterB", 10);
            enemies[3] = new Enemy(new Vector2(2050, 705), new Vector2(3.2f, 0), 220, "MonsterC", 7);
            enemies[4] = new Enemy(new Vector2(3120, 825), new Vector2(3, 0), 150, "MonsterA", 6);
            enemies[5] = new Enemy(new Vector2(4350, 825), new Vector2(2, 0), 100, "MonsterA", 6);
            enemies[6] = new Enemy(new Vector2(4500, 825), new Vector2(2, 0), 100, "MonsterA", 6);
            enemies[7] = new Enemy(new Vector2(3300, 320), new Vector2(2, 0), 60, "MonsterA", 6);

            map = new Map();
            InitializeTiles("Monster Level 3");
            map.Generate(tiles, 64);

            items[0] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_knife");
            items_rec[0] = new Rectangle(520, 420, items[0].Width, items[0].Height);
            items[1] = Game1.content.Load<Texture2D>("Sprites/Items/Extra_life");
            items_rec[1] = new Rectangle(3350, 1050, items[1].Width, items[1].Height);

            Door = Game1.content.Load<Texture2D>("Sprites/Doors/Door");
            Door_Rectangle = new Rectangle(3250, 130, Door.Width, Door.Height);
            song = Game1.content.Load<Song>("Sounds/Level4");
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
                    Level.level4_finished = true;
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
            //spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
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
