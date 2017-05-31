﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        protected float moveSpeed;
        protected int damage;
        protected SpriteAnimation sprite;
        protected Rectangle colissionBox;

        protected ContentManager content;
        protected FileManager fileManager;
        protected List<List<string>> attributes, contents;
        protected Texture2D image;
        protected Vector2 position;
        protected Rectangle collictionBox;


        protected Rectangle ColissionBox
        {
            get { return colissionBox; }
            set { colissionBox = value; }
        }

        public virtual void LoadContent(ContentManager content, InputManager input, Vector2 pos) {
            this.content = new ContentManager(content.ServiceProvider, "Content");



        }

        public virtual void UnloadContent() {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime, InputManager inputManag) {

        }

        public virtual Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 pos)
        {
            Vector2 currentPos = new Vector2();
            return currentPos;
        }

        public virtual void Update(GameTime gameTime, Vector2 pos)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 pos) {

        }
        
    }
}
