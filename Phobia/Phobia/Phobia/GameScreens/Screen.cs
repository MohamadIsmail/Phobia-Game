using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Phobia
{
  abstract class Screen
  {
      #region Fields

      protected Texture2D backGround;
      protected bool isPlaying;
      protected Song song;
      protected Text Title;

      #endregion

      #region Fuctions
      //Iterating list menu entries and updating text positioning.
      virtual public void UpdateTextPositioning() { }
      // Load your graphics and sound content.
      virtual public void LoadContent() { }
      //Responds to user input and updates the screen.
      virtual public void Update(GameTime gameTime) { }
      // Draws the screen.
      virtual public void Draw(SpriteBatch spriteBatch) { }

      #endregion
  }
}