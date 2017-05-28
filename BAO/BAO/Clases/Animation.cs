using System;
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

            rotation = 0.0f;
            axis = 0.0f;
            scale = 1.0f;
        }

        public virtual void UnloadConten()
        {
            content.Unload();
        }

        public virtual void Draw()
        {
            
        }

        public virtual void Update(SpriteBatch spriteBatch)
        {
            
        }
    }
}
