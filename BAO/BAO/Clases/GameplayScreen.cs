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
        SpriteAnimation playerAnimation;
        Vector2 spritePos;
        Player player;
        public override void LoadContent(ContentManager content)
        {
            //GraphicsDevice obj = fun.GraphicsDevice;
            playerAnimation = new SpriteAnimation();
            //spriteBatch = new SpriteBatch(obj);

            // TODO: use this.Content to load your game content here

            Texture2D playerTexture = content.Load<Texture2D>("MC");



                   

            spritePos = new Vector2(500, 500
                );

            playerAnimation.Initialize(playerTexture, spritePos, 32, 50, 4, 95, Color.White, 1f, true);
           //playerAnimation = new Animation();
             base.LoadContent(content);
            playerAnimation.Active = true;
            player = new Player();
            player.LoadContent(content, inputManager, playerAnimation, spritePos);
        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            playerAnimation.Update(gameTime);
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
            player.Draw(spriteBatch, playerAnimation, spritePos);
        }
    }
}
