using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BAO.Clases
{
    public class TittleScreen : GameScreen
    {
        private KeyboardState keyState;
        private SpriteFont Startfont;
        private SpriteFont Menufont;
        private FadeAnimation fade;
        private Vector2 position;
        private int states;
        private PulsatingButton btn1;
        private PulsatingButton btn2;
        private PulsatingButton btn3;
        private PulsatingButton btn4;
        private int selection;
        private Timer timer;
        int counter = 1;
        int limit = 50;
        float countDuration = 2f; //every  2s.
        float currentTime = 0f;
        private bool keydownU;
        private bool keydownD;
        SoundEffect soundEngine;
        SoundEffectInstance soundEngineInstance;
        private SoundEffect soundsMenuUp;
        private SoundEffect soundsMenuDown;
        private SoundEffect soundStart;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //Fuente
            if (Startfont == (null))
            {
                Startfont = content.Load<SpriteFont>("Agency24");
                Menufont = content.Load<SpriteFont>("Menufont");
            }

            states = 0;
            btn1 = new PulsatingButton();
            btn2 = new PulsatingButton();
            btn3 = new PulsatingButton();
            btn4 = new PulsatingButton();
            btn1.IsActive = true;


            soundEngine = Content.Load<SoundEffect>("Libera Me From Hell");
            soundEngineInstance = soundEngine.CreateInstance();
            soundsMenuUp = Content.Load<SoundEffect>("misc_menu_2");
            soundsMenuDown = Content.Load<SoundEffect>("misc_menu_4");
            soundStart = Content.Load<SoundEffect>("positive");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) //Lo que pasa al presionar esto abarca del 14 al 21 ffs orz
        {
            keyState = Keyboard.GetState();
            //currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            btn1.Update(gameTime);
            btn2.Update(gameTime);
            btn3.Update(gameTime);
            btn4.Update(gameTime);

            if (soundEngineInstance.State == SoundState.Stopped)
            {
                soundEngineInstance.Volume = 0.15f;
                soundEngineInstance.IsLooped = true;
                soundEngineInstance.Play();
            }
            else
                soundEngineInstance.Resume();

            inputManager.Update();
            if (inputManager.KeyPressed(Keys.Enter))
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
                            ScreenManager.Instance.AddScreen(new GameplayScreen(), 10.0f);
                            break;

                        case 3:

                            break;

                        case 4:
                            ScreenManager.Instance.AddScreen(new OptionsScreen(), 10.0f);
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
            draw.Draw(content ,spriteBatch, "TittleScreen", Vector2.Zero, Color.White);

            States(spriteBatch);
            
        }

        private void DrawButtons(SpriteBatch spriteBatch)
        {
            btn1.Draw(spriteBatch, "New Game", Menufont, new Vector2(450, 600), Color.White);
            btn2.Draw(spriteBatch, "Load Game", Menufont, new Vector2(450, 630), Color.White);
            btn3.Draw(spriteBatch, "Options", Menufont, new Vector2(450, 660), Color.White);
            btn4.Draw(spriteBatch, "Exit", Menufont, new Vector2(450, 690), Color.White);
        }

        private void States(SpriteBatch spriteBatch)
        {
            switch (states)
            {
                case 0: //PressStart
                        btn1.Draw(spriteBatch, "Press ENTER", Startfont, new Vector2(450, 600), Color.White);
                    break;

                case 1: //PressStartAnimation
                    btn1.FadeSpeed = 10.0f;
                    if (currentTime <= countDuration)
                    {
                        counter++;
                        //Actions
                        
                        btn1.Draw(spriteBatch, "Press ENTER", Startfont, new Vector2(450, 600), Color.White);
                    }
                    if (counter >= limit)
                    {
                        counter = 0;
                        //Actions
                        btn1.FadeSpeed = 1.0f;
                        states = 2;
                        btn1.IsActive = true;
                        btn2.IsActive = true;
                        btn3.IsActive = true;
                        btn4.IsActive = true;
                    }
                    break;

                case 2: //MenuSelect btn 1
                    btn1.FadeSpeed = 5.0f;
                    btn1.IsAnimated = true;
                    btn2.IsAnimated = false;
                    btn3.IsAnimated = false;
                    btn4.IsAnimated = false;
                    DrawButtons(spriteBatch);
                    break;

                case 3: //MenuSelect btn 1
                    btn2.FadeSpeed = 5.0f;
                    btn1.IsAnimated = false;
                    btn2.IsAnimated = true;
                    btn3.IsAnimated = false;
                    btn4.IsAnimated = false;
                    DrawButtons(spriteBatch);
                    break;

                case 4: //Options
                    btn3.FadeSpeed = 5.0f;
                    btn1.IsAnimated = false;
                    btn2.IsAnimated = false;
                    btn3.IsAnimated = true;
                    btn4.IsAnimated = false;
                    DrawButtons(spriteBatch);
                    break;

                case 5: //MenuSelect btn 1
                    btn4.FadeSpeed = 5.0f;
                    btn1.IsAnimated = false;
                    btn2.IsAnimated = false;
                    btn3.IsAnimated = false;
                    btn4.IsAnimated = true;
                    DrawButtons(spriteBatch);
                    break;
            }
        }
    }
}
