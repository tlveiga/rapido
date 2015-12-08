using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Shapes
{
    public class Rectangle : AABBBody
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

            Bounds = CalculateBounds(Position);
        }

        protected override Box CalculateBounds(Point position)
        {
            Box bounds = new Box();
            bounds.Left = position.X - _halfWidth;
            bounds.Right = position.X + _halfWidth;
            bounds.Top = position.Y - _halfHeight;
            bounds.Bottom = position.Y + _halfHeight;
            return bounds;
        }
    }
}
