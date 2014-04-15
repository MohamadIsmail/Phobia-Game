using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Phobia
{
    class WinScreen : Screen
    {

        #region Fields

        Text menuEntry;
        Text survived;

        #endregion

        #region Functions
        // Load your graphics and sound content.
        override public void LoadContent()
        {
            song = Game1.content.Load<Song>("Sounds/winScreen");
            survived = new Text("You have Conquered your fears", new Vector2(230, 150), ScreenManager.spriteFont_gameOver);
            menuEntry = new Text("Go to credits", new Vector2(30, 500), ScreenManager.spriteFont_mainMenu);
            backGround = Game1.content.Load<Texture2D>("Backgrounds/Win");
            isPlaying = true;
        }
        //Responds to user input and updates the winscreen.
        override public void Update(GameTime gametime)
        {
            if (isPlaying)
            {

                MediaPlayer.Play(song);
                isPlaying = false;

            }
            if (!menuEntry.Active)
            {
                menuEntry.Active = true;
                menuEntry.Update();
            }

            if (ScreenManager.keyboard.IsNewKeyPress(Keys.Enter))
            {
                ScreenManager.gameState = GameState.Credits;
                ScreenManager.gameScreen.ResetGamePlay();
                ScreenManager.intro_once = false;

            }
        }
        //Draws the winscreen.
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            survived.Draw(spriteBatch);
            menuEntry.Draw(spriteBatch);

        }

        #endregion
    }
}





