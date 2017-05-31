using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class Player : Entity
    {

        
        public SpriteAnimation playerL;
        public SpriteAnimation playerR;
        public SpriteAnimation playerStand;

        private Texture2D moveRight;
        private Texture2D moveLeft;
        private Texture2D stand;
        private int state;

        public override void LoadContent(ContentManager content, InputManager input, Vector2 pos)
        {
            moveRight = content.Load<Texture2D>("MC");
            moveLeft = content.Load<Texture2D>("MC2");
            stand = content.Load<Texture2D>("MC0");
            this.playerL = new SpriteAnimation();
            this.playerR = new SpriteAnimation();
            this.playerStand = new SpriteAnimation();
            base.LoadContent(content, input, pos);
            this.position = pos;
            health = 100;
            damage = 10;
            moveSpeed = 5;

            playerStand.Initialize(stand, position, 100, 100, 1, 95, Color.White, 1.0f, true);
            playerL.Initialize(moveRight, position, 32, 50, 4, 95, Color.White, 2.0f, true);
            playerR.Initialize(moveLeft, position, 32, 50, 4, 95, Color.White, 2.0f, true);

            sprite = playerStand;

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 pos)
        {
            sprite.Active = true;
            inputManag.Update();
            if (inputManag.KeyDown(Keys.Right, Keys.D))
            {
                sprite = playerL;
                position.X += moveSpeed;
                sprite.Active = true;
                sprite.Position = new Vector2(position.X, position.Y);
            }
            else
            {
                if (inputManag.KeyDown(Keys.Left, Keys.A))
                {
                    sprite = playerR;
                    position.X -= moveSpeed;
                    sprite.Active = true;
                    sprite.Position = new Vector2(position.X, position.Y);
                }
                else
                {
                    sprite = playerStand;
                    sprite.Position = new Vector2(position.X, position.Y);
                    sprite.Active = true;
                }
            }
            sprite.Update(gameTime);
            return position;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {

            sprite.Draw(spriteBatch);

            //switch (state)
            //{
            //    case 0:
            //        spriteBatch.Draw(spriteBatch);
            //        break;

            //    case 1:
            //        playerL.Draw(spriteBatch);
            //        break;

            //    case 2:
            //        playerR.Draw(spriteBatch);
            //        break;

            //}
        }
    }
}
