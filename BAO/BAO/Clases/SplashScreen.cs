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
    public class SplashScreen : GameScreen
    {
        private KeyboardState keyState;
        private SpriteFont font;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //Fuente
            if (font == null)
            {
                font = content.Load<SpriteFont>("Font1");
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) //Lo que pasa al presionar
        {

            //keyState = Keyboard.GetState();
            inputManager.Update();
            if (inputManager.KeyPressed(Keys.Z))
            {
                ScreenManager.Instance.AddScreen(new GameplayScreen());
            }
        }

        public override void Draw(SpriteBatch spriteBatch) //Esto es lo que dice la tittle en el string
        {
            DrawImage draw = new DrawImage();
            draw.Draw(content, spriteBatch, "Splash", Vector2.Zero, Color.White);
            //spriteBatch.DrawString(font, "SplashScreen", new Vector2(100,100), Color.AliceBlue);
        }
    }
}
