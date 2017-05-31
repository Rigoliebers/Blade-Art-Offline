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

        private Vector2 anotherpos;

        public override void LoadContent(ContentManager content, InputManager input, SpriteAnimation sprite,Vector2 pos)
        {
            base.LoadContent(content, input, sprite, pos);
            fileManager = new FileManager();
            Vector2 tempFrames = Vector2.Zero;
            moveAnimation = sprite;
            this.anotherpos = pos;

            fileManager.LoadContent("Load/Player.cme", attributes, contents);
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Health":
                            health = int.Parse(contents[i][j]);
                            break;
                        case "Frames":
                            string[] frames = contents[i][j].Split(' ');
                            tempFrames = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                        case "Image":
                            image = content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Position":
                            frames = contents[i][j].Split(' ');
                            position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                        default:
                            break;
                    }
                } 
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 pos)
        {
            moveAnimation.Active = true;
            inputManag.Update();
            if (inputManag.KeyDown(Keys.Right, Keys.D))
            {
                anotherpos.X += 2;
                moveAnimation.Position = new Vector2(anotherpos.X, anotherpos.Y);
            }
            else
            {
                if (inputManag.KeyDown(Keys.Left, Keys.A))
                {
                    anotherpos.X -= 2;
                    moveAnimation.Position = new Vector2(anotherpos.X, anotherpos.Y);
                }
                else
                {
                    moveAnimation.Active = false;
                }
            }
            moveAnimation.Update(gameTime);
            return anotherpos;
        }
        public override void Draw(SpriteBatch spriteBatch, SpriteAnimation sprite, Vector2 pos)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
