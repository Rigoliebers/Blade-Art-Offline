using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BAO.Clases
{
    public class Gravedad : Entity
    {

        bool keyreleased = true;
        InputManager inputManagersito = new InputManager();
        public override Vector2 Update (GameTime gameTime, InputManager inputManag, Vector2 position)
        {
            inputManag.Update();
            if (hasJumped == false)
            {
                moveSpeed.Y = 5.25f;
            }

            inputManagersito.Update();
            if (inputManagersito.KeyState.IsKeyUp(Keys.Space))
            {
                keyreleased = true;
            }

            position.Y = position.Y + moveSpeed.Y;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false && keyreleased==true)
            {
                position.Y -= 1f;
                moveSpeed.Y = -6.25f;
                hasJumped = true;
                keyreleased = false;
            }

            if (hasJumped == true)
            {
                moveSpeed.Y += 0.25f;
                position.Y = position.Y + moveSpeed.Y;
            }

            if (position.Y + 50 >= position.Y + 80)
            {
                hasJumped=false;
            }
            if (hasJumped == false)
            {
                moveSpeed.Y = 0f;
            }
            return position;
        }

    }
}
