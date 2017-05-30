using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BAO.Clases
{
    class OptionsScreen : GameScreen
    {

        private KeyboardState keyState;
        private SpriteFont font;
        private SoundEffect soundsMenuUp;
        private SoundEffect soundsMenuDown;
        private SoundEffect soundStart;
        private int states;
        private bool keydownU;
        private bool keydownD;
        private PulsatingButton btn1;
        protected Texture2D image;
        protected Rectangle sourceRectangle;


        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //Fuente
            if (font == (null))
            {
                font = content.Load<SpriteFont>("Menufont");
            }

            sourceRectangle = new Rectangle(0,0, 1024-100, 800-100);
            soundsMenuUp = Content.Load<SoundEffect>("misc_menu_2");
            soundsMenuDown = Content.Load<SoundEffect>("misc_menu_4");
            soundStart = Content.Load<SoundEffect>("positive");
            image = Content.Load<Texture2D>("pixel");
        }

        public override void Update(GameTime gameTime) //Lo que pasa al presionar
        {
            keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (states == 0)
                {
                    states = 1;
                    soundStart.Play();
                }
                else
                {
                    switch (states)
                    {
                        case 2:

                            break;

                        case 3:

                            break;

                        case 4:

                            break;

                        case 5:
                            Environment.Exit(1);
                            break;
                    }
                }
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                if (!keydownU)
                {
                    keydownU = true;
                    if (states > 2 && states < 6)
                    {
                        states--;
                        soundsMenuUp.Play(1.0f, 0.0f, 0.0f);
                    }
                }
            }

            if (keyState.IsKeyUp(Keys.Up))
            {
                keydownU = false;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                if (!keydownD)
                {
                    keydownD = true;
                    if (states < 5 && states > 1)
                    {
                        states++;
                        soundsMenuDown.Play(0.70f, 0.0f, 0.0f);
                    }
                }
            }

            if (keyState.IsKeyUp(Keys.Down))
            {
                keydownD = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) //Esto es lo que dice la tittle en el string
        {

            DrawImage draw = new DrawImage();
            draw.Draw(content, spriteBatch, "TittleScreen", Vector2.Zero, Color.White);
            spriteBatch.Draw(image, new Vector2(512, 400), sourceRectangle, Color.White * 0.5f, 0.0f, new Vector2(512, 400), new Vector2(512, 400), SpriteEffects.None, 0.4f);
            States(spriteBatch);
        }

        private void States(SpriteBatch spriteBatch)
        {
            switch (states)
            {

            }
        }

    }
}
