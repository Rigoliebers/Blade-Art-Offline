using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class ProyectilKnife
    {

        public bool Active;
        private int moveSpeed;
        public Rectangle colitionBox;
        private SpriteAnimation sprite;
        private Texture2D texture;
        private bool isLeft;
        public Vector2 position;
        private float Time;

        public void LoadContent(ContentManager content, int speed, bool derecha, Vector2 pos)
        {
            isLeft = derecha;
            texture = content.Load<Texture2D>("knife");
            this.moveSpeed = speed;
            this.colitionBox = new Rectangle((int)pos.X,(int)pos.Y,5,24);
            position = pos;
            sprite = new SpriteAnimation();
            sprite.Initialize(texture, pos, 5,17,0,2000, Color.White, 1.0f,true);
            Time = 0;
            sprite.Active = true;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
                Time = gameTime.ElapsedGameTime.Seconds;


                if (gameTime.ElapsedGameTime.Milliseconds != 0)
                {
                    if (isLeft)
                    {
                        position.X -= moveSpeed;
                    }
                    else
                    {
                        position.X += moveSpeed;
                    }
                }

            if (this.Active)
            {
                sprite.Position = new Vector2(position.X, position.Y);
                colitionBox = new Rectangle((int)position.X, (int)position.Y, 5, 24);
            }
            else
            {
                colitionBox = new Rectangle(0,0,0,0);
            }

            


        }

        public void Draw(SpriteBatch sprite)
        {
            if (this.sprite.Active)
            {
                if (isLeft)
                {
                    sprite.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    sprite.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                }
            }

        }

        public void Muerte()
        {
            this.sprite.Active = false;
            this.Active = false;
        }
    }
}
