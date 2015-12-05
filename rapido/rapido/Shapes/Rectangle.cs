using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Shapes
{
    class Rectangle : Body
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        protected float _halfWidth;
        protected float _halfHeight;

        public Rectangle(Point center, float width, float height)
        {
            Position = center;
            Width = width;
            _halfWidth = width / 2;
            Height = height;
            _halfHeight = height / 2;

            updateBounds();
        }

        protected void updateBounds()
        {
            _x = Position.X - _halfWidth;
            _X = Position.X + _halfWidth;
            _y = Position.Y - _halfHeight;
            _Y = Position.Y + _halfHeight;
        }
    }
}
