using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Collisions
{
    public abstract class AABB
    {
        public Box Bounds { get; set; }

        public delegate void CollisionHandler(object sender, object target);
        public event CollisionHandler Collision;

        public delegate void OutBoundsHandler(object sender, Box target);
        public event OutBoundsHandler OutBounds;

        public bool Collides(AABB aabb)
        {
            return (Bounds.Right >= aabb.Bounds.Left) && (Bounds.Bottom >= aabb.Bounds.Top) &&
                (aabb.Bounds.Right >= Bounds.Left) && (aabb.Bounds.Bottom >= Bounds.Top);
        }

        public bool InsideBounds(Box box)
        {
            return Bounds.Top > box.Top && Bounds.Bottom < box.Bottom && Bounds.Left > box.Left && Bounds.Right < box.Right;
        }

        public bool CheckBounds(Box box)
        {
            if (OutBounds != null && !InsideBounds(box))
            {
                OutBounds(this, box);
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Check collision and fires event
        /// </summary>
        /// <param name="target"></param>
        public bool CheckCollision(AABB target)
        {
            if (Collision != null && Collides(target))
            {
                Collision(this, target);
                return true;
            }
            else
                return false;
        }
    }
}
