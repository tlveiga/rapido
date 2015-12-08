using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Common
{
    public class Vector
    {
        private static Vector _zero = new Vector(Point.Zero);
        public static Vector Zero { get { return _zero; } }

        public Point Origin { get; private set; }
        public Point Lambda { get; private set; }


        public float Theta { get; private set; }
        public float Length { get; private set; }

        public Vector(Point endpoint) : this(new Point(), endpoint) { }

        public Vector(Point beginpoint, Point endpoint)
        {
            Origin = beginpoint;
            Lambda = endpoint - beginpoint;


            Theta = (float)Math.Atan2(endpoint.Y - beginpoint.Y, endpoint.X - endpoint.X);
            Length = beginpoint.DistanceTo(endpoint);
        }

        public Vector(float theta, float length) : this(Point.Zero, theta, length) { }
        public Vector(Point origin, float theta, float length)
        {
            Theta = theta;
            Length = length;

            Origin = origin;
            Lambda = new Point(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }

        public override int GetHashCode()
        {
            return Origin.GetHashCode() ^ Lambda.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector)) return false;
            Vector v = (Vector)obj;
            return Origin.Equals(v.Origin) && Lambda.Equals(v.Lambda);
        }

        public override string ToString()
        {
            return string.Format("Origin: {0} Lambda: {1} Theta: {2} Length: {3}", Origin, Lambda, Theta, Length);
        }

        #region Operator Overload
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.Origin, v1.Lambda + v2.Lambda);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.Origin, v1.Lambda - v2.Lambda);
        }

        #endregion
    }
}
