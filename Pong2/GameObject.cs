using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong2

{
    public class GameObject
    {
        Texture2D texture;
        public Vector2 Position;

        public Rectangle Box;

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.Box = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public GameObject(Texture2D texture, Vector2 position, int Width, int Height)
        {
            this.texture = texture;
            this.Position = position;
            this.Box = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Update(GameTime gametime)
        {
            Box.Location = Position.ToPoint();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box, Color.White);
        }
    }
}