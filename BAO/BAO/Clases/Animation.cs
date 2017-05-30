﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    class Animation
    {
        protected Texture2D image;
        protected string text;
        protected SpriteFont font;
        protected Color color;
        protected Rectangle sourceRectangle;
        private float rotation, scale, axis;
        private Vector2 origin, position;
        protected ContentManager content;
        protected bool isActive;
        protected float alpha;

        public virtual float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public bool IsActive
        {
            set { isActive = value; }
            get { return isActive; }
        }

        public float Scale
        {
            set { scale = value; }
        }


        public virtual void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            this.image = image;
            this.text = text;
            this.position = position;

            if (text != String.Empty)
            {
                font = Content.Load<SpriteFont>("AnimationFont");
                color = new Color(144, 77, 255);
            }

            if (image != null)
            {
                sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            }

            rotation = alpha = 0.0f;
            axis = 0.0f;
            scale = 1.0f;
            isActive = false;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            text = String.Empty;
            position = Vector2.Zero;
            sourceRectangle = Rectangle.Empty;
            image = null;
            isActive = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (image != null)
            {
                origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
                spriteBatch.Draw(image, position + origin, sourceRectangle, Color.White * alpha, rotation, origin, scale,
                    SpriteEffects.None, 0.0f);

            }

            if (text != String.Empty)
            {
                origin = new Vector2(font.MeasureString(text).X /2, font.MeasureString(text).Y /2);
                spriteBatch.DrawString(font, text, position+origin, color * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}
