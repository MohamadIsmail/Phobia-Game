using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Phobia
{
    public class Text
    {
        #region Fields
        public string text;
        Vector2 position;
        SpriteFont spritefont;
        Color CurrentColor, BasicColor, SelectedColor;
        bool active = false;
#endregion

        #region Properties 
        public Vector2 Position { get { return position; } set { position = value; } }
        
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public Vector2 Size
        {
            get { return spritefont.MeasureString(this.text); }
        }
        #endregion 

        #region Constructor
        public Text(string text, Vector2 position, SpriteFont spriteFont)
        {
            this.text = text;
            this.position = position;
            spritefont = spriteFont;
            CurrentColor = BasicColor = Color.White;
            SelectedColor = Color.Yellow;
        }
        #endregion 

        #region Update and Draw

        //Update the text's activation + color 
        public void Update()
        {
            if (this.active && this.CurrentColor == this.BasicColor)
                this.CurrentColor = this.SelectedColor;
            else if (!this.active && this.CurrentColor == this.SelectedColor)
                this.CurrentColor = this.BasicColor;
        }

        //Draw the string
        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.DrawString(spritefont, this.text, this.position, CurrentColor);
        }
        #endregion 

    }
}
