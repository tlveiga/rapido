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

        public Circle(Point center, float radius)
        {
            Position = center;
            Radius = radius;
            updateBounds();
        }

        protected void updateBounds()
        {
            _x = Position.X - Radius;
            _X = Position.X + Radius;
            _y = Position.Y - Radius;
            _Y = Position.Y + Radius;
        }
    }
}
