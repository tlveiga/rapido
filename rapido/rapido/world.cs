using rapido.Collisions;
using rapido.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace rapido
{
  public class World
  {
    private class CollisionPair
    {
      public Body Body1 { get; set; }
      public Body Body2 { get; set; }

      public override int GetHashCode()
      {
        return Body1.GetHashCode() ^ Body2.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        if (!(obj is CollisionPair))
          return false;

        CollisionPair pair = (CollisionPair)obj;

        return (Body1.Equals(pair.Body1) && Body2.Equals(pair.Body2)) || (Body1.Equals(pair.Body2) && Body2.Equals(pair.Body1));
      }
    }

    public Box Bounds;
    public Point Size { get; private set; }

    public float GameTime { get; private set; }

    public List<Body> Bodies { get; private set; }
    public List<Group> Groups { get; private set; }

    public World(Point size)
    {
      GameTime = 0;
      Size = size;

      Bounds = new Box();
      Bounds.Top = 0;
      Bounds.Left = 0;
      Bounds.Right = size.X;
      Bounds.Bottom = size.Y;

      Bodies = new List<Body>();
      Groups = new List<Group>();
    }

    public void UpdateWorld(float gametime)
    {
      float timelapsed = gametime - GameTime;
      // Move bodies
      foreach (Body body in Bodies)
      {
        body.BeginUpdate(timelapsed);
        body.UpdatePosition();
      }

      // Check world colisions
      foreach (Body body in Bodies)
      {
        if (body.CheckWorldCollision(this))
          body.UpdateWorldCollision(this);
      }


      // check body collisions
      if (Groups.Count > 0)
      {
        HashSet<CollisionPair> collisionpair = new HashSet<CollisionPair>();

        foreach (Body body in Bodies)
        {
          if (body.Collide)
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
                  if (body.CheckBodyCollision(groupbody))
                  {
                    CollisionPair pair = new CollisionPair();
                    pair.Body1 = body;
                    pair.Body2 = groupbody;
                    collisionpair.Add(pair);
                  }
                }
              }
            }
          }
        }

        foreach (CollisionPair item in collisionpair)
        {
          item.Body1.TryUpdateBodyCollision(item.Body2);
        }
      }

      foreach (Body body in Bodies)
      {
        body.EndUpdate();
      }

      for (int i = Bodies.Count - 1; i >= 0; i--)
      {
        Body body = Bodies[i];
        if (body.MarkDestroy)
        {
          foreach (Group group in Groups)
            group.Remove(body);
          Bodies.Remove(body);
        }
      }

      GameTime = gametime;
    }
  }
}
