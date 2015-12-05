using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace rapido
{
    public class World
    {
        public Point Size { get; private set; }

        public float GameTime { get; private set; }

        public List<Body> Bodies { get; private set; }
        public List<Group> Groups { get; private set; }

        public World(Point size)
        {
            Size = size;
            Bodies = new List<Body>();
            Groups = new List<Group>();
        }

        public void UpdateWorld(float gametime) { }
    }
}
