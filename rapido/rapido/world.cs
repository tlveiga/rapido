using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace rapido
{
    public class World
    {
        private Box _canvas;
        public Point Size { get; private set; }

        public float GameTime { get; private set; }

        public List<Body> Bodies { get; private set; }
        public List<Group> Groups { get; private set; }

        public World(Point size)
        {
            GameTime = 0;
            Size = size;

            _canvas = new Box();
            _canvas.Top = 0;
            _canvas.Left = 0;
            _canvas.Right = size.X;
            _canvas.Bottom = size.Y;

            Bodies = new List<Body>();
            Groups = new List<Group>();
        }

        public void UpdateWorld(float gametime)
        {
            float timelapsed = gametime - GameTime;
            // Move bodies
            foreach (Body body in Bodies)
                body.UpdateBody(timelapsed);

            // Check colisions
            foreach (Body body in Bodies)
            {
                if (body.CollideWithWorldBoundaries && body.CheckBounds(_canvas))
                {
                    float difX = 0f;
                    float difY = 0f;
                    if (body.Bounds.Left < 0)
                        difX = -body.Bounds.Left;
                    else if (body.Bounds.Right > _canvas.Right)
                        difX = _canvas.Right - body.Bounds.Right;
                    if (body.Bounds.Top < 0)
                        difY = -body.Bounds.Top;
                    else if (body.Bounds.Bottom > _canvas.Bottom)
                        difY = _canvas.Bottom - body.Bounds.Bottom;

                    body.Position = new Point(body.Position.X + difX, body.Position.Y + difY);
                    if (body.CollideWithWorldBoundaries)
                    {
                        Point newlambda = new Point(difX != 0 ? -body.Velocity.Lambda.X : body.Velocity.Lambda.X, difY != 0 ? -body.Velocity.Lambda.Y : body.Velocity.Lambda.Y);
                        body.Velocity = new Vector(body.Velocity.Origin, newlambda);
                    }
                }

                if (body.Collide && Groups.Count > 0)
                {
                    foreach (string group in body.CollisionGroups)
                    {
                        Group grp = Groups.Find(x => x.ID.Equals(group));
                        if (grp != null)
                        {
                            foreach (Body groupbody in grp)
                            {
                                if (body == groupbody) continue;
                                // check condition
                                if (groupbody.Collide)
                                {
                                    body.CheckCollision(groupbody);
                                }
                            }
                        }
                    }
                }
            }

            for (int i = Bodies.Count - 1; i >= 0; i--)
            {
                if (Bodies[i].WillDestroy)
                    Bodies[i].Destroy();
            }

            GameTime = gametime;
        }
    }
}
