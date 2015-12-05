using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Collisions
{
    public abstract class AABB : CollisionObject
    {
        protected float _x;
        protected float _X;
        protected float _y;
        protected float _Y;

        public override event CollisionHandler Collision;

        public override bool Collides(AABB aabb)
        {
            return (_X >= aabb._x) && (_Y >= aabb._y) && (aabb._X >= _x) && (aabb._Y >= _y); 
        }

        protected override void checkCollision(AABB target)
        {
            if (Collision != null && Collides(target))
                Collision(this, target);
        }
    }
}
