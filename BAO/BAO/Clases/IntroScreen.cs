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
    public class IntroScreen : GameScreen
    {

        private DialogScreen dialog;
        int r1x;
        int r2x;
        int velocidad;
        int width = 1024;
        int height = 800;
        Texture2D escena;
        Rectangle rectangle1;
        Rectangle rectangle2;
        private SoundEffect BGM;
        private SoundEffectInstance BGMInstance;

        public override void LoadContent(ContentManager content)
        {
            //GraphicsDevice obj = fun.GraphicsDevice;
            //spriteBatch = new SpriteBatch(obj);

            // TODO: use this.Content to load your game content here

            r1x = 0;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            r2x = r1x + rectangle1.Width;
            rectangle2 = new Rectangle(r2x, 0, width, height);
            velocidad = 3;
            //playerAnimation = new Animation();
            base.LoadContent(content);

            BGM = content.Load<SoundEffect>("introbgm");
            BGMInstance = BGM.CreateInstance();

            BGMInstance.Volume = 0.75f;
            BGMInstance.Play();

            escena = content.Load<Texture2D>("Background/Background2");
            dialog = new DialogScreen();
            string[,] dialogo = new string[,]
            {
                    {"monito0", "El Vigilante", "¿Conoces la historia de 'Pepe y los globos'?", "Sounds/Voices/nada"},
                    {"Ishtar", "Ishtar", "No. ¿Quíen es él? Vigilante", "Sounds/Voices/nada2"},
                    {"monito0", "El Vigilante", "Casi nadie lo conoce ya... Este hombre, Pepe, realizó una de las más grandes hazañas que he presenciado en todo el espacio tiempo." , "Sounds/Voices/nada"},
                    {"monito0", "El Vigilante", "¿Te gustaría oirla?", "Sounds/Voices/nada2"},
                    {"Ishtar", "Ishtar", "Por supuesto. Vigilante.", "Sounds/Voices/nada"},
                    {"monito0", "El Vigilante", "Bien, bien. Perfecto entonces...", "Sounds/Voices/nada2"},
                    {"monito0", "El Vigilante", "Te contare la historia de un hombre que desafio al propio destino y en una batalla cruenta, y lo derrotó. Un hombre que lo dio todo a cambio de nada. Él más grande héroe que ha nacido.", "Sounds/Voices/nada"},
                    {"monito0", "El Vigilante", "Pepe Juan Panfiloponcio de la Santa Cruz Rodrigues Ademán de la Trinidad de la Ascencion de la divina iglesia de la santa paz del niño Jésus de Asís", "Sounds/Voices/nada2"},
                    {"Ishtar", "Ishtar", "Estoy ansiosa...", "Sounds/Voices/nada"},
            };


            dialog.LoadContent(content, dialogo);

        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

            r1x -= velocidad;
            r2x -= velocidad;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            rectangle2 = new Rectangle(r2x, 0, width, height);

            if (rectangle1.X <= -width)
            {
                r1x = 0;
            }
            if (rectangle2.X <= 0)
            {
                r2x = width;
            }

            if (!dialog.Active)
            {
                BGMInstance.Stop();
                ScreenManager.Instance.AddScreen(new GameplayScreen(), 0.6f);
            }

            inputManager.Update();
            dialog.Update2(gameTime, inputManager);





        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(escena, rectangle1, Color.White);
            spriteBatch.Draw(escena, rectangle2, Color.White);
            dialog.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
