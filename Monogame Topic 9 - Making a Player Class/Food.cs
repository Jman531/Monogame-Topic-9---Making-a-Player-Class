using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Topic_9___Making_a_Player_Class
{
    internal class Food
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;

        public Food(Texture2D texture, Rectangle rect, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public void Move(Rectangle window)
        {
            _rectangle.Offset(_speed);

            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
            {
                _speed.X *= -1;
            }
            if (_rectangle.Bottom > window.Height || _rectangle.Top < 0)
            {
                _speed.Y *= -1;
            }
        }

        public void Collide(Rectangle item)
        {
            if (_rectangle.Intersects(new Rectangle(item.Right, item.Y, 1, item.Height)) || _rectangle.Intersects(new Rectangle(item.Left, item.Y, 1, item.Height)))
            {
                _speed.X *= -1;
            }
            if (_rectangle.Intersects(new Rectangle(item.X, item.Top, item.Width, 1)) || _rectangle.Intersects(new Rectangle(item.X, item.Bottom, item.Width, 1)))
            {
                _speed.Y *= -1;
            }
        }
    }
}
