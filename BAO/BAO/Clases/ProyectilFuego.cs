using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    class ProyectilFuego
    {
        private ContentManager content;
        public bool isPlayer;
        public bool Active;
        private int moveSpeed;
        public Rectangle colitionBox;
        private SpriteAnimation sprite;
        private Texture2D texture;
        private bool isLeft;
        public Vector2 position;
        private float Time;
        public bool colide = false;

        public void LoadContent(ContentManager content, int speed, bool derecha, Vector2 pos)
        {
            this.content = content;
            isLeft = derecha;
            texture = content.Load<Texture2D>("Sprites/Proyectil/FireProyectil");
            this.moveSpeed = speed;
            this.colitionBox = new Rectangle((int)pos.X, (int)pos.Y, 30, 30);
            position = pos;
            sprite = new SpriteAnimation();
            sprite.Initialize(texture, pos, 30, 30, 0, 2000, Color.White, 1.0f, true);
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

            if (this.Active && !colide)
            {
                sprite.Position = new Vector2(position.X, position.Y);
                colitionBox = new Rectangle((int)position.X, (int)position.Y, 24, 8);
            }
            else
            {
                colitionBox = new Rectangle(0, 0, 0, 0);
                sprite.Update(gameTime);
            }




        }

        public void Draw(SpriteBatch sprite)
        {
            if (this.sprite.Active && !colide)
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
            else
            {
                if (colide)
                {
                    this.sprite.Draw(sprite);
                }
            }

        }

        public void Muerte()
        {
            this.sprite.Active = true;
            this.Active = false;
            this.colide = true;
            SetColide();
            this.colitionBox = new Rectangle(0, 0, 0, 0);
        }

        private void SetColide()
        {
            texture = content.Load<Texture2D>("Sprites/DisposeKnife");
            this.sprite.Initialize(texture, sprite.Position, 50, 50, 30, 30, Color.White, 1.5f, false);
        }
    }
}
