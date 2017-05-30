﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class Enemy : Entity
    {
        public override void LoadContent(ContentManager content, InputManager input, SpriteAnimation sprite, Vector2 pos)
        {
            base.LoadContent(content, input, sprite, pos);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, Vector2 pos)
        {
          
        }
        public override void Draw(SpriteBatch spriteBatch, SpriteAnimation sprite, Vector2 pos)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
