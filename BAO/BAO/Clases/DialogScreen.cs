using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BAO.Clases
{
    public class DialogScreen
    {

        public string[,] texto;
        public string imagen;
        public bool Active;

        public bool isSound = true;
        private ContentManager content;
        private SoundEffect sound;
        private Vector2 position;
        private Vector2 txtposition;
        private Vector2 nameposition;
        private Texture2D image;
        private Texture2D back;
        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private SpriteFont font;
        private SpriteFont fontTittle;
        private int contador;
        private string name;

        public void LoadContent(ContentManager Content, string[,] Dialog)
        {
            this.content = Content;
            this.texto = Dialog;
            back = Content.Load<Texture2D>("backdialog");
            font = Content.Load<SpriteFont>("DialogFont");
            fontTittle = Content.Load<SpriteFont>("TittleDialog");
            image = content.Load<Texture2D>(texto[0, 0]);
            nameposition = new Vector2(200, 625);
            position = new Vector2(0, 650);
            txtposition = new Vector2(200, 675);
            Active = true;
        }

        public void Update(GameTime gameTime, InputManager inputManag)
        {
            if (Active)
            {

                image = content.Load<Texture2D>(texto[contador,0]);
                sound = content.Load<SoundEffect>(texto[contador, 3]);

                if (isSound)
                {
                    sound.Play(1.0f, 0, 0);
                    isSound = false;
                }

                if (inputManag.KeyPressed(Keys.Enter))
                {
                    if (contador == texto.GetUpperBound(0))
                    {
                        sound.Dispose();
                        contador = 0;
                        Active = false;
                    }
                    else
                    {
                        sound.Dispose();
                        isSound = true;
                        contador++;
                    }
                }
                inputManag.Update();
            }
        }
        public void Update2(GameTime gameTime, InputManager inputManag)
        {
            if (Active)
            {

                image = content.Load<Texture2D>(texto[contador, 0]);

                if (inputManag.KeyPressed(Keys.Enter))
                {
                    if (contador == texto.GetUpperBound(0))
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
                spriteBatch.Draw(image, position, new Rectangle(0,0,150,150), Color.White);
                spriteBatch.DrawString(fontTittle, WrapText(fontTittle, texto[contador,1], 650), nameposition, Color.White);
                spriteBatch.DrawString(font, WrapText(font, texto[contador,2], 650), txtposition, Color.White);
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
