using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Phobia
{
    abstract class Humanoids
    {
        #region Fields

        protected Animation idleAnimation;
        protected Animation runAnimation;
        protected Animation jumpAnimation;
        protected Animation dieAnimation;
        protected Vector2 position;
        protected Vector2 velocity;

        public SpriteEffects flip = SpriteEffects.None;
        protected Vector2 level_position;
        protected bool isAlive;
        protected Rectangle localBounds;

        protected AnimationPlayer sprite;

        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }


        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - sprite.Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }

        }
        #endregion 


        #region Functions
        // Performs physics and animates the sprites.
        virtual public void Update(GameTime gameTime) { }
        // Draws the animated object.
        virtual public void Draw(GameTime gametime, SpriteBatch spriteBatch) { }
        // Load animated textures.
        virtual public void Load(ContentManager Content) { }
        // Resets the object to life.
        virtual public void Reset(Vector2 position) { }

        #endregion
    }
}
