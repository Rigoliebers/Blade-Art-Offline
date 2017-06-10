using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BAO.Clases
{
    public class GameScreen
    {
        public InputManager inputManager;
        protected ContentManager content;


        public virtual void Initialize()
        {
            
        }

        public virtual void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            inputManager = new InputManager();
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
 
        }

    }
}
