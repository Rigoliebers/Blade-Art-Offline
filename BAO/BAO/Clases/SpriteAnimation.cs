using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class SpriteAnimation
    {
        private bool activator;
        Texture2D spriteStrip;
        float scale;
        int elapsedTime;
        int frameTime;
        int frameCount;
        int currentFrame;
        Color color;
        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();
        public int FrameWidth;  //32 * 50
        public int FrameHeight;
        public bool Active;
        public bool Looping;
        public Vector2 Position;
        public Vector2 currentPosition;
        public bool Reverse;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;
            this.currentPosition = position;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            elapsedTime = 0;
            currentFrame = 0;

            activator = true;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!Active) return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime)
            {
                if (Reverse)
                {
                    if ((currentFrame < frameCount) && (activator == true))
                    {
                        activator = true;
                        currentFrame++;
                    }
                    else if (currentFrame > 0)
                    {
                        activator = false;
                        currentFrame--;
                        if (currentFrame == 0)
                        {
                            activator = true;
                        }
                    }
                    elapsedTime = 0;
                }
                else
                {
                    currentFrame++;
                    if (currentFrame == frameCount)
                    {
                        if (!Looping) Active = false;
                        currentFrame = 0;
                    }
                    elapsedTime = 0;
                }


            }

            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            destinationRect = new Rectangle(
                (int)Position.X - (int)(FrameWidth * scale) / 2,
                (int)Position.Y - (int)(FrameHeight * scale) / 2,
                (int)(FrameWidth * scale),
                (int)(FrameHeight * scale)
                );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }

    }
}