using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAO.Clases
{
    class Suelo
    {

        //public List<Rectangle> listaObs;
        public Texture2D texturaObs;
        public string directorio;
        public Rectangle rectangulo;
        public Suelo()
        {

        }

        public Suelo(string direccion, ContentManager content) {
            this.directorio = direccion;
            texturaObs = content.Load<Texture2D>(directorio);
        }

        public virtual void LoadContent(ContentManager content)
        {
            texturaObs = content.Load<Texture2D>(directorio);
        }

        public virtual void Update() {

        }

        public virtual void UnloadContent() {

        }


        public virtual void Draw(SpriteBatch spriteBatch, Rectangle recto, Texture2D A) {
            spriteBatch.Draw(A, recto, Color.White);
        }

    }
}
