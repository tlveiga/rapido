using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Collisions
{
   public abstract class CollisionObject
    {
        public delegate void CollisionHandler(object sender, object target);
        public abstract event CollisionHandler Collision;

        public abstract bool Collides(AABB aabb);

        protected abstract void checkCollision(AABB target);
    }
}
