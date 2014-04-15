using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Phobia
{
    class Knife : Projectile
    {

        #region Functions

        public Knife(Vector2 Player_Pos, SpriteEffects Player_flip)
            : base(Player_Pos, Player_flip)
        {
        }
        // Load animated textures.
        override public void Load(ContentManager Content)
        {

            Shoot = new Animation(Content.Load<Texture2D>("Sprites/Items/Knife"), 0.35f, false, 1, 0, 1, false);
            sprite.SetAnimation(Shoot);
        }
        // Performs physics 
        override public bool Update(GameTime gameTime, ref Map map)
        {
            if (IsDrawable)
            {
                Position += velocity;
                foreach (CollisionTiles tile in map.CollissionTiles)
                {
                    Collision(tile.Rectangle, map.Width, map.Height);
                    if (!IsDrawable)
                        return false;
                }
            }
            return true;
        }
        // Detect collision with the world objects.
        override public void Collision(Rectangle newRectangle, int X_Offset, int Y_Offset)
        {

            if (BoundingRectangle.TouchLeftOf(newRectangle) || BoundingRectangle.TouchRightOf(newRectangle) || BoundingRectangle.TouchBottomOf(newRectangle) || (BoundingRectangle.TouchTopOF(newRectangle)))
                IsDrawable = false;
            if (Position.X >= X_Offset)
                IsDrawable = false;
        }

        // Draws the animated knife.
        override public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (IsDrawable)
            {
                if (sprite.Animation != null)
                    sprite.Draw(gametime, spriteBatch, Position, flip);
            }
        }

        #endregion
    }
}
