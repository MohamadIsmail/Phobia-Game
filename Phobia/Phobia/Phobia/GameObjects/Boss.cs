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

    class Boss : Humanoids
    {
        #region Fields

        private Animation appearAnimation;
        private Animation fadeAnimation;
        private Animation fireAnimation;
        private int boss_lives;
        private int[] arrRange;
        public static bool isHit;
        Projectile fireball;

        SoundEffect killedSound;

        #endregion

        #region Functions
       
        #region Constructor
        public Boss(Vector2 position)
        {
            Load(Game1.content);
            boss_lives = 10;
            IsAlive = true;
            Position = position;
            isHit = false;
        }
        #endregion 
        #region Load
        // Load animated textures.
        override public void Load(ContentManager Content)
        {


            appearAnimation = new Animation(Content.Load<Texture2D>("Sprites/Boss/Appear"), 0.05f, false, 28, 0, 28, false);
            fireAnimation = new Animation(Content.Load<Texture2D>("Sprites/Boss/Fire"), 0.2f, false, 12, 0, 12, false);
            idleAnimation = new Animation(Content.Load<Texture2D>("Sprites/Boss/Fire"), 0.7f, false, 12, 0, 5, true);
            fadeAnimation = new Animation(Content.Load<Texture2D>("Sprites/Boss/Appear"), 0.05f, false, 28, 0, 28, true);
            dieAnimation = new Animation(Content.Load<Texture2D>("Sprites/MonsterA/Die"), 0.35f, false, 7, 0, 7, false);
            sprite = new AnimationPlayer();
            arrRange = new int[20];
            for (int i = 0; i < 20; i++)
                arrRange[i] = i * 40;
            localBounds = sprite.bounding(idleAnimation);
            // Load sounds.            
            killedSound = Content.Load<SoundEffect>("Sounds/MonsterKilled");
            int width = (int)(idleAnimation.FrameWidth * 0.7);
            int left = (idleAnimation.FrameWidth - width) / 2;
            int height = (int)(idleAnimation.FrameHeight);
            int top = idleAnimation.FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);


            // Set the initial animation of Boss.
            sprite.SetAnimation(appearAnimation);

        }

        // Resets the Boss to life.
        override public void Reset(Vector2 position)
        {
            this.position = position;
            IsAlive = true;
        }
        // Performs physics and animates the boss sprite.
        public void Update(GameTime gameTime, ref Player player, ref Map map)
        {

            if (fireball != null)
            {
                if (fireball.IsDrawable)
                    fireball.Update(gameTime, ref map);
                else
                    fireball = null;
            }
            velocity.Y = 0f;

            if (sprite.FrameIndex == sprite.Animation.End - 1 && sprite.Animation == appearAnimation)
            {

                sprite.SetAnimation(fireAnimation);
            }
            if (sprite.FrameIndex == sprite.Animation.End - 1 && sprite.Animation == fireAnimation)
            {
                fireball = new FireBall(this.position, this.flip);
                sprite.SetAnimation(idleAnimation);
            }
            if (sprite.FrameIndex == sprite.Animation.Start && sprite.Animation == idleAnimation)
            {

                sprite.SetAnimation(fadeAnimation);

            }
            if (sprite.FrameIndex == sprite.Animation.Start && sprite.Animation == fadeAnimation)
            {
                Random random = new Random();
                position.X = arrRange[random.Next(0, 19)];
                sprite.SetAnimation(appearAnimation);

            }



            Collision_With_player(ref player);
        }
        #endregion
        // Detect collision with player
        public void Collision_With_player(ref Player player)
        {


            if ((BoundingRectangle.TouchLeftOf(player.BoundingRectangle) || BoundingRectangle.TouchRightOf(player.BoundingRectangle)) && player.IsAlive && !isHit)
            {
                if (player.Ishitting)
                {
                    if (this.sprite.Animation != idleAnimation)
                    {
                        player.OnKilledBoss(this);
                    }
                    else
                    {
                        isHit = true;
                        OnKilled(player);
                    }
                }

                else if (this.IsAlive && sprite.Animation != idleAnimation && player.IsAlive)
                {
                    //player dies
                    player.OnKilledBoss(this);
                }

            }
            if (fireball != null && fireball.BoundingRectangle.Intersects(player.BoundingRectangle) && player.IsAlive)
            {
                player.OnKilledBoss(this);
                fireball.IsDrawable = false;
            }
            for (int i = 0; i < 7; i++)
            {
                if (player.knife[i] != null)
                {
                    if (player.knife[i].BoundingRectangle.Intersects(this.BoundingRectangle))
                    {
                        if (this.IsAlive)
                        {
                            player.knife[i].IsDrawable = false;
                            player.knife[i] = null;
                            if (this.sprite.Animation == idleAnimation)
                                this.OnKilled(player);
                        }
                    }
                }
            }

        }

        // Draws the animated boss.
        public void Draw(GameTime gametime, SpriteBatch spriteBatch, Player player)
        {


            if (fireball != null)
                fireball.Draw(spriteBatch, gametime);
            // Draw facing the way the enemy is moving.
            if (IsAlive)
            {
                if (position.X > player.Position.X)
                    flip = SpriteEffects.FlipHorizontally;
                else if (position.X < player.Position.X)
                    flip = SpriteEffects.None;
            }

            if (sprite.Animation != null)

                sprite.Draw(gametime, spriteBatch, Position, flip);
        }

        // Called when the boss has been hit.
        public void OnKilled(Player killedBy)
        {
            if (boss_lives > 1)
            {
                boss_lives--;
            }
            else
            {
                IsAlive = false;
                sprite.SetAnimation(dieAnimation);
            }
            killedSound.Play();
        }

        #endregion
    }
}