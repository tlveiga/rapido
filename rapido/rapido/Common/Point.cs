using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Common
{
    public class Point
    {
        private static Point _zero = new Point(0f, 0f);
        public static Point Zero { get { return _zero; } }

        private static Point _invalid = new Point(float.NaN, float.NaN);
        public static Point Invalid { get { return _invalid; } }

        public float X { get; private set; }
        public float Y { get; private set; }

        public Point() : this(0f, 0f) { }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }


        public float DistanceTo(Point p)
        {
            float difX = X - p.X;
            float difY = Y - p.Y;

            return (float)Math.Sqrt(difX * difX + difY * difY);
        }


        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;
            Point p = (Point)obj;
            return X.Equals(p.X) && Y.Equals(p.Y);
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y:{1}", X, Y);
        }
        #region Operator Overloads
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(Point p1, Point p2)
        {
            return new Point(p1.X * p2.X, p1.Y * p2.Y);
        }

        #endregion

    }
}
