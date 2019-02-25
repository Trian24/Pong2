using System;
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
    class AI : GameObject
    {
        public Boolean isMovingDown, isMovingUp;

        public AI(Texture2D texture, Vector2 position, int Width, int Height) : base(texture, position, Width, Height)
        {
            isMovingDown = false;
            isMovingUp = false;
        }

        public void AIMove(Ball ball)
        {
            if (ball.Position.Y > this.Box.Center.Y)
            {
                this.Position.Y += 1f;
                isMovingUp = true;
            }
            if (ball.Position.Y < this.Box.Center.Y)
            {
                this.Position.Y -= 1f;
                isMovingDown = true;
            }
        }
        
        public void isAiIntersect(GameObject wallUp, GameObject wallDown)
        {
            if (this.Box.Intersects(wallUp.Box))
            {
                this.Position.Y = wallUp.Box.Bottom;
            }
            if (this.Box.Intersects(wallDown.Box))
            {
                this.Position.Y = wallDown.Box.Top - this.Box.Height;
            }
        }
    }
}
