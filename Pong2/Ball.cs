using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong2
{
    class Ball : GameObject
    {

        public Vector2 Velocity;

        public Ball(Texture2D texture, Vector2 position, int Height, int Width, Vector2 velocity) : base(texture, position, Height, Width)
        {
            Velocity = velocity;
        }

        public void checkBallIntersect(GameObject wallUp, GameObject wallDown, GameObject wallLeft, GameObject wallRight, Player player, AI ai)
        {
            if (this.Box.Intersects(wallLeft.Box))
            {
                this.Position.X = wallLeft.Box.Right + this.Box.Width;
                this.Velocity.X *= -1;
            }

            if (this.Box.Intersects(wallRight.Box))
            {
                this.Position.X = wallRight.Box.Left - this.Box.Width;
                this.Velocity.X *= -1;
            }

            if (this.Box.Intersects(wallUp.Box))
            {
                this.Position.Y = wallUp.Box.Bottom + this.Box.Width;
                this.Velocity.Y *= -1;
            }

            if (this.Box.Intersects(wallDown.Box))
            {
                this.Position.Y = wallDown.Box.Top - this.Box.Width;
                this.Velocity.Y *= -1;
            }

            if(this.Box.Intersects(player.Box))
            {
                this.Position.X = player.Box.Right;
                this.Velocity.X *= -1;

                
            }

            if(this.Box.Intersects(ai.Box))
            {
                this.Position.X = ai.Box.Left - this.Box.Width;
                this.Velocity.X *= -1;
            }
        }

        public override void Update(GameTime gametime)
        {
            Position += Velocity;
            Box.Location = Position.ToPoint();
        }
    }
}
