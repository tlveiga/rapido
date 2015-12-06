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

        public void UpdateWorld(float gametime) {
            float timelapsed = gametime - GameTime;
            // Move bodies
            foreach (Body body in Bodies)
                body.UpdateBody(timelapsed);

            // Check colisions
            foreach (Body body in Bodies)
            {
                if (body.CollideWithWorldBoundaries)
                    body.CheckBounds(_canvas);

                if (body.Collide && Groups.Count > 0) {
                    foreach (string group in body.CollisionGroups)
                    {
                        Group grp = Groups.Find(x => x.GroupID.Equals(group));
                        if (grp != null)
                        {
                            foreach (Body groupbody in grp)
                            {
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

            GameTime = gametime;
        }
    }
}
