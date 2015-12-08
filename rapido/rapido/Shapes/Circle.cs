using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Shapes
{
    public class Circle : AABBBody
    {
        public float Radius { get; private set; }

        public Circle(Point center, float radius)
        {
            Position = center;
            Radius = radius;
            Bounds = CalculateBounds(Position);
        }

        protected override Box CalculateBounds(Point position)
        {
            Box bounds = new Box();
            bounds.Left = position.X - Radius;
            bounds.Right = position.X + Radius;
            bounds.Top = position.Y - Radius;
            bounds.Bottom = position.Y + Radius;
            return bounds;
        }
    }
}
