using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Shapes
{
    public class Rectangle : Body
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        protected float _halfWidth;
        protected float _halfHeight;

        public Rectangle(World world, Point center, float width, float height) : base(world)
        {
            Position = center;
            Width = width;
            _halfWidth = width / 2;
            Height = height;
            _halfHeight = height / 2;

            updateBounds();
        }

        protected override void updateBounds()
        {
            Box bounds = new Box();
            bounds.Left = Position.X - _halfWidth;
            bounds.Right = Position.X + _halfWidth;
            bounds.Top = Position.Y - _halfHeight;
            bounds.Bottom = Position.Y + _halfHeight;
            Bounds = bounds;
        }
    }
}
