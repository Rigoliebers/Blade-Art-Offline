using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class Enemy : Entity
    {
        private bool isleft;
        public SpriteAnimation EnemyL;
        public SpriteAnimation EnemyRL;
        private double elapsedtime;
        private Texture2D enemyL;
        private Texture2D EnRunLeft;
        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            base.LoadContent(content, input, pos);
            enemyL = content.Load<Texture2D>("eWalkL");
            EnRunLeft = content.Load<Texture2D>("eRunkl");
            this.EnemyRL = new SpriteAnimation();
            this.EnemyL = new SpriteAnimation();
            this.position = pos;
            health = 15;
            damage = 10;
            moveSpeed.X = 2;
            isleft = false;
            ColissionBox2 = new Rectangle((int)pos.X, (int)pos.Y, 64, 32);

            EnemyL.Initialize(enemyL, position, 64, 32, 5, 100, Color.White, 2.0f, true);
            //EnemyRL.Initialize(EnRunLeft, position, 67, 32, 4, 100, Color.White, 2.0f,false);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, Vector2 pos)
        {
            sprite.Active = true;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;

            isleft = true;
            this.colissionBox2 = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, 64, 32);
            sprite.Position = new Vector2(position.X, position.Y);
            sprite.Looping = true;

            if (elapsedtime>=1000)
            {
                elapsedtime = 0;
                isleft = false;
                if (isleft == false)
                {
                    sprite.Active = true;
                    sprite = EnemyRL;
                    this.colissionBox2 = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, 64, 32);
                    sprite.Position = new Vector2(position.X, position.Y);
                    position.X += moveSpeed.X;
                    sprite.Looping = true;
                    sprite.Update(gameTime);
                }
            }

            sprite.Update(gameTime);



        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
