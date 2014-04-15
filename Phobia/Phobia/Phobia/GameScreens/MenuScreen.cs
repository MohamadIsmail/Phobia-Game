using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
namespace Phobia
{

    public enum MenuState { Main, Help }

    class MenuScreen : Screen
    {

        #region Fields

        List<Text> menuEntries, helpText;
        MenuState menuState = MenuState.Main;
        int selection;
        static new bool isPlaying = true;

        #endregion

        #region Constructor

        public MenuScreen()
        {
            selection = 1;

        }
        #endregion

        #region Load
        // Load your graphics and sound content.
        override public void LoadContent()
        {
            backGround = Game1.content.Load<Texture2D>("Backgrounds/main_menu");
            Title = new Text("Phobia", new Vector2(450, 400), ScreenManager.spriteFont_title);
            song = Game1.content.Load<Song>("Sounds/menu");

            #region Menu Items
            menuEntries = new List<Text>();
            menuEntries.Add(new Text("Continue", new Vector2(30, 80), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("NewGame", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Credits", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Help", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Quit", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            #endregion
            #region Help Items
            helpText = new List<Text>();
            helpText.Add(new Text("Help", new Vector2(30, 80), ScreenManager.spriteFont_mainMenu));
            helpText.Add(new Text("Left Arrow - Move Left.                   F - Throw knife.", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            helpText.Add(new Text("Right Arrow - Move Right.                 D - Hit.", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            helpText.Add(new Text("Up Arrow  - Enter door.                   Space - Jump.", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            helpText.Add(new Text("Enter - Back to main menu.", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            #endregion
            UpdateTextPositioning();
        }

        //Iterating list menu entries and updating text positioning.
        override public void UpdateTextPositioning()
        {
            #region Menu Items
            for (int i = 1; i < menuEntries.Count; i++)
                menuEntries[i].Position += new Vector2(0, menuEntries[i - 1].Position.Y + menuEntries[i - 1].Size.Y);
            #endregion

            #region Help Items
            for (int i = 1; i < helpText.Count; i++)
                helpText[i].Position += new Vector2(0, helpText[i - 1].Position.Y + helpText[i - 1].Size.Y);
            #endregion
        }
        #endregion

        #region Update and Draw

        //Responds to user input and updates the menuscreen screen.
        override public void Update(GameTime gametime)
        {
            #region Main
            if (isPlaying)
            {
                MediaPlayer.Play(song);
                isPlaying = false;

            }
            if (menuState == MenuState.Main)
            {
                if (ScreenManager.keyboard.Down)
                {
                    if (selection < menuEntries.Count - 1)
                        selection++;
                    else
                        selection = 0;
                }
                else if (ScreenManager.keyboard.Up)
                {
                    if (selection > 0)
                        selection--;
                    else
                    {
                        selection = menuEntries.Count - 1;
                    }
                }
                else if (ScreenManager.keyboard.PauseOrQuit)
                    ScreenManager.IsExiting = true;
                else if (ScreenManager.keyboard.MenuSelection)
                {
                    switch (selection)
                    {
                        case 0:
                            {
                                if (ScreenManager.intro_once == false)
                                {
                                    ScreenManager.gameState = GameState.intro;
                                }
                                else
                                    ScreenManager.gameState = GameState.Play;
                            }
                            break;
                        case 1:
                            {
                                ScreenManager.gameScreen.ResetGamePlay();
                                ScreenManager.intro_once = false;
                                ScreenManager.gameState = GameState.intro;
                            }
                            break;
                        case 2: ScreenManager.gameState = GameState.Credits;
                            break;
                        case 3: menuState = MenuState.Help;
                            break;
                        case 4: ScreenManager.IsExiting = true;
                            break;
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
            #endregion
            #region Help
            else
            {
                if (ScreenManager.keyboard.MenuSelection || ScreenManager.keyboard.PauseOrQuit)
                    menuState = MenuState.Main;
                if (!helpText[4].Active)
                {
                    helpText[4].Active = true;
                    helpText[4].Update();
                }
            }
            #endregion
        }

        // Draws the menuscreen screen.
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            Title.Draw(spriteBatch);
            #region Main
            if (menuState == MenuState.Main)
            {
                foreach (Text t in menuEntries)
                    t.Draw(spriteBatch);
            }
            #endregion
            #region Help
            if (menuState == MenuState.Help)
            {
                foreach (Text t in helpText)
                    t.Draw(spriteBatch);
            }
            #endregion
        }

        #endregion
    }
}