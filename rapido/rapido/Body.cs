using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;

namespace rapido
{
    public abstract class Body : Collisions.AABB
    {
        protected abstract void updateBounds();

        public delegate void BodyHandler(object sender);
        public event BodyHandler Destroyed;
        public string ID { get; set; }
        /// <summary>
        /// Current archor position
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// Diference between current and last update position
        /// </summary>
        public Point Delta { get; set; }
        /// <summary>
        /// Object moving in vector direction
        /// </summary>
        public Vector Velocity { get; set; }
        /// <summary>
        /// Body collides with world boundaries
        /// </summary>
        public bool CollideWithWorldBoundaries { get; set; }
        /// <summary>
        /// Bounces when collides with world boundaries
        /// </summary>
        public bool BounceInWorldBoundaries { get; set; }
        /// <summary>
        /// Body collides with other Bodies
        /// </summary>
        public bool Collide { get; set; }

        public List<string> CollisionGroups { get; private set; }

        public World World { get; set; }

        /// <summary>
        /// Will be destroyed next update
        /// </summary>
        public bool WillDestroy { get; set; }

        public Body(World world)
        {
            CollisionGroups = new List<string>();
            WillDestroy = false;
            World = world;
        }

        public void UpdateBody(float timelapsed)
        {
            if (Velocity.Equals(Vector.Zero))
                Delta = Point.Zero;
            else {
                Point newposition = new Point(Position.X + Velocity.Lambda.X * timelapsed, Position.Y + Velocity.Lambda.Y * timelapsed);
                Delta = newposition - Position;
                Position = newposition;
            }

            updateBounds();

        }

        public void Destroy()
        {
            foreach (Group group in World.Groups)
                group.Remove(this);
            World.Bodies.Remove(this);

            if (Destroyed != null)
                Destroyed(this);
        }

    }
}
