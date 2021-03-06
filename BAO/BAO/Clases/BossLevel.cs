﻿using System;
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
    class BossLevel : GameScreen
    {
        XnaFunctionscs fun = new XnaFunctionscs();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 spritePos;
        Vector2 spriteEnemypos;
        Player player;
        Enemy enemy;
        List<Rectangle> listaObs;
        List<Rectangle> listaObsIzq;
        List<Suelo> suelos;
        Texture2D texturaObs;
        BackgroundAnimation fondo;
        int pito = 0;
        private THEWORLDTimer timerTW;
        private THEWORLDTimer timerCD;
        private InputManager anotherInput;
        private bool oneshote = true;
        private SoundEffect stab;
        private SoundEffect part1bgm;
        private SoundEffectInstance part1bgminstance;
        private SoundEffect part2bgm;
        private SoundEffectInstance part2bgminstance;
        private SoundEffect knifeCling;

        private SoundEffect cling;
        public List<ProyectilKnife> listaNKnives;
        private ProyectilKnife shoot;

        private EnemyDestuno Boss;



        private Gravedad gravedad;

        private DialogScreen dialog;
        public override void LoadContent(ContentManager content)
        {
            gravedad=new Gravedad();
            stab = content.Load<SoundEffect>("Knife Stab Sound Effect");
            part1bgm = content.Load<SoundEffect>("Destined Cruz");
            part2bgm = content.Load<SoundEffect>("Bloody Tears");
            part1bgminstance = part1bgm.CreateInstance();
            part2bgminstance = part2bgm.CreateInstance();
            base.LoadContent(content);
            inputManager = new InputManager();
            Boss = new EnemyDestuno();
            timerCD = new THEWORLDTimer();
            timerCD.LoadContent(content, Vector2.Zero);
            timerTW = new THEWORLDTimer();
            timerTW.LoadContent(content, new Vector2(400, 0));
            knifeCling = content.Load<SoundEffect>("knifeCling");
            anotherInput = new InputManager();
            listaNKnives = new List<ProyectilKnife>();
            suelos = new List<Suelo>();
            fondo = new BackgroundAnimation("Background/background resized");
            spritePos = new Vector2(500, 500);
            fondo.LoadContent(content);
            dialog = new DialogScreen();
            listaObs = new List<Rectangle>();
            listaObs.Add(new Rectangle(0, 0, 0, 1200));
            listaObsIzq = new List<Rectangle>();
            listaObsIzq.Add(new Rectangle(1024, 0 , 0 , 1200));
            Suelo suelo = new Suelo("Stone3", content);
            Suelo suelo2 = new Suelo("Stone3", content);
            Suelo suelo3 = new Suelo("Stone3", content);
            Suelo suelo4 = new Suelo("Stone3", content);
            Suelo suelo5 = new Suelo("Stone3", content);
            Suelo suelo6 = new Suelo("Stone3", content);
            Suelo suelo7 = new Suelo("Stone3", content);
            suelo.rectangulo = new Rectangle(50, 200, 150, 50);
            suelo2.rectangulo = new Rectangle(150, 300, 150, 50);
            suelo3.rectangulo = new Rectangle(250, 400, 150, 50);
            suelo4.rectangulo = new Rectangle(700, 300, 150, 50);
            suelo5.rectangulo = new Rectangle(600, 400, 150, 50);
            suelo6.rectangulo = new Rectangle(0, 550, 1024, 800);
            suelo7.rectangulo = new Rectangle(800, 200, 150, 50);
            spriteEnemypos = new Vector2(600,100);
            suelos.Add(suelo);
            suelos.Add(suelo2);
            suelos.Add(suelo3);
            suelos.Add(suelo4);
            suelos.Add(suelo5);
            suelos.Add(suelo6);
            suelos.Add(suelo7);
            texturaObs = content.Load<Texture2D>("pisitoo");
            player = new Player();
            player.LoadContent(content, inputManager, spritePos);
            player.LoadContent(gravedad);
            player.playerR.Active = true;
            Boss.LoadContent(content, inputManager, new Vector2(750, 100));
            Boss.active = false;
            Boss.setList(listaNKnives);
            player.LoadContent(gravedad);


            string[,] dialogo = new string[,]
            {
                    {"Sprites/Frames/RichterFace", "Pepe", "Ugggghhhhhhhh", "Sounds/Voices/dialogoPepe1"},
                    {"Sprites/Frames/DeathFace", "??????", "Al fin nos encontramos... Centauro del Norte.", "Sounds/Voices/dialogoDestino1"},
                    {"Sprites/Frames/RichterFace", "Pepe (Centauro del Norte)", "Tú... ¡¿Quién eres?!...", "Sounds/Voices/dialogoPepe2"},
                    {"Sprites/Frames/DeathFace", "??????", "Al único al que has evadido durante todos estos años... Pepe...", "Sounds/Voices/dialogoDestino2"},
                    {"Sprites/Frames/RichterFace", "Pepe (Centauro del Norte)", "Tch", "Sounds/Voices/dialogoPepe3"},
                    {"Sprites/Frames/DeathFace", "??????", "¡Así es! ¡PEPE! ¡Soy el Destino!", "Sounds/Voices/dialogoDestino3"},
                    {"Sprites/Frames/RichterFace", "Pepe (Centauro del Norte)", "Y qué es lo que quieres...", "Sounds/Voices/dialogoPepe4"},
                    {"Sprites/Frames/DeathFace", "Destino", "Nada importante... Centauro... Tu alma", "Sounds/Voices/dialogoDestino4_5"},
                    {"Sprites/Frames/RichterFace", "Pepe (Centauro del Norte)", "¡Ja! No lo creo 'Destino'. Si me disculpas. Tengo cosas que hacer, lugares a donde ir... Gente que asesinar.",  "Sounds/Voices/dialogoPepe5"},
                    {"Sprites/Frames/DeathFace", "Destino", "No lo has entendido Pepe... ¡Tu viaje terminó ahora y para siempre!",  "Sounds/Voices/dialogoDestino6"},

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
                    DispararCuchillo(15, player.isLeft, new Vector2(spritePos.X, spritePos.Y + 10));
                }
                else
                {
                    DispararCuchillo(15, player.isLeft, new Vector2(spritePos.X, spritePos.Y - 25));
                }


            }

            if (!dialog.Active)
            {
                if (Boss.state<3)
                {
                part1bgminstance.Volume = 0.5f;
                part1bgminstance.Play();
                }
                
                Boss.active = true;
                spritePos = player.Update(gameTime, inputManager, spritePos);
                spriteEnemypos = Boss.Update(gameTime, inputManager, spriteEnemypos);
                if (spritePos.X - Boss.position.X <0)
                {
                    Boss.isLeft = true;
                }
                else
                {
                    Boss.isLeft = false;
                }
                
            }

            if (oneshote)
            {
                oneshote = false;
                player.Update(gameTime, inputManager, spritePos);
            }

            

            Boss.Update(player.TheWorldTime, spriteEnemypos, spritePos);

            if (Boss.state == 6 )
            {
                part2bgminstance.Dispose();
            }

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
                    if (spritePos.Y >= sueloInter.Y && gravedad.moveSpeed.Y >= 0.0f)
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

            foreach (var VARIABLE in listaNKnives)
            {
                if (VARIABLE.isPlayer && VARIABLE.colitionBox.Intersects(Boss.ColissionBox))
                {
                    Boss.IsHitted(player.damage);
                    stab.Play(1, 1, 0);
                    VARIABLE.Muerte();
                }
            }

            UpdateCuchillos(player.TheWorldTime);
            Timer(gameTime);
            dialog.Update(gameTime, inputManager);
            DisposeCuchillos();

            if (Boss.zawarudo)
            {
                player.ZaWarudoActive = true;
            }

            if (Boss.state == 3)
            {
                part1bgminstance.Dispose();
            }

            if (Boss.state == 4)
            {
                part2bgminstance.Play();
            }

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

            
            Boss.Draw(spriteBatch, spriteEnemypos);
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
                    if (VARIABLE.colitionBox.Intersects(VARIABLE2.colitionBox) && !VARIABLE.Equals(VARIABLE2))
                    {
                        if (VARIABLE.isPlayer && !VARIABLE2.isPlayer)
                        {
                            knifeCling.Play(0.5f, 0, 0);
                            VARIABLE.Muerte();
                            VARIABLE2.Muerte();
                        }
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
            shoot.LoadContent(this.content, speed, left, pos, new Vector2(24, 8), "knife", 10, 1.5f, new Vector2(16, 8));
            shoot.isPlayer = true;
            listaNKnives.Add(shoot);
        }
    }
}
