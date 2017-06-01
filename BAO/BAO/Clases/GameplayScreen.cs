using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace BAO.Clases
{
    public class GameplayScreen : GameScreen
    {
        XnaFunctionscs fun = new XnaFunctionscs();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 spritePos;
        Player player;
        List<Rectangle> listaObs;
        Texture2D texturaObs;
        BackgroundAnimation fondo;

        public override void LoadContent(ContentManager content)
        {
            fondo = new BackgroundAnimation("Background/background resized");
            //GraphicsDevice obj = fun.GraphicsDevice;
            //spriteBatch = new SpriteBatch(obj);
            // TODO: use this.Content to load your game content here
            spritePos = new Vector2(500, 500);
            fondo.LoadContent(content);
           //playerAnimation = new Animation();
             base.LoadContent(content);
            
            listaObs = new List<Rectangle>();
            listaObs.Add(new Rectangle(900, 400, 400, 200));
            texturaObs = content.Load<Texture2D>("pisitoo");
            player = new Player();
            player.LoadContent(content, inputManager, spritePos);
            player.playerR.Active = true;
        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            fondo.Update(gameTime);
            int posx = (int)spritePos.X;
            int posy = (int)spritePos.Y;
            player.playerStandR.Update(gameTime);
            inputManager.Update();
            //player.Update(gameTime, inputManager,spritePos);
            foreach (Rectangle recto in listaObs)
            {
                if (recto.Intersects(player.ColissionBox))
                {
                    spritePos.X = posx - 5;
                    break;
                }
            }
            spritePos = player.Update(gameTime, inputManager, spritePos);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //fun.GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();

            fondo.Draw(spriteBatch);
            foreach (Rectangle item in listaObs)
            {
                spriteBatch.Draw(texturaObs, item, Color.White);
            }
            base.Draw(spriteBatch);
            player.Draw(spriteBatch, spritePos);
            //spriteBatch.End();
        }
    }
}
