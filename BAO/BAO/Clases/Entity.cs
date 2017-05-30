using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class Entity
    {
        protected int health;
        protected SpriteSheetAnimation moveAnimation;
        protected float moveSpeed;
        protected ContentManager content;
        protected FileManager fileManager;
        protected List<List<string>> attributes, contents;
        protected Texture2D image;
        protected Vector2 position;

        public virtual void LoadContent(ContentManager content, InputManager input) {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
        }

        public virtual void UnloadContent() {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime, InputManager inputManag) {

        }
        public virtual void Draw(SpriteBatch spriteBatch) {

        }
        
    }
}
