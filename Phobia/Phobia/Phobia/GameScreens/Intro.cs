using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Storage;

namespace Phobia
{
    class Intro : Screen
    {

        #region Fields

        List<List<Text>> Story;
        List<Text> Paragraph;
        int index = 0;

        #endregion

        #region Functions

        public Intro()
        {
            Story = new List<List<Text>>(3);
            Paragraph = new List<Text>();
        }
        // Load your graphics and sound content.
        override public void LoadContent()
        {
            string Path = string.Format("Content/Intro/" + (index + 1) + ".txt");
            Paragraph.Add(new Text("", new Vector2(20, 70), ScreenManager.spriteFont_intro));
            using (Stream fileStream = TitleContainer.OpenStream(Path))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Paragraph.Add(new Text(line, new Vector2(20, 5.0f), ScreenManager.spriteFont_intro));
                    line = reader.ReadLine();
                }
            }
            Story.Add(Paragraph);
            backGround = Game1.content.Load<Texture2D>("Backgrounds/Hospital");
            UpdateTextPositioning();
        }
        //Iterating list menu entries and updating text positioning.
        override public void UpdateTextPositioning()
        {
            for (int i = 1; i < Story[index].Count; i++)
                Story[index][i].Position += new Vector2(0, Story[index][i - 1].Position.Y + Story[index][i - 1].Size.Y);
        }
        //Responds to user input and updates the intro screen.
        override public void Update(GameTime gametime)
        {
            if (!Story[index][Story[index].Count - 1].Active)
            {
                Story[index][Story[index].Count - 1].Active = true;
                Story[index][Story[index].Count - 1].Update();
            }
            if (ScreenManager.keyboard.IsNewKeyPress(Keys.Enter))
            {
                if (index < 2)
                {

                    index++;
                    Paragraph.Clear();
                    LoadContent();
                    // UpdateTextPositioning();

                }
                else
                {
                    index = 0;
                    Paragraph.Clear();
                    Story.Clear();
                    ScreenManager.intro_once = true;
                    ScreenManager.gameState = GameState.Play;
                }
            }
        }
        // Draws the intro screen.
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            foreach (Text t in Story[index])
                t.Draw(spriteBatch);
        }

        #endregion
    }
}
