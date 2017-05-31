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
        public override void LoadContent(ContentManager content)
        {
            //GraphicsDevice obj = fun.GraphicsDevice;
            //spriteBatch = new SpriteBatch(obj);

            // TODO: use this.Content to load your game content here




                   

            spritePos = new Vector2(500, 500);

           //playerAnimation = new Animation();
             base.LoadContent(content);
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
            player.playerStandR.Update(gameTime);
            inputManager.Update();
            //player.Update(gameTime, inputManager,spritePos);
            spritePos = player.Update(gameTime, inputManager, spritePos);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //fun.GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            //spriteBatch.End();
            base.Draw(spriteBatch);
            player.Draw(spriteBatch, spritePos);
        }
    }
}
