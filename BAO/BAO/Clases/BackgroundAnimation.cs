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
    class BackgroundAnimation
    {
        int width = 1024;
        int height = 800;
        Texture2D escena;
        Rectangle rectangle1;
        Rectangle rectangle2;
        int r1x;
        int r2x;
        int velocidad;
        ContentManager contenido;
        string directorio;

        public BackgroundAnimation()
        {

        }

        public BackgroundAnimation(String directorioRecibir) {
            this.directorio = directorioRecibir;
        }

        public int Width {
            get { return width; }
            set { width = value; }
        }

        public int Height {
            get { return height; }
            set { height = value; }
        }

        public Texture2D Escena {
            get { return escena; }
            set { escena = value; }
        }

        public virtual void Initialize() {

        }

        public virtual void LoadContent(ContentManager content) {
            r1x = 0;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            r2x = r1x + rectangle1.Width;
            rectangle2 = new Rectangle(r2x, 0, width, height);
            velocidad = 5;
            escena = content.Load<Texture2D>(directorio);
        }

        public virtual void Update(GameTime gameTime) {
            r1x -= velocidad;
            r2x -= velocidad;
            rectangle1 = new Rectangle(r1x, 0, width, height);
            rectangle2 = new Rectangle(r2x, 0, width, height);

            if (rectangle1.X <= -width)
            {
                r1x = 0;
            }
            if (rectangle2.X <= 0)
            {
                r2x = width;
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(escena, rectangle1, Color.White);
            spriteBatch.Draw(escena, rectangle2, Color.White);
        }


    }
}
