using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace BAO.Clases
{
    public class Player : Entity
    {

        private bool isLeft;

        
        public SpriteAnimation playerL;
        public SpriteAnimation playerR;
        public SpriteAnimation playerStandR;
        public SpriteAnimation playerStandL;
        public SpriteAnimation playerDuckR;

        private Texture2D moveRight;
        private Texture2D moveLeft;
        private Texture2D standL;
        private Texture2D standR;
        private Texture2D duckR;
        private int state;

        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            moveRight = content.Load<Texture2D>("pWalkR");
            moveLeft = content.Load<Texture2D>("pWalkL");
            standL = content.Load<Texture2D>("pStandL");
            standR = content.Load<Texture2D>("pStandR");
            duckR = content.Load<Texture2D>("pDuckR");
            this.playerL = new SpriteAnimation();
            this.playerR = new SpriteAnimation();
            this.playerStandR = new SpriteAnimation();
            this.playerStandL = new SpriteAnimation();
            this.playerDuckR = new SpriteAnimation();
            base.LoadContent(content, input, pos);
            this.position = pos;
            health = 100;
            damage = 10;
            moveSpeed.X = 5;
            isLeft = false;
            ColissionBox = new Rectangle(0,0,32,50);
            playerStandR.Initialize(standR, position, 32, 50, 2, 300, Color.White, 2.0f, true);
            playerStandL.Initialize(standL, position, 32, 50, 2, 150, Color.White, 2.0f, true);
            playerL.Initialize(moveRight, position, 32, 50, 8, 100, Color.White, 2.0f, true);
            playerR.Initialize(moveLeft, position, 32, 50, 8, 100, Color.White, 2.0f, true);
            playerDuckR.Initialize(duckR, position, 32, 50, 2, 100, Color.White, 2.0f, false);
            hasJumped = false;
            sprite = playerStandR;

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 position)
        {
            sprite.Active = true;

            if (inputManag.KeyDown(Keys.Right, Keys.D))
            {
                sprite = playerL;
                isLeft = false;
                sprite.Reverse = false;
                position.X += moveSpeed.X;
                sprite.Active = true;
                sprite.Ended = false;
                sprite.Looping = true;
                sprite.Position = new Vector2(position.X, position.Y);
            }
            else
            {
                if (inputManag.KeyDown(Keys.Left, Keys.A))
                {
                    sprite = playerR;
                    isLeft = true;
                    sprite.Reverse = false;
                    position.X -= moveSpeed.X;
                    sprite.Active = true;
                    sprite.Ended = false;
                    sprite.Looping = true;
                    sprite.Position = new Vector2(position.X, position.Y);
                }
                else
                {
                    if (inputManag.KeyDown(Keys.Down, Keys.S))
                    {

                        if (isLeft)
                        {
                            
                        }
                        else
                        {
                            sprite = playerDuckR;


                        }
                        sprite.Reverse = false;
                        sprite.Looping = true;
                        sprite.Active = true;
                        sprite.Ended = true;
                        sprite.Position = new Vector2(position.X, position.Y);
                    }
                    else
                    {

                        if (inputManag.KeyReleased(Keys.S))
                        {
                            sprite.duck = true;
                        }

                        if (isLeft)
                        {
                            sprite = playerStandL;

                        }
                        else
                        {
                            sprite = playerStandR;

                        }

                        sprite.Ended = false;
                        sprite.Reverse = true;
                        sprite.Active = true;
                        sprite.Looping = true;
                        sprite.Position = new Vector2(position.X, position.Y);

                    }
                }
                
            }



                     
            sprite.Update(gameTime);
            this.colissionBox = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, 28, 50);
            inputManag.Update();
            float i = 1;
            if (hasJumped==false)
            {
            moveSpeed.Y = 5.25f;
            }

            position.Y = position.Y + moveSpeed.Y;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 1f;
                moveSpeed.Y = -6.25f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                moveSpeed.Y += 0.25f * i;
                position.Y = position.Y + moveSpeed.Y;
            }

            if (position.Y + 50 >= position.Y + 80)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                moveSpeed.Y = 0f;
            }
            return position;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
