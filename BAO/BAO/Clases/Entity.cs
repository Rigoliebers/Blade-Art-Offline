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
        public int health;
        public Vector2 moveSpeed;
        public int damage;
        public SpriteAnimation sprite;
        public Rectangle colissionBox;
        protected Rectangle colissionBox2;
        protected ContentManager content;
        protected FileManager fileManager;
        protected List<List<string>> attributes, contents;
        protected Texture2D image;
        public Vector2 position;
        public  Rectangle collictionBox;
        public bool hasJumped;
        public bool caida=false;

        public Rectangle ColissionBox
        {
            get { return colissionBox; }
            set { colissionBox = value; }
        }

        public Rectangle ColissionBox2
        {
            get { return colissionBox2; }
            set { colissionBox2 = value; }
        }
        
        public virtual void LoadContent(ContentManager content, InputManager input, Vector2 pos) {
            this.content = new ContentManager(content.ServiceProvider, "Content");



        }

        public virtual void LoadContent(ContentManager content, InputManager input, Vector2 pos, Vector2 playerpos)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
        } 
    

        public virtual void UnloadContent() {
          
        }

        public virtual void Update(GameTime gameTime, InputManager inputManag) {

        }

        public virtual Vector2 Update(GameTime gameTime, InputManager inputManag, Vector2 pos)
        {
            Vector2 currentPos = new Vector2();
            return currentPos;
        }

        public virtual void Update(GameTime gameTime, Vector2 playerpos, Vector2 pos)
        {

        }

        public virtual void Update(GameTime gameTime, Vector2 pos)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 pos) {

        }
        
    }
}
