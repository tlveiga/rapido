using rapido.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido.Collisions
{
  public abstract class AABBBody : Body
  {
    protected Point _currentPosition;
    protected Box _currentBounds;
    protected Vector _currentVelocity;
    protected float _timelapsed;

    public Box Bounds { get; set; }

    public AABBBody()
    {
      Bounds = Box.Zero;
    }


    public override event DidEventHandler DidCollide;
    public override event DidEventHandler DidDestroy;
    public override event DidEventHandler DidWorldCollide;
    public override event WillEventHandler WillCollide;
    public override event WillEventHandler WillDestroy;
    public override event WillEventHandler WillWorldCollide;

    public override void BeginUpdate(float timelapsed)
    {
      _timelapsed = timelapsed;
      _currentPosition = Position;
      _currentBounds = Bounds;
      _currentVelocity = Velocity;
    }

    public override void UpdatePosition()
    {
      if (!_currentVelocity.Equals(Vector.Zero))
      {
        _currentPosition = new Point(Position.X + Velocity.Lambda.X * _timelapsed, Position.Y + Velocity.Lambda.Y * _timelapsed);
        _currentBounds = CalculateBounds(_currentPosition);
      }
    }


    public override bool CheckWorldCollision(World world)
    {
      return CollideWithWorldBoundaries && !InsideBounds(_currentBounds, world.Bounds);
    }

    public override void UpdateWorldCollision(World world)
    {
      bool allowcollision = true;
      BodyEventArgs args = null;
      if (WillWorldCollide != null)
      {
        args = new BodyEventArgs();
        args.Target = world;
        allowcollision = WillWorldCollide(this, args);
      }

      if (allowcollision)
      {
        float difX = 0f;
        float difY = 0f;
        float dif = 0f;
        if ((dif = _currentBounds.Left - world.Bounds.Left) < 0)
          difX = -dif;
        else if ((dif = world.Bounds.Right - _currentBounds.Right) < 0)
          difX = dif;
        if ((dif = _currentBounds.Top - world.Bounds.Top) < 0)
          difY = -dif;
        else if ((dif = world.Bounds.Bottom - _currentBounds.Bottom) < 0)
          difY = dif;

        _currentPosition = new Point(_currentPosition.X + difX, _currentPosition.Y + difY);
        _currentBounds = CalculateBounds(_currentPosition);

        if (BounceInWorldBoundaries)
        {
          Point newlambda = new Point(difX != 0 ? -_currentVelocity.Lambda.X : _currentVelocity.Lambda.X, difY != 0 ? -_currentVelocity.Lambda.Y : _currentVelocity.Lambda.Y);
          _currentVelocity = new Vector(_currentVelocity.Origin, newlambda);
        }

        if (DidWorldCollide != null)
        {
          DidWorldCollide(this, args);
        }
      }
    }

    public override bool CheckBodyCollision(object body)
    {
      AABBBody target = (AABBBody)body;
      return Collide && CollidesWith(_currentBounds, target._currentBounds);
    }

    public override bool TryUpdateBodyCollision(object body)
    {
      AABBBody target = (AABBBody)body;

      BodyEventArgs args = new BodyEventArgs();
      args.Target = body;

      if (WillCollide == null || WillCollide(this, args))
      {
        float difR = Bounds.Right - target.Bounds.Left;
        float difB = Bounds.Bottom - target.Bounds.Top;
        float difL = target.Bounds.Right - Bounds.Left;
        float difT = target.Bounds.Bottom - Bounds.Top;

        float difH = 0, difV = 0;
        if (difR < 0) // right cleared
          difH = (_currentBounds.Right - target._currentBounds.Left) / 1.99f;
        else if (difL < 0) // left cleared
          difH = (_currentBounds.Left - target._currentBounds.Right) / 1.99f;
        else if (difB < 0) // bottom cleared
          difV = (_currentBounds.Bottom - target._currentBounds.Top) / 1.99f;
        else if (difT < 0) // top cleared
          difV = (_currentBounds.Top - target._currentBounds.Bottom) / 1.99f;

        Point offset = new Point(difH, difV);

        _currentPosition -= offset;
        _currentBounds = CalculateBounds(_currentPosition);
        target._currentPosition += offset;
        target._currentBounds = CalculateBounds(target._currentPosition);

        if (CollidesWith(_currentBounds, target._currentBounds))
        {
          int a = 0;
        }

        if (DidCollide != null)
          DidCollide(this, args);

        return CollidesWith(_currentBounds, target._currentBounds);
      }

      return true;
    }


    public override void EndUpdate()
    {
      Delta = _currentPosition - Position;
      Position = _currentPosition;
      Bounds = _currentBounds;
      Velocity = _currentVelocity;
    }



    protected abstract Box CalculateBounds(Point position);

    public static bool CollidesWith(Box aabb1, Box aabb2)
    {
      return (aabb1.Right >= aabb2.Left) && (aabb1.Bottom >= aabb2.Top) &&
          (aabb2.Right >= aabb1.Left) && (aabb2.Bottom >= aabb1.Top);
    }

    public static bool InsideBounds(Box insidebox, Box outsidebox)
    {
      return insidebox.Top > outsidebox.Top && insidebox.Bottom < outsidebox.Bottom && insidebox.Left > outsidebox.Left && insidebox.Right < outsidebox.Right;
    }

  }
  /*

  public class AABB
  {
      public Box Bounds { get; set; }





      public bool Collides(AABB aabb)
      {
          return (Bounds.Right >= aabb.Bounds.Left) && (Bounds.Bottom >= aabb.Bounds.Top) &&
              (aabb.Bounds.Right >= Bounds.Left) && (aabb.Bounds.Bottom >= Bounds.Top);
      }

      public bool InsideBounds(Box box)
      {
          return Bounds.Top > box.Top && Bounds.Bottom < box.Bottom && Bounds.Left > box.Left && Bounds.Right < box.Right;
      }

      public bool CheckBounds(Box box)
      {
          bool result = !InsideBounds(box);
          if (OutBounds != null && result)
              OutBounds(this, box);
          return result;
      }

      /// <summary>
      /// Check collision and fires event
      /// </summary>
      /// <param name="target"></param>
      public bool CheckCollision(AABB target)
      {
          bool result = Collides(target);
          if (Collision != null && result)
              Collision(this, target);
          return result;
      }
  }*/
}
