using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BAO.Clases
{
    public class TittleScreen : GameScreen
    {
        private KeyboardState keyState;
        private SpriteFont font;
        private FadeAnimation fade;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //Fuente
            if (font == (null))
            {
                font = content.Load<SpriteFont>("Agency");
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) //Lo que pasa al presionar
        {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Enter))
            {
                ScreenManager.Instance.AddScreen(new SplashScreen());
            }
        }

        public override void Draw(SpriteBatch spriteBatch) //Esto es lo que dice la tittle en el string
        {

            DrawImage draw = new DrawImage();
            draw.Draw(content ,spriteBatch, "TittleScreen", Vector2.Zero, Color.White);
            
            /*spriteBatch.Draw(image, sourceRectangle, Color.White)*/
            ;
            spriteBatch.DrawString(font, "Press Start", new Vector2(450, 600), Color.White);
            
        }
    }
}
