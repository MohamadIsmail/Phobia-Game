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
    class Credits : Screen
    {

        #region Fields

        List<Text> menuEntries;

        #endregion

        #region Functions
        // Load your graphics content.
        override public void LoadContent()
        {
            menuEntries = new List<Text>();
            Title = new Text("Phobia", new Vector2(450, 400), ScreenManager.spriteFont_title);
            menuEntries.Add(new Text("This game was made by : ", new Vector2(30, 80), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Omar Mohamed                       Mohamed Ismail", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Fawzy Abdallah                     Abdallah Hassan", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("FCIS Ain-Shams University.", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            menuEntries.Add(new Text("Back to main menu", new Vector2(30, 5.0f), ScreenManager.spriteFont_mainMenu));
            backGround = Game1.content.Load<Texture2D>("Backgrounds/main_menu");
            UpdateTextPositioning();
        }
        //Iterating list menu entries and updating text positioning.
        override public void UpdateTextPositioning()
        {
            for (int i = 1; i < menuEntries.Count; i++)
                menuEntries[i].Position += new Vector2(0, menuEntries[i - 1].Position.Y + menuEntries[i - 1].Size.Y);
        }
        //Responds to user input and updates the credits screen.
        override public void Update(GameTime gametime)
        {
            if (!menuEntries[4].Active)
            {
                menuEntries[4].Active = true;
                menuEntries[4].Update();
            }

            if (ScreenManager.keyboard.IsNewKeyPress(Keys.Enter))
                ScreenManager.gameState = GameState.Menu;
        }
        // Draws the credits screen.
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            Title.Draw(spriteBatch);
            foreach (Text t in menuEntries)
                t.Draw(spriteBatch);

        }

        #endregion
    }
}
