using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    class THEWORLDTimer
    {

        private double CD;
        public double seconds;
        public double miliseconds;
        private SpriteFont font;
        private Vector2 position;
        public bool Active;

        public void LoadContent(ContentManager content, Vector2 pos)
        {
            font = content.Load<SpriteFont>("Font1");
            position = pos;
            Active = false;
        }
        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                miliseconds += gameTime.ElapsedGameTime.Milliseconds;

                if (miliseconds >= 1000)
                {
                    miliseconds = 0;
                    seconds++;
                }
            }
        }

        public void Reset()
        {
            seconds = 0;
            miliseconds = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, seconds+"."+miliseconds, position, Color.White);
        }
    }
}
