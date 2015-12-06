using rapido;
using rapido.Common;
using rapido.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime begin = DateTime.Now;
            World world = new World(new Point(100, 50));

            Rectangle body1 = new Rectangle(new Point(100f * 0.1f, 50f * 0.5f), 5, 5);
            body1.Velocity = new Vector(new Point(15, 0));
            body1.Collision += Body_Collision;
            body1.Collide = true;
            body1.CollisionGroups.Add("grp2");
            body1.CollideWithWorldBoundaries = true;
            body1.OutBounds += Body1_OutBounds;

            Rectangle body2 = new Rectangle(new Point(100f * 0.9f, 50f * 0.5f), 5, 5);
            body2.Velocity = new Vector(new Point(0, 5));
            body2.Collision += Body_Collision;
            body2.Collide = true;
            body2.CollideWithWorldBoundaries = true;
            body2.OutBounds += Body2_OutBounds; ;

            world.Bodies.Add(body1);

            Group grp1 = new Group("grp1");
            grp1.Add(body1);
            world.Groups.Add(grp1);

            Group grp2 = new Group("grp2");
            grp2.Add(body2);
            world.Groups.Add(grp2);


            while (true)
            {
                world.UpdateWorld((float)(DateTime.Now - begin).TotalSeconds);

                if (world.GameTime > 0 && world.Bodies.Count == 1)
                    world.Bodies.Add(body2);


                Console.WriteLine("Body1:{0} Body2:{1}", body1.Bounds, body2.Bounds);

                Thread.Sleep(1000 / 25);
            }

        }

        private static void Body2_OutBounds(object sender, Box target)
        {
            Body body = sender as Body;
            if (body.Bounds.Top <= 0)
                body.Velocity = new Vector(new Point(0, 10));
            else
                body.Velocity = new Vector(new Point(0, -10));
        }

        private static void Body1_OutBounds(object sender, Box target)
        {
            Body body = sender as Body;
            if (body.Bounds.Left <= 0)
                body.Velocity = new Vector(new Point(5, 0));
            else
                body.Velocity = new Vector(new Point(-5, 0));
        }

        private static void Body_Collision(object sender, object target)
        {
            Console.WriteLine("Collision");
            //(sender as Body).Velocity = Vector.Zero;
        }

    }
}
