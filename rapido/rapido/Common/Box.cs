using System;

namespace rapido.Common
{
    public class Box
    {
        private static Box _zero = new Box();
        public static Box Zero { get { return _zero; } }

        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }

        public Box() : this(0f, 0f, 0f, 0f) { }

        public Box(float left, float rigth, float top, float bottom)
        {
            Left = left;
            Right = rigth;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return string.Format("Left: {0:#.00} Top: {1:#.00} Right: {2:#.00} Bottom: {3:#.00}", Left, Top, Right, Bottom);
        }
    }
}
