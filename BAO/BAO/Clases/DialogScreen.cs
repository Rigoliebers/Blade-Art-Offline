using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BAO.Clases
{
    public class DialogScreen
    {

        public string[] texto;
        public string imagen;
        public bool Active;

        private ContentManager content;
        private Vector2 position;
        private Vector2 txtposition;
        private Texture2D image;
        private Texture2D back;
        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private SpriteFont font;
        private int contador;

        private string name;

        public void LoadContent(ContentManager Content, string[] Dialog, string Pj, string pjImg)
        {
            this.content = Content;
            this.texto = Dialog;
            image = Content.Load<Texture2D>(pjImg);
            back = Content.Load<Texture2D>("backdialog");
            font = Content.Load<SpriteFont>("Font1");

            position = new Vector2(0, 650);
            txtposition = new Vector2(200, 675);
            Active = true;
        }

        public void Update(GameTime gameTime, InputManager inputManag)
        {
            if (Active)
            {
                if (inputManag.KeyPressed(Keys.Z, Keys.Enter))
                {
                    if (contador == texto.Length - 1)
                    {
                        contador = 0;
                        Active = false;
                    }
                    else
                    {
                        contador++;
                    }
                }

                inputManag.Update();
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (Active)
            {
                spriteBatch.Draw(back, position, Color.White);
                spriteBatch.Draw(image, position, Color.White);
                spriteBatch.DrawString(font, WrapText(font, texto[contador], 650), txtposition, Color.White);
            }
        }

        public static string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    if (size.X > maxLineWidth)
                    {
                        if (sb.ToString() == "")
                        {
                            sb.Append(WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                        else
                        {
                            sb.Append("\n" + WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }
            }

            return sb.ToString();
        }

    }
}
