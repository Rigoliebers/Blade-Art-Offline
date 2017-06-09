using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    class End : GameScreen
    {

        private Texture2D imagen;
        private SpriteFont font;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Agency24");
            imagen = content.Load<Texture2D>("patreon_logo");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Gracias por jugar la Demo. Aporten a nuestro patreon!", new Vector2(400, 200),Color.White);
            spriteBatch.Draw(imagen, new Vector2(518, 400),Color.White);
        }
    }
}
