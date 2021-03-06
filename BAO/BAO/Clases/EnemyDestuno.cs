﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class EnemyDestuno : Entity
    {
        private InputManager inputo;
        private bool movingx = false;
        private bool movingy = false;
        public bool zawarudo = false;
        public bool zawarudocreated = false;
        public bool zawarudomust = false;

        private SpriteFont font;

        private int anothertime;
        private int elapsedtime;
        public int state;

        public bool active;

        public bool isLeft;

        private SpriteAnimation StandL;
        private SpriteAnimation StandR;

        private Texture2D standLeft;
        private Texture2D standRight;
        private Texture2D item;

        private ProyectilKnife fuego;

        private bool fire;

        public List<ProyectilKnife> listaNKnives;

        

        private ContentManager contentmanager;
        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            font = content.Load<SpriteFont>("Agency24");
            this.contentmanager = content;
            ColissionBox = new Rectangle((int)pos.X, (int)pos.Y, 138,160);
            health = 1000;
            ColissionBox = new Rectangle(0,0,67,80);
            damage = 10;
            moveSpeed = new Vector2(15,10);
            position = pos;
            standLeft = content.Load<Texture2D>("Sprites/Enemies/DestinyStandL");
            standRight = content.Load<Texture2D>("Sprites/Enemies/DestinyStandR");
            item = content.Load<Texture2D>("Zawarudoitem");

            StandL = new SpriteAnimation();
            StandR = new SpriteAnimation();

            isLeft = true;

            inputo = input;

            StandL.Initialize(standLeft, pos, 69, 80, 1, 8000, Color.White, 2.0f, true);
            StandR.Initialize(standRight, pos, 69, 80, 1, 8000, Color.White, 2.0f, true);

            sprite = StandL;
        }

        public void setList(List<ProyectilKnife> lista)
        {
            this.listaNKnives = lista;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, Vector2 pos, Vector2 playerpos)
        {



            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;


            if (isLeft)
            {
                sprite = StandL;
            }
            else
            {
                sprite = StandR;
            }
            

            sprite.Active = true;


            
            if (active)
            {
                switch (state)
                {
                    case (0):
                        state0(gameTime, pos);
                        break;
                    case (1):
                        state1(gameTime, pos);
                        break;
                    case (2):
                        state2(gameTime, pos, playerpos);
                        break;
                    case (3):
                        state3(gameTime, pos, playerpos);
                        break;
                    case (4):
                        state4(gameTime, pos, playerpos);
                        break;
                    case (5):
                        state5(gameTime, pos, playerpos);
                        break;
                    case (6):
                        state6();
                        break;

                }
            }


            colissionBox.X = (int) position.X - 50;
            colissionBox.Y = (int) position.Y - 80;
            colissionBox.Width = 80;
            colissionBox.Height = 160;

            sprite.Ended = false;
            sprite.Reverse = false;
            sprite.Active = true;
            sprite.Looping = true;
            
            sprite.Position = new Vector2(position.X, position.Y);
            sprite.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            sprite.Draw(spriteBatch);

            if (zawarudomust)
            {
                spriteBatch.Draw(item, new Rectangle(560, 400, 30,30),Color.White);
                zawarudocreated = true;
            }
            if (zawarudo)
            {
                spriteBatch.DrawString(font, "Presiona X PEPE", new Vector2(520, 100),Color.White);
            }
        }

        public void state0(GameTime gameTime, Vector2 pos) //Idle
        {
            if (position.X > 512)
            {
                position.X -= 0.5f;
            }
            if (position.Y < 200)
            {
                position.Y += 0.3f;
            }

            colissionBox.X = (int)position.X - 50;
            colissionBox.Y = (int)position.Y - 80;
            colissionBox.Width = 0;
            colissionBox.Height = 0;

            if (position.X > 511 && position.X < 513 && position.Y >= 199 && position.Y <= 201)
            {
                state = 1;
            }
        }

        public void state1(GameTime gameTime, Vector2 pos)
        {
            Random x = new Random();
            int y = x.Next(0, 5);

            if (elapsedtime > 500)
            {
                switch (y)
                {
                    case 0:
                        Fire(new Vector2(position.X, position.Y + 50));
                        elapsedtime = 0;
                        break;

                    case 1:
                        Fire(new Vector2(position.X, position.Y));
                        elapsedtime = 0;
                        break;

                    case 2:
                        Fire(new Vector2(position.X, position.Y +65));
                        elapsedtime = 0;
                        break;

                    case 3:
                        Fire(new Vector2(position.X, position.Y - 50));
                        elapsedtime = 0;
                        break;

                    case 4:
                        Fire(new Vector2(position.X, position.Y - 65));
                        elapsedtime = 0;
                        break;

                }
            }

            if (health < 800)
            {
                state = 2;
            }

            
        }

        public void state2(GameTime gameTime, Vector2 pos, Vector2 playerpos)
        {
            Random x = new Random();
            int y = x.Next(0, 5);


            if (position.X > 300 && !movingx)
            {
                position.X += 1.0f;
                if (position.X > 795 && position.X <= 805)
                    movingx = true;
            }

            if (position.X < 800 && movingx)
            {
                position.X -= 1.0f;
                if (position.X > 295 && position.X <= 305)
                    movingx = false;
            }

            if (position.Y > 100 && !movingy)
            {
                position.Y += 2.0f;
               
                    if (position.Y > 395 && position.Y <= 405)
                        movingy = true;
            }

            if (position.Y < 600 && movingy)
            {
                position.Y -= 2.0f;
                if (position.Y > 95 && position.Y <= 105)
                    movingy = false;
            }


            if (elapsedtime > 300)
            {
                FireAtWill(new Vector2(position.X, position.Y), playerpos);
                elapsedtime = 0;
            }

            //if (anothertime > 4000)
            //{
            //    if (fire)
            //    {
            //        fire = false;
            //    }
            //    else
            //    {
            //        fire = true;
            //    }
            //    anothertime = 0;
            //}

            if (health < 500)
            {
                state = 3;
            }
        }


        public void state3(GameTime gameTime, Vector2 pos, Vector2 playerpos)
        {
            if (position.X > 512)
            {
                position.X -= 1f;
            }
            if (position.Y < 200)
            {
                position.Y += 1f;
            }

            colissionBox.X = (int)position.X - 50;
            colissionBox.Y = (int)position.Y - 80;
            colissionBox.Width = 0;
            colissionBox.Height = 0;

            if (position.X > 505 && position.X < 520 && position.Y >= 190)
            {
                if (zawarudo)
                {
                    state = 4;
                }
                else
                {
                    GetZaWarudo(playerpos);
                }
            }

        }

        public void state4(GameTime gameTime, Vector2 pos, Vector2 playerpos)
        {
            if (gameTime.ElapsedGameTime.Milliseconds != 0)
            {
                anothertime += gameTime.ElapsedGameTime.Milliseconds;

                if (position.X > 300 && !movingx)
                {
                    position.X += 2.0f;
                    if (position.X > 795 && position.X <= 805)
                        movingx = true;
                }

                if (position.X < 800 && movingx)
                {
                    position.X -= 2.0f;
                    if (position.X > 295 && position.X <= 305)
                        movingx = false;
                }

                if (position.Y > 100 && !movingy)
                {
                    position.Y += 3.0f;

                    if (position.Y > 395 && position.Y <= 405)
                        movingy = true;
                }

                if (position.Y < 600 && movingy)
                {
                    position.Y -= 3.0f;
                    if (position.Y > 95 && position.Y <= 105)
                        movingy = false;
                }


                if (elapsedtime > 50 && fire)
                {
                    FireAtWill(new Vector2(position.X, position.Y), playerpos);
                    elapsedtime = 0;
                }

                if (anothertime > 4000)
                {
                    if (fire)
                    {
                        fire = false;
                    }
                    else
                    {
                        fire = true;
                    }
                    anothertime = 0;
                }

                if (health < 300)
                {
                    state = 5;
                }
            }         
        }

        public void state5(GameTime gameTime, Vector2 pos, Vector2 playerpos)
        {
            if (gameTime.ElapsedGameTime.Milliseconds != 0)
            {
                anothertime += gameTime.ElapsedGameTime.Milliseconds;

                if (position.X > 300 && !movingx)
                {
                    position.X += 5.0f;
                    if (position.X > 795 && position.X <= 805)
                        movingx = true;
                }

                if (position.X < 800 && movingx)
                {
                    position.X -= 5.0f;
                    if (position.X > 295 && position.X <= 305)
                        movingx = false;
                }

                if (position.Y > 100 && !movingy)
                {
                    position.Y += 5.0f;

                    if (position.Y > 395 && position.Y <= 405)
                        movingy = true;
                }

                if (position.Y < 600 && movingy)
                {
                    position.Y -= 5.0f;
                    if (position.Y > 95 && position.Y <= 105)
                        movingy = false;
                }


                if (elapsedtime > 50 && fire)
                {
                    FireAtWill(new Vector2(position.X, position.Y), playerpos);
                    elapsedtime = 0;
                }

                if (anothertime > 3000)
                {
                    if (fire)
                    {
                        fire = false;
                    }
                    else
                    {
                        fire = true;
                    }
                    anothertime = 0;
                }

                if (health < 0)
                {
                    state = 6;
                }
            }
        }

        public void state6()
        {
            ScreenManager.Instance.AddScreen(new End(), 0.6f);
        }

        public void IsHitted(int hit)
        {
            health -= hit;
        }

        public void Fire(Vector2 position)
        {
            fuego = new ProyectilKnife();
            fuego.LoadContent(contentmanager, 3, isLeft, position, new Vector2(30, 30), "Sprites/Proyectil/FireProyectil", 10, 1.0f, new Vector2(30, 30));
            fuego.isPlayer = false;
            listaNKnives.Add(fuego);
        }

        public void FireAtWill(Vector2 position, Vector2 playerpos)
        {
            float difx = playerpos.X - position.X;
            float dify = playerpos.Y - position.Y;
            float M;

            if (Math.Abs(difx) > Math.Abs(dify))
            {
                M = difx;
                M = M/5;
            }
            else
            {
                M = dify;
                M = M/5;
            }

            fuego = new ProyectilKnife();
            fuego.speddo.X = Math.Abs(difx/M);
            fuego.speddo.Y = Math.Abs(dify/M);

            if (playerpos.Y < position.Y)
            {
                fuego.speddo.Y = fuego.speddo.Y*-1;
            }

            fuego.LoadContent(contentmanager, 3, isLeft, position, new Vector2(30, 30), "Sprites/Proyectil/FireProyectil", 10, 1.0f, new Vector2(30, 30));
            fuego.isHomming = true;
            fuego.finalpos = playerpos;
            fuego.isPlayer = false;
            listaNKnives.Add(fuego);
        }


        public void GetZaWarudo(Vector2 player)
        {
            zawarudomust = true;
            if (zawarudocreated && new Rectangle(560, 400, 30, 30).Intersects(new Rectangle((int)player.X, (int)player.Y, 100,100)))
            {
                zawarudo = true;
            }
        }
    }
}
