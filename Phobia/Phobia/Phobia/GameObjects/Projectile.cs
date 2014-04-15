using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Phobia
{
    abstract class Projectile
    {
        #region Fields

        public Animation Shoot;
        public AnimationPlayer sprite;
        public SpriteEffects flip = SpriteEffects.None;
        public bool IsDrawable;
        Vector2 position;
        public Vector2 velocity;
        
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X);
                int top = (int)Math.Round(Position.Y);
                return new Rectangle(left, top, sprite.Animation.FrameWidth, sprite.Animation.FrameHeight);
            }
        }

        #endregion

        #region Functions
        #region Constructor
        public Projectile(Vector2 Player_Pos, SpriteEffects Player_flip)
        {
            sprite = new AnimationPlayer();
            this.position.X = Player_Pos.X;
            this.position.Y = Player_Pos.Y - 30;
            this.flip = Player_flip;
            IsDrawable = true;
            this.Load(Game1.content);
            if (flip == SpriteEffects.None)
                velocity.X = 8;
            else
                velocity.X = -8;
        }
        #endregion
        // Load animated textures.
        public abstract void Load(ContentManager content);
        // Performs physics
        public abstract bool Update(GameTime gameTime, ref Map map);
        // Detect collision with the world objects.
        public abstract void Collision(Rectangle newRectangle, int X_Offset, int Y_Offset);
        // Draws the animated object.
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gametime);

        #endregion
    }
}
