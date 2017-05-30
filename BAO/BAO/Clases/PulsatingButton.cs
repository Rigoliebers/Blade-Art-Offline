using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    class PulsatingButton
    {

        private string texto;
        private bool isActive;
        private float fade = 0.0f;
        private Vector2 pos;
        private bool increase = false;
        private float fadeSpeed = 1.0f;
        private TimeSpan defaultTime, timer = new TimeSpan(0, 0 ,1);
        private bool startTimer;
        private float activateValue = 0.0f;
        private bool stopUpdating = false;
        private float defaultAlpha;
        private float alpha;
        private SpriteFont fuente;
        private Color color;
        private SpriteBatch spriteBatch;
        private bool isAnimated = true;

        public float FadeSpeed
        {
            get { return fadeSpeed; }
            set { fadeSpeed = value; }
        }
        public float ActivateValue
        {
            get { return activateValue; }
            set { activateValue = value; }
        }
        public TimeSpan Timer
        {
            get { return timer; }
            set
            {
                defaultTime = value;
                timer = defaultTime;
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsAnimated
        {
            get { return isAnimated; }
            set { isAnimated = value; }
        }


        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                if (!increase)
                {
                    alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (alpha <= 0.0f)
                {
                    alpha = 0.0f;
                    increase = true;
                }
                else if (alpha >= 1.0f)
                {
                    alpha = 1.0f;
                    increase = false;
                }
                if (!isAnimated)
                {
                    alpha = 1.0f;
                }
            }
            else
            {
                alpha = defaultAlpha;
            }
        }

        public void Draw(SpriteBatch Content, string Text, SpriteFont font, Vector2 position, Color color)
        {
            Content.DrawString(font, Text, position, color * alpha, 0.0f, Vector2.Zero, 1.0f,
                SpriteEffects.None, 0.0f);
        }
    }
}
