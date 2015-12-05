using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Common
{
    public class Vector
    {
        public Point Origin { get; private set; }
        public Point Lambda { get; private set; }


        public float Theta { get; private set; }
        public float Length { get; private set; }

        public Vector() : this(new Point(), new Point()) { }

        public Vector(Point p1, Point p2)
        {
            Origin = p1;
            Lambda = p2 - p1;
            

            Theta = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p2.X);
            Length = p1.DistanceTo(p2);
        }

        public override int GetHashCode()
        {
            return Origin.GetHashCode() ^ Lambda.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Vector v = obj as Vector;
            return v != null && Origin.Equals(v.Origin) && Lambda.Equals(v.Lambda);
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
