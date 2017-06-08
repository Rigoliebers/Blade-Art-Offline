using System;
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

        private int elapsedtime;
        public int state;

        public bool active;

        public bool isLeft;

        private SpriteAnimation StandL;
        private SpriteAnimation StandR;

        private Texture2D standLeft;
        private Texture2D standRight;

        private ProyectilKnife fuego;

        public List<ProyectilKnife> listaNKnives;

        private ContentManager contentmanager;
        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            this.contentmanager = content;

            health = 1000;
            ColissionBox = new Rectangle(0,0,67,80);
            damage = 10;
            moveSpeed = new Vector2(15,10);
            position = pos;
            standLeft = content.Load<Texture2D>("Sprites/Enemies/DestinyStandL");
            standRight = content.Load<Texture2D>("Sprites/Enemies/DestinyStandR");

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

        public override void Update(GameTime gameTime, Vector2 pos)
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
                }
            }

            
            ColissionBox = new Rectangle((int)position.X, (int)position.Y-80, 134, 160);

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
        }

        public void state0(GameTime gameTime, Vector2 pos) //Idle
        {
            if (position.X > 600)
            {
                position.X -= 0.4f;
            }
            if (position.Y < 200)
            {
                position.Y += 0.3f;
            }


            if (position.X > 599 && position.X < 601 && position.Y >= 199 && position.Y <= 201)
            {
                state = 1;
            }
        }

        public void state1(GameTime gameTime, Vector2 pos)
        {
            Random x = new Random();
            int y = x.Next(0, 3);


            if (elapsedtime > 200)
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
                        Fire(new Vector2(position.X, position.Y -50));
                        elapsedtime = 0;
                        break;
                }
            }

            
        }

        public void IsHitted(int hit)
        {
            health += hit;
        }

        public void Fire(Vector2 position)
        {
            fuego = new ProyectilKnife();
            fuego.LoadContent(contentmanager, 3, isLeft, position, new Vector2(30, 30), "Sprites/Proyectil/FireProyectil", 10, 1.0f, new Vector2(30, 30));
            fuego.isPlayer = false;
            listaNKnives.Add(fuego);
        }

    }
}
