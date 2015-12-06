using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Shapes
{
    public class Circle : Body
    {
        public float Radius { get; private set; }

        public Circle(World world, Point center, float radius) : base (world)
        {
            Position = center;
            Radius = radius;
            updateBounds();
        }

        protected override void updateBounds()
        {
            Box bounds = new Box();
            bounds.Left = Position.X - Radius;
            bounds.Right = Position.X + Radius;
            bounds.Top = Position.Y - Radius;
            bounds.Bottom = Position.Y + Radius;
            Bounds = bounds;
        }
    }
}
