using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace BAO.Clases
{
    class SpriteSheetAnimation : Animation
    {
        int frameCounter;
        int switchFrame;
        Vector2 frames;
        Vector2 currentFrame;

        public Vector2 Frames {
            set { frames = value; }            
        }

        public Vector2 CurrentFrame {
            set { currentFrame = value; }
        }

        public int FrameWidth {
            get { return image.Width / (int)frames.X; }
        }

        public int FrameHeight {
            get { return image.Height / (int)frames.Y; }
        }

        public override void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            frameCounter = 0;
            switchFrame = 100; //lolwhat y tho
            frames = new Vector2(3, 4);
            currentFrame = new Vector2(0, 0);  //y controla las filas 
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameCounter>=switchFrame)
                {
                    frameCounter = 0;
                    currentFrame.X++;
                    if (currentFrame.X * FrameWidth >= image.Width)
                    {
                        currentFrame.X = 0;
                    }                    
                }
            }
            else
            {
                frameCounter = 0;
            }
            sourceRectangle = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
