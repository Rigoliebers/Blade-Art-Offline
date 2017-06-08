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
        Vector2 spritePosEnemy;
        Player player;
        List<Rectangle> listaObs;
        List<Rectangle> listaObsIzq;
        List<Suelo> suelos;
        Texture2D texturaObs;
        BackgroundAnimation fondo;
        ///Suelo objSuelo = new Suelo();
        int pito = 0;
        private THEWORLDTimer timerTW;
        private THEWORLDTimer timerCD;
        private InputManager anotherInput;
        public Gravedad gravedad = new Gravedad();
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
            fondo = new BackgroundAnimation("Background/background oreimo");
            // TODO: use this.Content to load your game content here
            spritePos = new Vector2(10, 780);
            fondo.LoadContent(content);
             base.LoadContent(content);
            dialog = new DialogScreen();
            listaObs = new List<Rectangle>();
            listaObs.Add(new Rectangle(0, 0, 0, 1200));
            listaObsIzq = new List<Rectangle>();
            listaObsIzq.Add(new Rectangle(1024, 0, 0, 350));
            listaObsIzq.Add(new Rectangle(1024, 600, 0, 700));
            #region suelos
            Suelo suelo = new Suelo("Stone1", content);
            Suelo suelo2 = new Suelo("Stone1", content);
            Suelo suelo3 = new Suelo("Stone1", content);
            Suelo suelo4 = new Suelo("Stone1", content);
            Suelo suelo5 = new Suelo("Stone1", content);
            Suelo suelo6 = new Suelo("Stone1", content);
            Suelo suelo7 = new Suelo("Stone1", content);
            Suelo suelo8 = new Suelo("Stone1", content);
            Suelo suelo9 = new Suelo("Stone1", content);
            Suelo suelo10 = new Suelo("Stone1", content);
            Suelo suelo11= new Suelo("Stone1", content);
            Suelo suelo12= new Suelo("Stone1", content);
            Suelo suelo13= new Suelo("Stone1", content);
            suelo10.rectangulo = new Rectangle(880, 300, 200, 50);
            suelo9.rectangulo = new Rectangle(940, 490, 250, 50);
            suelo8.rectangulo = new Rectangle(550, 220, 40, 50);
            suelo7.rectangulo = new Rectangle(320, 50, 100, 50);
            suelo6.rectangulo = new Rectangle(0, 100, 100, 50);
            suelo5.rectangulo = new Rectangle(0, 250, 100, 50);
            suelo4.rectangulo = new Rectangle(0, 400, 100 , 50);
            suelo3.rectangulo = new Rectangle(320, 620, 100, 50);
            suelo2.rectangulo = new Rectangle(0, 550, 250, 50);
            suelo.rectangulo = new Rectangle(0, 780, 200, 50);
            suelos.Add(suelo8);
            suelos.Add(suelo2);
            suelos.Add(suelo3);
            suelos.Add(suelo4);
            suelos.Add(suelo5);
            suelos.Add(suelo6);
            suelos.Add(suelo7);
            suelos.Add(suelo);
            suelos.Add(suelo9);
            suelos.Add(suelo10);
            #endregion
            texturaObs = content.Load<Texture2D>("Stone3");
            player = new Player();
            player.LoadContent(content, inputManager, spritePos);
            player.LoadContent(gravedad);
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
                if (player.isAgachado)
                {
                    DispararCuchillo(15, player.isLeft, new Vector2(spritePos.X, spritePos.Y +10));
                }
                else
                {
                    DispararCuchillo(15, player.isLeft, new Vector2(spritePos.X, spritePos.Y - 25));
                }


            }


            if (!dialog.Active)
                spritePos = player.Update(gameTime, inputManager, spritePos);



            foreach (Rectangle recto in listaObs)
            {
                if (recto.Intersects(player.ColissionBox))
                {
                    spritePos.X = spritePos.X + player.moveSpeed.X;
                    break;
                }

            }

            foreach (Rectangle recto in listaObsIzq)
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
                sueloInter.Y = sueloInter.Y - 40;
                if (sueloInter.Intersects(player.ColissionBox))
                {
                    if (spritePos.Y >= sueloInter.Y  && gravedad.moveSpeed.Y >= 0.0f)
                    {
                        spritePos.Y = sueloInter.Y;
                         gravedad.hasJumped = false;
                        gravedad.caida = false;
                    }
                    break;
                }
                else
                {
                    gravedad.caida = true;
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
           /* foreach (Rectangle item in listaObs)
            {
                spriteBatch.Draw(texturaObs, item, Color.White);
                
            }*/

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
