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
    class Player : Humanoids
    {
        #region Fields

        //Player animations variables
        private Animation jumpHitAnimation;
        private Animation throwAnimation;
        private Animation hitanimation1;
        private Animation hitanimation2;
        private Animation hitanimation3;

        //Current animation booleans
        public bool Ishitting = false;
        public bool Isthrowing = false;
        public bool Isjumphitting = false;




        //Projectile
        public Projectile[] knife;
        public static int knives_available = 3;
        public static int lives = 3;

        //Sounds 
        SoundEffect fallSound;
        SoundEffect killedSound;
        SoundEffect jumpSound;

        /// <summary>
        /// Gets whether or not the player's feet are on the ground.
        /// </summary>
        // Jumping state
        private bool hasJumped = false;

        //Delay Fields 
        private float waitTime;
        private float MaxWaitTime = 2.0f;
        private float cwaitTime;
        private float cMaxWaitTime = 2.0f;
        public Texture2D LifeTexture
        {
            get { return idleAnimation.Texture; }
        }
        //Combo iterator and switch
        private int combo = 0;
        private bool c = false;

        #endregion

        #region Functions

        #region Construtor
        public Player(Vector2 position)
        {
            Load(Game1.content);
            level_position = position;
            Reset(level_position);

        }
#endregion 

        // Load animated textures.
        override public void Load(ContentManager Content)
        {
            sprite = new AnimationPlayer();

            idleAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/Idle"), 0.35f, true, 4, 0, 4, false);
            runAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/Run"), 0.12f, true, 6, 0, 6, false);
            jumpAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/Jump"), 0.12f, false, 4, 0, 4, false);
            dieAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/Die"), 0.35f, false, 3, 0, 3, false);
            hitanimation1 = new Animation(Content.Load<Texture2D>("Sprites/Player/Hit"), 0.12f, false, 13, 0, 3, false);
            hitanimation2 = new Animation(Content.Load<Texture2D>("Sprites/Player/Hit"), 0.12f, false, 13, 4, 8, false);
            hitanimation3 = new Animation(Content.Load<Texture2D>("Sprites/Player/Hit"), 0.12f, false, 13, 9, 13, false);
            jumpHitAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/JumpHit"), 0.12f, false, 3, 0, 3, false);
            throwAnimation = new Animation(Content.Load<Texture2D>("Sprites/Player/Throw"), 0.12f, false, 4, 0, 4, false);
            waitTime = 0;
            cwaitTime = 0;

            knife = new Knife[10];

            killedSound = Content.Load<SoundEffect>("Sounds/PlayerKilled");
            jumpSound = Content.Load<SoundEffect>("Sounds/PlayerJump");
            fallSound = Content.Load<SoundEffect>("Sounds/PlayerFall");

        }


        // Resets the player to life.
        override public void Reset(Vector2 position)
        {
            Position = position;

            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.SetAnimation(idleAnimation);
            localBounds = sprite.bounding(idleAnimation);
        }
        #region Update and Draw
        // Performs physics and animates the boss sprite.
        public void Update(GameTime gameTime, Map map)
        {
            if (!isAlive)
            {
                velocity.X = 0;
            }
            else
                Input(gameTime);
            position += velocity;

            if (velocity.Y < 100)
            {
                if (velocity.Y != 0)
                    hasJumped = true;
                velocity.Y += 0.365f;
            }
            for (int i = 0; i < 10; i++)
                if (knife[i] != null)
                {
                    if (!knife[i].Update(gameTime, ref map))
                        knife[i] = null;
                }

            if (!this.IsAlive && sprite.FrameIndex == sprite.Animation.FrameCount - 1)
            {

                waitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (waitTime >= MaxWaitTime)
                {
                    if (lives == 0)
                    {
                        ScreenManager.gameState = GameState.gameOver;
                        lives = 3;
                        knives_available = 3;
                    }
                    else
                        Reset(level_position);
                    waitTime = 0;
                }
            }
            if (IsAlive)
            {
                if (Ishitting)
                {
                    if (!hasJumped && sprite.Animation != jumpHitAnimation && combo == 0 && c == true)
                    {
                        sprite.SetAnimation(hitanimation1);
                        localBounds = sprite.bounding(sprite.Animation);
                    }
                    else if (!hasJumped && sprite.Animation != jumpHitAnimation && combo == 1 && c == true)
                    {
                        sprite.SetAnimation(hitanimation2);
                        localBounds = sprite.bounding(sprite.Animation);
                    }
                    else if (!hasJumped && sprite.Animation != jumpHitAnimation && combo == 2 && c == true)
                    {
                        sprite.SetAnimation(hitanimation3);
                        localBounds = sprite.bounding(sprite.Animation);
                    }
                }
                else if (Isjumphitting)
                {
                    sprite.SetAnimation(jumpHitAnimation);
                    localBounds = sprite.bounding(sprite.Animation);
                }
                else if (Isthrowing)
                {
                    sprite.SetAnimation(throwAnimation);
                    localBounds = sprite.bounding(sprite.Animation);
                }

                else if (hasJumped)
                {
                    sprite.SetAnimation(jumpAnimation);
                    localBounds = sprite.bounding(sprite.Animation);
                }
                else if (Math.Abs(Velocity.X) - 0.02f > 0)
                {
                    sprite.SetAnimation(runAnimation);
                    localBounds = sprite.bounding(sprite.Animation);
                }
                else
                {
                    sprite.SetAnimation(idleAnimation);
                    localBounds = sprite.bounding(sprite.Animation);
                }
            }

            cwaitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Ishitting && sprite.FrameIndex == sprite.Animation.End - 1)
            {
                Ishitting = false;
                Boss.isHit = false;
                c = false;
                if (combo == 0)
                    combo = 1;
                else if (combo == 1)
                    combo = 2;
                else
                    combo = 0;
            }
            if (Isjumphitting && sprite.FrameIndex == sprite.Animation.End - 1)
            {
                Isjumphitting = false;
                Boss.isHit = false;
            }
            if (cwaitTime >= cMaxWaitTime)
            {
                combo = 0;
                c = false;
                cwaitTime = 0;
            }
            if (Isthrowing && sprite.FrameIndex == sprite.Animation.End - 1)
            {
                Isthrowing = false;
            }

            foreach (CollisionTiles tile in map.CollissionTiles)
            {
                Collision(tile.Rectangle, map.Width, map.Height);
                ScreenManager.camera.Update(Position, map.Width, map.Height);
            }


        }
        // Called when the boss has been killed by monster.
        public void OnKilled(Enemy killedBy)
        {
            isAlive = false;

            if (killedBy != null)
                killedSound.Play();
            else
                fallSound.Play();

            sprite.SetAnimation(dieAnimation);
            localBounds = sprite.bounding(sprite.Animation);
            lives--;
        }
        // Called when the boss has been killed by the boss.
        public void OnKilledBoss(Boss killedBy)
        {
            isAlive = false;

            if (killedBy != null)
                killedSound.Play();
            else
                fallSound.Play();
            sprite.SetAnimation(dieAnimation);
            localBounds = sprite.bounding(sprite.Animation);
            lives--;
        }
        // Handles input.
        public void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3.85f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3.85f;
            else
                velocity.X = 0f;
            if (ScreenManager.keyboard.IsNewKeyPress(Keys.D) && Ishitting == false)
            {
                if (sprite.Animation == jumpAnimation)
                    Isjumphitting = true;
                else
                    Ishitting = true;
                if (sprite.Animation != jumpHitAnimation)
                {
                    c = true;
                    cwaitTime = 0;
                }
            }
            if (ScreenManager.keyboard.IsNewKeyPress(Keys.Space) && hasJumped == false)
            {
                position.Y -= 9f;
                velocity.Y = -10f;
                hasJumped = true;
                if (!Ishitting)
                    combo = 0;

                jumpSound.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.gameState = GameState.Menu;
                //ScreenManager.menuScreen.LoadContent();
            }
            if (ScreenManager.keyboard.IsNewKeyPress(Keys.F) && Isthrowing == false)
            {
                Isthrowing = true;
                if (knives_available > 0)
                {
                    knife[knives_available - 1] = new Knife(this.position, this.flip);
                    knives_available--;
                }
            }
        }
        //Detects collision with the world objects.
        public void Collision(Rectangle newRectangle, int X_Offset, int Y_Offset)
        {
            if (BoundingRectangle.TouchTopOF(newRectangle))
            {
                position.Y = (newRectangle.Y) + 5;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (BoundingRectangle.TouchLeftOf(newRectangle))
                position.X = newRectangle.X - BoundingRectangle.Width + 8;

            else if (BoundingRectangle.TouchRightOf(newRectangle))
                position.X = newRectangle.X + newRectangle.Width + 22;
            else if (BoundingRectangle.TouchBottomOf(newRectangle))
                velocity.Y = 4f;
            if (position.X < 20)
                position.X = 20;
            if (position.X > X_Offset - BoundingRectangle.Width + 2)
                position.X = X_Offset - BoundingRectangle.Width + 2;
            if (position.Y < 0)
                velocity.Y = 1f;
            if (position.Y > Y_Offset && isAlive)
            {
                position.Y = Y_Offset;
                OnKilled(null);
            }
        }
        // Draws the animated player.
        override public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            // Flip the sprite to face the way we are moving.

            if (Velocity.X > 0)
                flip = SpriteEffects.None;
            else if (Velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            // Draw that sprite.
            if (sprite.Animation != null)
                sprite.Draw(gametime, spriteBatch, Position, flip);
            for (int i = 0; i < 7; i++)
                if (knife[i] != null)
                {
                    knife[i].Draw(spriteBatch, gametime);
                }

        }
        #endregion

        #endregion
    }
}