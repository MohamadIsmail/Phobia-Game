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


namespace Phobia
{
    class GameScreen : Screen
    {
        #region Fields
        Level level;
        Levels lev = Levels.BasicLevel;
        #endregion

        #region Functions

        public void ResetGamePlay()
        {
            Level.level1_finished = false;
            Level.level2_finished = false;
            Level.level3_finished = false;
            Level.level4_finished = false;
            Level.level5_finished = false;
            Level.level6_finished = false;
            Level.level = Levels.Level5;
            Player.knives_available = 3;
            Player.lives = 3;
            LoadContent();
        }
        #region Load 
        //Creating and loading the current level using polymorphism.
        override public void LoadContent()
        {
            Tile.Content = Game1.content;
            if (Level.level == Levels.BasicLevel)
                level = new BasicLevel();
            else if (Level.level == Levels.Level1)
                level = new Level1();
            else if (Level.level == Levels.Level2)
                level = new Level2();
            else if (Level.level == Levels.Level3)
                level = new Level3();
            else if (Level.level == Levels.BasicLevel2)
                level = new BasicLevel2();
            else if (Level.level == Levels.Level4)
                level = new Level4();
            else if (Level.level == Levels.Level5)
                level = new Level5();
            else if (Level.level == Levels.Level6)
                level = new Level6();
            else if (Level.level == Levels.BossLevel)
                level = new BossLevel();

            level.LoadContent();
        }
        #endregion 

        #region Update and Draw 
        //Updates the gamescreen screen.
        override public void Update(GameTime gameTime)
        {

            if (lev != Level.level)
            {
                lev = Level.level;
                LoadContent();
            }

            level.Update(gameTime);
        }
        //Draws the gamescreen screen.
        public void Draw(SpriteBatch spriteBatch, GameTime gametime, GraphicsDevice graphic)
        {
            level.Draw(gametime, spriteBatch);

            spriteBatch.DrawString(ScreenManager.spriteFont_gameplay, "Lives : " + Player.lives.ToString(), new Vector2((graphic.Viewport.TitleSafeArea.X) + ScreenManager.camera.center.X - 600, graphic.Viewport.TitleSafeArea.Y + ScreenManager.camera.center.Y - 300), Color.White);
            spriteBatch.DrawString(ScreenManager.spriteFont_gameplay, "Knives : " + Player.knives_available.ToString(), new Vector2((graphic.Viewport.TitleSafeArea.X) + ScreenManager.camera.center.X - 600, graphic.Viewport.TitleSafeArea.Y + ScreenManager.camera.center.Y - 250), Color.Orange);

            if (level.enemies_num != 0)
                spriteBatch.DrawString(ScreenManager.spriteFont_gameplay, "Monster Killed : " + level.enemies_killed.ToString() + "/" + level.enemies_num.ToString(), new Vector2((graphic.Viewport.TitleSafeArea.X) + ScreenManager.camera.center.X + 300, graphic.Viewport.TitleSafeArea.Y + ScreenManager.camera.center.Y - 300), Color.White);
        }
        #endregion 

        #endregion
    }
}