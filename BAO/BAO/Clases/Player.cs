using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace BAO.Clases
{
    public class Player : Entity
    {
        public bool isLeft;
        public bool isAgachado;
        public bool isShooting;
        public bool ZaWarudoActive;

        public SpriteAnimation playerL;
        public SpriteAnimation playerR;
        public SpriteAnimation playerStandR;
        public SpriteAnimation playerStandL;
        public SpriteAnimation playerDuckR;
        public SpriteAnimation playerDuckL;
        public SpriteAnimation playerShootR;
        public SpriteAnimation playerShootL;

        private SoundEffect BegoF;
        public SoundEffect Tick;
        private SoundEffect EndOf;
        public bool TheWorld;
        private double elapsedTime;
        private double anotherTimer;
        private ProyectilKnife shoot;
        private ContentManager content;
        public bool CD;
        private bool otro = true;
        public GameTime TheWorldTime;
        Gravedad gravedad = new Gravedad();
        private Texture2D moveRight;
        private Texture2D moveLeft;
        private Texture2D standL;
        private Texture2D standR;
        private Texture2D duckR;
        private Texture2D duckL;
        private Texture2D shootR;
        private Texture2D shootL;

        private int state;


        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            BegoF = content.Load<SoundEffect>("Za Warudo Sound Effect");
            Tick = content.Load<SoundEffect>("Tick");
            EndOf = content.Load<SoundEffect>("TheEnd");
            this.content = content;
            TheWorldTime = new GameTime();
            moveRight = content.Load<Texture2D>("pWalkR");
            moveLeft = content.Load<Texture2D>("pWalkL");
            standL = content.Load<Texture2D>("pStandL");
            standR = content.Load<Texture2D>("pStandR");
            duckR = content.Load<Texture2D>("pDuckR");
            duckL = content.Load<Texture2D>("pDuckL");
            shootR = content.Load<Texture2D>("Sprites/Player/playerShootR");
            shootL = content.Load<Texture2D>("Sprites/Player/playerShootL");
            this.playerL = new SpriteAnimation();
            this.playerR = new SpriteAnimation();
            this.playerStandR = new SpriteAnimation();
            this.playerStandL = new SpriteAnimation();
            this.playerDuckR = new SpriteAnimation();
            this.playerDuckL = new SpriteAnimation();
            playerShootR = new SpriteAnimation();
            playerShootL = new SpriteAnimation();
            shoot = new ProyectilKnife();
            shoot.LoadContent(content, 10, isLeft, position);
            this.position = pos;
            health = 100;
            damage = 10;
            moveSpeed.X = 5;
            ZaWarudoActive = false;
            isLeft = false;
            ColissionBox = new Rectangle(0,0,32,50);
            playerStandR.Initialize(standR, position, 32, 50, 2, 300, Color.White, 2.0f, true);
            playerStandL.Initialize(standL, position, 32, 50, 2, 150, Color.White, 2.0f, true);
            playerL.Initialize(moveRight, position, 32, 50, 8, 100, Color.White, 2.0f, true);
            playerR.Initialize(moveLeft, position, 32, 50, 8, 100, Color.White, 2.0f, true);
            playerDuckR.Initialize(duckR, position, 32, 50, 2, 100, Color.White, 2.0f, false);
            playerDuckL.Initialize(duckL, position, 32, 50, 2, 100, Color.White, 2.0f, false);
            playerShootR.Initialize(shootR, position, 40, 50, 7, 30, Color.White, 2.0f, false);
            playerShootL.Initialize(shootL, position, 40, 50, 7, 30, Color.White, 2.0f, false);

            hasJumped = false;
            sprite = playerStandR;
        }

        public virtual void LoadContent(Gravedad gravity) {
            gravedad = gravity;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 position)
        {
            sprite.Active = true;

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            anotherTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (anotherTimer >= 100 && isShooting)
            {
                anotherTimer = 0;
                isShooting = false;
            }

            if (elapsedTime >= 8000)
            {
                CD = false;
                elapsedTime = 0;
            }

            if (elapsedTime >= 6000 && TheWorld)
            {
                if (otro)
                {
                    EndOf.Play(1.0f, 0, 0);
                    otro = false;
                }

                if (elapsedTime >= 7000 && TheWorld)
                {
                    TheWorld = false;
                    elapsedTime = 0;
                    CD = true;
                    otro = true;
                }
            }

            if (inputManag.KeyDown(Keys.X) && !TheWorld && !CD && ZaWarudoActive)
            {
                BegoF.Play(1.0f, 0, 0);
                elapsedTime = 0;
                TheWorld = true;
                TheWorldTime = new GameTime();
            }

            if (!TheWorld)
            {
                TheWorldTime = gameTime;
            }

            if (inputManag.KeyPressed(Keys.Z))
            {
                if (isLeft)
                    sprite = playerShootL;

                if (!isLeft)
                    sprite = playerShootR;

                anotherTimer = 0;
                sprite.duck = true;
                sprite.Reverse = false;
                isShooting = true;
                sprite.Active = true;
                sprite.Ended = true;
                sprite.Looping = false;
                sprite.Position = new Vector2(position.X, position.Y);
            }
            else
            {
                if (inputManag.KeyDown(Keys.Right, Keys.D))
                {
                    if (!isShooting)
                    {
                        sprite = playerL;


                        sprite.Reverse = false;
                        sprite.Active = true;
                        sprite.Ended = false;
                        sprite.Looping = true;
                        position.X += moveSpeed.X;
                    }
                    isLeft = false;

                    sprite.Position = new Vector2(position.X, position.Y);

                }
                else
                {
                    if (inputManag.KeyDown(Keys.Left, Keys.A))
                    {
                        if (!isShooting)
                        {
                            sprite = playerR;


                            sprite.Reverse = false;
                            sprite.Active = true;
                            sprite.Ended = false;
                            sprite.Looping = true;
                            position.X -= moveSpeed.X;
                        }
                        isLeft = true;

                        sprite.Position = new Vector2(position.X, position.Y);
                    }
                    else
                    {
                        if (inputManag.KeyDown(Keys.Down, Keys.S))
                        {

                            if (isLeft)
                            {
                                sprite = playerDuckL;
                            }
                            else
                            {
                                sprite = playerDuckR;


                            }
                            sprite.Reverse = false;
                            sprite.Looping = true;
                            sprite.Active = true;
                            sprite.Ended = true;
                            isAgachado = true;
                            sprite.Position = new Vector2(position.X, position.Y);
                        }
                        else
                        {

                            if (inputManag.KeyReleased(Keys.S, Keys.Down))
                            {
                                sprite.duck = true;
                                isAgachado = false;
                            }

                            if (isLeft)
                            {
                                if (!isShooting)
                                    sprite = playerStandL;

                            }
                            else
                            {
                                if (!isShooting)
                                    sprite = playerStandR;

                            }

                            if (!isShooting)
                            {
                                sprite.Ended = false;
                                sprite.Reverse = true;
                                sprite.Active = true;
                                sprite.Looping = true;
                                sprite.Position = new Vector2(position.X, position.Y);
                            }


                        }
                    }

                }
            }

            if (isShooting)
                sprite.Position = new Vector2(position.X, position.Y);

            sprite.Update(gameTime);
            this.colissionBox = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, 28, 50);
            inputManag.Update();
            position=gravedad.Update(gameTime,inputManag, position);
            return position;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {

            sprite.Draw(spriteBatch);
        }

        public void Shoot()
        {
            sprite.Ended = false;
            sprite.Reverse = false;
            sprite.Active = true;
            sprite.Looping = false;
            sprite.Position = new Vector2(position.X, position.Y);
            sprite = playerShootR;
        }
    }
}
