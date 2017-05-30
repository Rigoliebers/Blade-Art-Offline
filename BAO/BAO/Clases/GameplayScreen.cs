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
            GraphicsDevice obj = fun.GraphicsDevice;
            playerAnimation = new SpriteAnimation();
            spriteBatch = new SpriteBatch(obj);

            // TODO: use this.Content to load your game content here

            Texture2D playerTexture = fun.Content.Load<Texture2D>("Imagenes/MC");

            spritePos = new Vector2(
                obj.Viewport.TitleSafeArea.X +
                obj.Viewport.TitleSafeArea.Width / 2,
                obj.Viewport.TitleSafeArea.Y +
                obj.Viewport.TitleSafeArea.Height / 2
                );

            spritePos = new Vector2(500, 500
                );

            playerAnimation.Initialize(playerTexture, spritePos, 32, 50, 4, 95, Color.White, 1f, true);
           // playerAnimation = new Animation();
             base.LoadContent(content);
            //player = new Player();
           // player.LoadContent(content, inputManager);
        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
            player.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            playerAnimation.Update(gameTime);
            //inputManager.Update();
            //player.Update(gameTime, inputManager);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            fun.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            playerAnimation.Draw(spriteBatch);
            spriteBatch.End();
            //base.Draw(spriteBatch);
            //player.Draw(spriteBatch);
        }
    }
}
