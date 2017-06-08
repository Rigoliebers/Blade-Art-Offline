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


        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
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

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, Vector2 pos)
        {
            if (isLeft)
            {
                sprite = StandL;
            }
            else
            {
                sprite = StandR;
            }
            

            sprite.Active = true;


            if (health >800)
            {
                state = 0;
            }
            if (active)
            {
                switch (state)
                {
                    case (0):
                        state1(gameTime, pos);
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

        public void state1(GameTime gameTime, Vector2 pos) //Idle
        {
            if (position.X > 600)
            {
                position.X -= 0.4f;
            }
            if (position.Y < 200)
            {
                position.Y += 0.3f;
            }

            if (position.X > 600 && position.Y < 200)
            {
                state = 1;
            }
        }

        public void IsHitted(int hit)
        {
            health += hit;
        }

    }
}
