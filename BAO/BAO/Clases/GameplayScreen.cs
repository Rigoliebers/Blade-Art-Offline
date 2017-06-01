using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int width = 1024;
        int height=800;
        Texture2D escena;
        Rectangle rectangle1;
        Rectangle rectangle2;
        int r1x;
        int r2x;
        int velocidad;

        private DialogScreen dialog;
        public override void LoadContent(ContentManager content)
        {
            r1x = 0;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            r2x = r1x + rectangle1.Width;
            rectangle2 = new Rectangle(r2x, 0, width, height);
            velocidad = 5;
            //GraphicsDevice obj = fun.GraphicsDevice;
            //spriteBatch = new SpriteBatch(obj);
            escena = content.Load<Texture2D>("Background/background resized");
            // TODO: use this.Content to load your game content here
            spritePos = new Vector2(500, 500);

           //playerAnimation = new Animation();
             base.LoadContent(content);
            dialog = new DialogScreen();
            listaObs = new List<Rectangle>();
            listaObs.Add(new Rectangle(900, 400, 400, 200));
            texturaObs = content.Load<Texture2D>("pisitoo");
            player = new Player();
            player.LoadContent(content, inputManager, spritePos);
            player.playerR.Active = true;

            string[,] dialogo = new string[,]
            {
                    {"monito0", "El", "Mi pito amigo"},
                    {"monito0", "El", "2"},
                    {"monito0", "El", "3"},
              
                
            };

            dialog.LoadContent(content, dialogo);
        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            r1x -= velocidad;
            r2x -= velocidad;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            rectangle2 = new Rectangle(r2x, 0, width, height);

            if (rectangle1.X <= -width)
            {
                r1x = 0;
            }
            if (rectangle2.X<=0)
            {
                r2x = width;
            }

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
            dialog.Update(gameTime, inputManager);

            if (!dialog.Active)
                spritePos = player.Update(gameTime, inputManager, spritePos);


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //fun.GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            
            spriteBatch.Draw(escena, rectangle1, Color.White);
            spriteBatch.Draw(escena, rectangle2, Color.White);
            foreach (Rectangle item in listaObs)
            {
                spriteBatch.Draw(texturaObs, item, Color.White);
            }

            player.Draw(spriteBatch, spritePos);
            dialog.Draw(spriteBatch);
                        base.Draw(spriteBatch);
        }
    }
}
