using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Topic_9___Making_a_Player_Class
{
    internal class Player
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;

        public Player(Texture2D texture, int x, int y)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 30, 30);
            _speed = new Vector2();
        }

        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }

        private void Move()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
        }

        public void Update()
        {
            Move();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }

        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public void Offscreen(Rectangle screenSize)
        {
            if (_location.Left >= screenSize.Width)
            {
                _location.X = (1 - _location.Width);
            }
            if (_location.Right <= 0)
            {
                _location.X = screenSize.Width;
            }
            if (_location.Top >= screenSize.Height)
            {
                _location.Y = (1 - _location.Height);
            }
            if (_location.Bottom <= 0)
            {
                _location.Y = screenSize.Height;
            }
        }

        public void UndoMove()
        {
            _location.X -= (int)_speed.X;
            _location.Y -= (int)_speed.Y;
        }

        public void Grow()
        {
            _location.Width += 1;
            _location.Height += 1;
        }
    }
}
