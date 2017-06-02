using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class DrawImage
    {

        private ContentManager content;
        private Vector2 position;
        private Texture2D image;
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;



        public virtual void Draw(ContentManager Content, SpriteBatch spriteBatch, string Image, Vector2 Position, Color color)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            image = content.Load<Texture2D>(Image);
            spriteBatch.Draw(image, position, color);
        }

    }
}
