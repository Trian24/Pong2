using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong2
{
    class Player : GameObject
    {
        public Boolean isMovingDown, isMovingUp;

        public Player(Texture2D texture, Vector2 position, int Width, int Height) : base(texture, position, Width, Height)
        {
            isMovingDown = false;
            isMovingUp = false;
        }

        public void isPlayerIntersect (GameObject wallUp, GameObject wallDown)
        {
            if(this.Box.Intersects(wallUp.Box))
            {
                this.Position.Y = wallUp.Box.Bottom;
            }
            if(this.Box.Intersects(wallDown.Box))
            {
                this.Position.Y = wallDown.Box.Top - this.Box.Height;
            }
        }

        public void playerMove(KeyboardState keyboardState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Position.Y -= 1f;
                isMovingUp = true;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Position.Y += 1f;
                isMovingDown = true;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.Position.Y -= 1f;
                isMovingUp = true;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.Position.Y += 1f;
                isMovingDown = true;
            }
        }
    }
}
