using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Phobia
{

    class Enemy : Humanoids
    {
        #region Fields

        private int range = 150;

        SoundEffect killedSound;

        #endregion

        #region Functions

        #region Constructor
        public Enemy(Vector2 position, Vector2 velocity, int range, string spriteset, int frames)
        {
            Load(Game1.content, spriteset, frames);
            level_position = position;
            this.range = range;
            this.velocity = velocity;
            Reset(position);
        }
        #endregion

        #region Load
        // Load animated textures.
        public void Load(ContentManager Content, string spriteSet, int frames)
        {
            sprite = new AnimationPlayer();
            spriteSet = "Sprites/" + spriteSet + "/";
            runAnimation = new Animation(Content.Load<Texture2D>(spriteSet + "Run"), 0.1f, true, frames, 0, frames, false);
            dieAnimation = new Animation(Content.Load<Texture2D>(spriteSet + "Die"), 0.1f, false, frames, 0, frames, false);



            // Load sounds.            
            killedSound = Content.Load<SoundEffect>("Sounds/MonsterKilled");

        }

        // Resets the enemy to life.
        override public void Reset(Vector2 position)
        {
            Position = position;
            isAlive = true;
            // Calculate bounds within texture size. 
            sprite.SetAnimation(runAnimation);
            localBounds = sprite.bounding(sprite.Animation);
        }
        #endregion

        #region  Update and Draw
        // Performs physics and animates the enemy sprite.
        public void Update(GameTime gameTime, Map map, ref Player player, Level level)
        {
            position += velocity;

            if (position.X > level_position.X + range)
                velocity.X *= -1;
            else if (position.X < level_position.X - range)
                velocity.X *= -1;

            Collision_With_player(ref player, level);
        }
        // Detect collision with player
        public void Collision_With_player(ref Player player, Level level)
        {

            if ((BoundingRectangle.TouchLeftOf(player.BoundingRectangle) || BoundingRectangle.TouchRightOf(player.BoundingRectangle)) && player.IsAlive)
            {
                if ((player.Ishitting) && ((player.flip == SpriteEffects.None && this.position.X > player.Position.X) || (player.flip == SpriteEffects.FlipHorizontally && this.position.X < player.Position.X)))
                {
                    if (this.isAlive)
                    {
                        level.enemies_killed++;
                        this.OnKilled(player);
                    }
                }
                else if (this.isAlive)
                    player.OnKilled(this);
            }
            for (int i = 0; i < 10; i++)
            {
                if (player.knife[i] != null)
                {
                    if (player.knife[i].BoundingRectangle.Intersects(this.BoundingRectangle))
                    {
                        if (this.isAlive)
                        {
                            player.knife[i].IsDrawable = false;
                            player.knife[i] = null;
                            level.enemies_killed++;
                            this.OnKilled(player);
                        }
                    }
                }
            }
        }

        // Draws the animated enemy.
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            // Draw facing the way the enemy is moving.
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;
            sprite.Draw(gametime, spriteBatch, Position, flip);
        }
        #endregion 
        // Called when the enemy has been killed.
        public void OnKilled(Player killedBy)
        {
            isAlive = false;
            velocity.X = 0;
            sprite.SetAnimation(dieAnimation);
            killedSound.Play();
        }

        #endregion
    }
}

