using rapido.Common;
using System;
using System.Collections.Generic;

namespace rapido
{
    public abstract class Body : Collisions.AABB
    {
        /// <summary>
        /// Current archor position
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// Object moving in vector direction
        /// </summary>
        public Vector Velocity { get; set; }
        /// <summary>
        /// Body colides with world boundaries
        /// </summary>
        public bool CollideWithWorldBoundaries { get; set; }
        /// <summary>
        /// Body collides with other Bodies
        /// </summary>
        public bool Collide { get; set; }

        public List<string> CollisionGroups { get; private set; }

        public Body()
        {
            CollisionGroups = new List<string>();
        }
    }
}
