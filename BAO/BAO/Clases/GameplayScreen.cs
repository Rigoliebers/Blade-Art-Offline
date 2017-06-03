using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace BAO.Clases
{
    public class GameplayScreen : GameScreen
    {
        XnaFunctionscs fun = new XnaFunctionscs();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 spritePos;
        Player player;
        List<Rectangle> listaObs;
        List<Suelo> suelos;
        Texture2D texturaObs;
        BackgroundAnimation fondo;
        ///Suelo objSuelo = new Suelo();
        int pito = 0;
        private THEWORLDTimer timerTW;
        private THEWORLDTimer timerCD;
        private InputManager anotherInput;

        private SoundEffect knifeCling;

        private SoundEffect cling;
        public List<ProyectilKnife> listaNKnives;
        private ProyectilKnife shoot;


        private DialogScreen dialog;
        public override void LoadContent(ContentManager content)
        {
            knifeCling = content.Load<SoundEffect>("knifeCling");
            anotherInput = new InputManager();
            listaNKnives = new List<ProyectilKnife>();
            timerCD = new THEWORLDTimer();
            timerCD.LoadContent(content, Vector2.Zero);
            timerTW = new THEWORLDTimer();
            timerTW.LoadContent(content, new Vector2(400,0));
            suelos = new List<Suelo>();
            fondo = new BackgroundAnimation("Background/background resized");
            // TODO: use this.Content to load your game content here
            spritePos = new Vector2(500, 500);
            fondo.LoadContent(content);
             base.LoadContent(content);
            dialog = new DialogScreen();
            listaObs = new List<Rectangle>();
            listaObs.Add(new Rectangle(900, 400, 400, 200));
            Suelo pito = new Suelo("pisitoo", content);
            Suelo pito2 = new Suelo("pisitoo", content);
            pito2.rectangulo = new Rectangle(20, 550, 400, 20);
            pito.rectangulo = new Rectangle(350, 650, 400, 20);
            suelos.Add(pito);
            suelos.Add(pito2);
            texturaObs = content.Load<Texture2D>("pisitoo");
            player = new Player();
            player.LoadContent(content, inputManager, spritePos);
            player.playerR.Active = true;

            string[,] dialogo = new string[,]
            {
                    {"monito0", "El", "Mi pito amigo"},
                    {"monito0", "El", "2"},
                    {"monito0", "El", "3"},
              
                
            };

            dialog.LoadContent(content, dialogo);
        }

        public override void UnloadContent()
        {
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            anotherInput.Update();
            fondo.Update(gameTime);

            if (anotherInput.KeyPressed(Keys.Z) && !dialog.Active)
            {
                DispararCuchillo(15, player.isLeft, spritePos);

            }

            if (!dialog.Active)
                spritePos = player.Update(gameTime, inputManager, spritePos);



            foreach (Rectangle recto in listaObs)
            {
                if (recto.Intersects(player.ColissionBox))
                {
                    spritePos.X = spritePos.X - player.moveSpeed.X;                  
                    break;
                }
                
            }
            foreach (Suelo recto in suelos)
            {
                Rectangle sueloInter = recto.rectangulo;
                if (sueloInter.Intersects(player.ColissionBox))
                {
                    if (spritePos.Y >= sueloInter.Y-40)
                    {
                        spritePos.Y = sueloInter.Y-40;
                        player.hasJumped = false;
                    }
                    break;
                }

            }


            UpdateCuchillos(player.TheWorldTime);
            Timer(gameTime);
            dialog.Update(gameTime, inputManager);
            DisposeCuchillos();





        }

        public void UpdateCuchillos(GameTime gameTime)
        {
            foreach (var VARIABLE in listaNKnives)
            {
                VARIABLE.Update(gameTime);
                ColisionCuchillos();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            fondo.Draw(spriteBatch);
            foreach (Rectangle item in listaObs)
            {
                spriteBatch.Draw(texturaObs, item, Color.White);
                
            }

            foreach (Suelo item in suelos)
            {
                item.Draw(spriteBatch, item.rectangulo, item.texturaObs);
            }
            foreach (var VARIABLE in listaNKnives)
            {
                VARIABLE.Draw(spriteBatch);
            }

            player.Draw(spriteBatch, spritePos);
            dialog.Draw(spriteBatch);
            timerCD.Draw(spriteBatch);
            timerTW.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        private void ColisionCuchillos()
        {
            foreach (var VARIABLE in listaNKnives)
            {
                foreach (var VARIABLE2 in listaNKnives)
                {
                    if (VARIABLE.colitionBox.Intersects(VARIABLE2.colitionBox) && !VARIABLE.Equals(VARIABLE2) )
                    {

                        knifeCling.Play(0.5f, 0, 0);
                        VARIABLE.Muerte();
                        VARIABLE2.Muerte();

                        //if (!VARIABLE.isPlayer && !VARIABLE2.isPlayer)
                        //{
                        //    knifeCling.Play(0.5f, 0, 0);
                        //    VARIABLE.Muerte();
                        //    VARIABLE2.Muerte();
                        //}
                    }
                }
            }
        }

        private void DisposeCuchillos()
        {
            listaNKnives.RemoveAll(ProyectilKnife => ProyectilKnife.position.X > 1024 || ProyectilKnife.position.X < 0);
        }

        private void Timer(GameTime gameTime)
        {
            if (player.TheWorld)
            {
                timerTW.Active = true;
                timerTW.Update(gameTime);
                if (timerTW.miliseconds == 0)
                {
                    player.Tick.Play(1.0f, 0, 0);
                }
            }
            else
            {
                timerTW.Active = false;
                timerTW.Reset();
            }
            if (player.CD)
            {
                timerCD.Active = true;
                timerCD.Update(gameTime);
            }
            else
            {
                timerCD.Active = false;
                timerCD.Reset();

            }
        }

        private void DispararCuchillo(int speed, bool left, Vector2 pos)
        {
            shoot = new ProyectilKnife();
            shoot.LoadContent(this.content, speed, left, pos);
            shoot.isPlayer = true;
            listaNKnives.Add(shoot);
        }
    }
}
