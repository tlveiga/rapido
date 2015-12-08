using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;

namespace rapido
{
    public class BodyEventArgs
    {
        public object Target { get; set; }
    }

    public abstract class Body
    {
        #region Events


        public delegate bool WillEventHandler(Body sender, BodyEventArgs args);
        public delegate void DidEventHandler(object sender, BodyEventArgs args);

        public abstract event WillEventHandler WillCollide;
        public abstract event DidEventHandler DidCollide;

        public abstract event WillEventHandler WillWorldCollide;
        public abstract event DidEventHandler DidWorldCollide;

        public abstract event WillEventHandler WillDestroy;
        public abstract event DidEventHandler DidDestroy;

        #endregion

        #region Life Events
        /// <summary>
        /// Start the update cycle
        /// </summary>
        /// <param name="gametime"></param>
        public abstract void BeginUpdate(float timelapsed);
        /// <summary>
        /// Update position acording with the body velocity
        /// </summary>
        public abstract void UpdatePosition();
        public abstract bool CheckWorldCollision(World world);
        public abstract void UpdateWorldCollision(World world);
        public abstract bool CheckBodyCollision(object body);
        public abstract bool TryUpdateBodyCollision(object body);

        public abstract void EndUpdate();

        #endregion

        //protected abstract void updateBounds();
        public Box _lastbounds;

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

        public bool Bounce { get; set; }

        public HashSet<string> CollisionGroups { get; private set; }

        public World World { get; set; }

        /// <summary>
        /// Will be destroyed next update
        /// </summary>
        public bool MarkDestroy { get; set; }

        public Body()
        {
            CollisionGroups = new HashSet<string>();
            Position = Point.Zero;
            Velocity = Vector.Zero;

            MarkDestroy = false;
        }
        /*
        public void UpdateBody(float timelapsed)
        {
            if (Velocity.Equals(Vector.Zero))
                Delta = Point.Zero;
            else {
                Point newposition = new Point(Position.X + Velocity.Lambda.X * timelapsed, Position.Y + Velocity.Lambda.Y * timelapsed);
                Delta = newposition - Position;
                Position = newposition;
            }

            _lastbounds = Bounds;
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
        */
    }
}
