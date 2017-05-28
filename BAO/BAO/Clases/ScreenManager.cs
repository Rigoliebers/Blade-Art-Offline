using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class ScreenManager
    {
#region Variables

        private ContentManager content;
        private GameScreen currentScreen;
        private GameScreen newScreen;

        /// <summary>
        /// ScreenManager Instance
        /// </summary>
       
        private static ScreenManager instance;

        /// <summary>
        /// Screen Stack
        /// </summary>

        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        /// <summary>
        /// Screens width and height
        /// </summary>

         Vector2 dimensions;

        #endregion


        public void AddScreen(GameScreen screen)
        {
            transition = true;
            fade.IsActive = true;
            fade.Alpha = 0.0f;
            fade.ActivateValue = 1.0f;
            newScreen = screen;
        }



        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        private bool transition;

        private FadeAnimation fade;

        private Texture2D fadeTexture;







        public void Initialize()
        {
            currentScreen = new SplashScreen();
            fade = new FadeAnimation();
        }

        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content);

            fadeTexture = content.Load<Texture2D>("pixel");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = dimensions.X;
        }

        public void Update(GameTime gameTime)
        {
            if (!transition)
            {
                currentScreen.Update(gameTime);
            }
            else
            {
                Transition(gameTime);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
            {
                fade.Draw(spriteBatch);
            }
        }

        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);
            if ((fade.Alpha == 1.0f) && (fade.Timer.TotalSeconds == 1.0f))
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content);
            }
            else if (fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }
    }
}
