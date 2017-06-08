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
        private Vector2 image;
        private float scale;
        private string sprait;

        public void LoadContent(ContentManager content, int speed, bool derecha, Vector2 pos, Vector2 colition, string image, int damage, float scale, Vector2 spritevector)
        {
            this.scale = scale;
            this.image = colition;
            sprait = image;
            this.content = content;
            isLeft = derecha;
            texture = content.Load<Texture2D>(image);
            this.moveSpeed = speed;
            this.colitionBox = new Rectangle((int)pos.X,(int)pos.Y,(int)colition.X, (int)colition.Y);
            position = pos;
            sprite = new SpriteAnimation();
            sprite.Initialize(texture, pos,(int)spritevector.X ,(int)spritevector.Y,0,2000, Color.White, scale,true);
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
                colitionBox = new Rectangle((int)position.X, (int)position.Y, (int)image.X, (int)image.Y);
            }
            else
            {
                colitionBox = new Rectangle(0,0,0,0);
                sprite.Update(gameTime);
            }

            


        }

        public void Draw(SpriteBatch sprite)
        {
            if (this.sprite.Active && !colide)
            {
                if (isLeft)
                {
                    sprite.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    sprite.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
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
            this.colitionBox = new Rectangle(0,0,0,0);
        }

        private void SetColide()
        {
            if (sprait.Equals("knife") )
            {
                texture = content.Load<Texture2D>("Sprites/DisposeKnife");
                this.sprite.Initialize(texture, sprite.Position, 50, 50, 6, 20, Color.White, 1.5f, false);
            }
            else
            {
                
            }
            
        }
    }
}
