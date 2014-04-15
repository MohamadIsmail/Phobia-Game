using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobia
{
    /// <summary>
    /// Controls playback of an Animation.
    /// </summary>
    class AnimationPlayer
    {

        #region Fields

        Animation animation;
        private float time;
        int frameIndex;
#endregion

        #region Properties
        /// <summary>
        /// Gets the animation which is currently playing.
        /// </summary>
        public Animation Animation
        {
            get { return animation; }
        }
        
        /// <summary>
        /// Gets the index of the current frame in the animation.
        /// </summary>
        public int FrameIndex
        {
            get { return frameIndex; }
        }

        /// <summary>
        /// The amount of time in seconds that the current frame has been shown for.
        /// </summary>
        
        /// <summary>
        /// Gets a texture origin at the bottom center of each frame.
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight); }
        }


        #endregion

        #region Functions

        /// <summary>
        /// Begins or continues playback of an animation.
        /// </summary>
        public void SetAnimation(Animation animation)
        {
            // If this animation is already running, do not restart it.
            if (Animation == animation)
                return;

            // Start the new animation.
            this.animation = animation;

            if (!animation.Reverse)
                this.frameIndex = animation.Start;
            else
                this.frameIndex = animation.End - 1;

            this.time = 0.0f;
        }

        //Calculate the local bounds around the current frame.
        public Rectangle bounding(Animation animation)
        {
            int width = (int)(animation.FrameWidth * 0.4);
            int left = (int)((animation.FrameWidth - width) / 1.75f);
            int height = (int)(animation.FrameHeight * 0.65);
            int top = animation.FrameHeight - height;
            return new Rectangle(left, top, width, height);
        }
        /// <summary>
        /// Advances the time position and draws the current frame of the animation.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            // Process passing time.
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > Animation.FrameTime)
            {
                time -= Animation.FrameTime;

                // Advance the frame index; looping or clamping as appropriate.
                if (!Animation.Reverse)
                {
                    if (Animation.IsLooping)
                    {
                        frameIndex = (frameIndex + 1) % Animation.End;
                    }
                    else
                    {
                        frameIndex = Math.Min(frameIndex + 1, Animation.End - 1);
                    }
                }
                else
                {
                    if (Animation.IsLooping)
                    {
                        if (frameIndex > Animation.Start)
                            frameIndex = frameIndex - 1;
                        else
                            frameIndex = Animation.End - 1;
                    }
                    else
                    {
                        frameIndex = Math.Max(frameIndex - 1, Animation.Start);
                    }
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(FrameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.Texture.Height);

            // Draw the current frame.
            spriteBatch.Draw(Animation.Texture, position, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
        }

        #endregion
    }
}