using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Phobia
{
    class GameOver : Screen
    {

        #region Fields

        List<Text> menuEntries;
        Text died;
        int selection;

        #endregion

        #region Functions


        #region Constructor
        public GameOver()
        {
            selection = 0;
        }
        #endregion
        #region Load 
        // Load your graphics and sound content.
        override public void LoadContent()
        {

            died = new Text("You Died...", new Vector2(480, 450), ScreenManager.spriteFont_gameOver);
            menuEntries = new List<Text>();
            menuEntries.Add(new Text("Restart Game", new Vector2(30, 80), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Main Menu", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            song = Game1.content.Load<Song>("Sounds/Gameover");
            backGround = Game1.content.Load<Texture2D>("Backgrounds/GameOver");
            isPlaying = true;
            UpdateTextPositioning();
        }
        //Iterating list menu entries and updating text positioning.
        override public void UpdateTextPositioning()
        {
            for (int i = 1; i < menuEntries.Count; i++)
                menuEntries[i].Position += new Vector2(0, menuEntries[i - 1].Position.Y + menuEntries[i - 1].Size.Y);


        }
        #endregion 

        #region Update and Draw
        //Responds to user input and updates the gameover screen.
        override public void Update(GameTime gametime)
        {
            if (isPlaying)
            {
                MediaPlayer.Play(song);
                isPlaying = false;

            }
            if (ScreenManager.keyboard.IsNewKeyPress(Keys.Down))
            {
                if (selection < menuEntries.Count - 1)
                    selection++;
                else
                    selection = 0;
            }
            else if (ScreenManager.keyboard.IsNewKeyPress(Keys.Up))
            {
                if (selection > 0)
                    selection--;
                else
                    selection = menuEntries.Count - 1;
            }
            else if (ScreenManager.keyboard.IsNewKeyPress(Keys.Enter))
            {
                ScreenManager.gameScreen.ResetGamePlay();
                if (selection == 0)
                {
                    ScreenManager.gameState = GameState.Play;
                }
                else
                {

                    ScreenManager.gameState = GameState.Menu;
                    ScreenManager.intro_once = false;
                }
            }
            for (int i = 0; i < menuEntries.Count; i++)
            {
                if (i == selection)
                {
                    if (!menuEntries[i].Active)
                        menuEntries[i].Active = true;
                }
                else
                {
                    if (menuEntries[i].Active)
                        menuEntries[i].Active = false;
                }
                menuEntries[i].Update();
            }
        }
        // Draws the gameover screen.
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            died.Draw(spriteBatch);
            foreach (Text t in menuEntries)
                t.Draw(spriteBatch);

        }
        #endregion 

        #endregion
    }
}