using System;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong.Traits
{

    /* Provides object name so Traits can be looked up for the object */
    public interface ITraitsObject
    {
        string ObjectName { get; }
    }

    public interface CollisionCheckTraits
    {
      bool CheckAndResolveStaticTileStaticTileCollision(Tile obj1, Tile obj2, double px, double py, double dx, double dy,
                                                        out Vector obj1PosDp, out Vector obj1CollDirection,
                                                        out Vector obj2PosDp, out Vector obj2CollDirection);

      /*bool CheckAndResolveMovingTileStaticTileCollision(Tile movingObj, Vector movingObjVel, Tile staticObj, out Point movingObjCollPos,
          out Vector movingObjCollDirection, out Point staticObjCollPos, out Vector staticObjCollDirection);

      bool CheckAndResolveMovingTileMovingTileCollision(Tile obj1, Vector obj1Vel, Tile obj2, Vector obj2Vel, out Point obj1CollPos,
          out Vector obj1CollDirection, out Point obj2CollPos, out Vector obj2CollDirection);*/
    }

    public class CircleCircleCollisionCheckTraits : CollisionCheckTraits
    {
        public bool CheckAndResolveStaticTileStaticTileCollision(Tile obj1, Tile obj2, double px, double py, double dx, double dy,
                                                        out Vector obj1PosDp, out Vector obj1CollDirection,
                                                        out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            Circle circle1 = (Circle)obj1;
            Circle circle2 = (Circle)obj2;

            double gridVert = 0;
            double gridHorz = 0;

            if (dx < obj2.XW)
            {
                gridHorz = -1; //circle1 is on left side of circle2
            }
            else if (obj2.XW < dx)
            {
                gridHorz = 1; //circle1 is on right side of circle2
            }
  
            if (dy < obj2.YW)
            {
                gridVert = -1; //circle1 is on top side of circle2
            }
            else if (obj2.YW < dy)
            {
                gridVert = 1; //circle1 is on bottom side of circle2
            }
            return CollisionDetection.ProjCircleCircle(circle1, circle2, px, py, gridHorz, gridVert,
                        out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
      }

      /*bool CheckAndResolveMovingTileStaticTileCollision(Tile movingObj, Vector movingObjVel, Tile staticObj, out Point movingObjCollPos,
          out Vector movingObjCollDirection, out Point staticObjCollPos, out Vector staticObjCollDirection)
      {
          Circle circle1 = (Circle)movingObj;
          Circle circle2 = (Circle)staticObj;
          throw new NotImplementedException();
      }

      bool CheckAndResolveMovingTileMovingTileCollision(Tile obj1, Vector obj1Vel, Tile obj2, Vector obj2Vel, out Point obj1CollPos,
          out Vector obj1CollDirection, out Point obj2CollPos, out Vector obj2CollDirection)
      {
          throw new NotImplementedException();
      }*/
    }

    public class CircleRecCollisionCheckTraits : CollisionCheckTraits
    {
        public bool CheckAndResolveStaticTileStaticTileCollision(Tile obj1, Tile obj2, double px, double py, double dx, double dy,
                                                        out Vector obj1PosDp, out Vector obj1CollDirection,
                                                        out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            Circle circle = (Circle)obj1;
            Rectangle rec = (Rectangle)obj2;
          
            double gridVert = 0;
            double gridHorz = 0;
    
            if (dx < (obj2.XW * -1))
            {
                gridHorz = -1; //circle1 is on left side of circle2
            }
            else if (obj2.XW < dx)
            {
                gridHorz = 1; //circle1 is on right side of circle2
            }
   
            if (dy < (obj2.YW * -1))
            {
                gridVert = -1; //circle1 is on top side of circle2
            }
            else if (obj2.YW < dy)
            {
                gridVert = 1; //circle1 is on bottom side of circle2
            }
			
            return CollisionDetection.ProjCircleRec(circle, rec, px, py, gridHorz, gridVert,
                  out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
      }

      /*bool CheckAndResolveMovingTileStaticTileCollision(Tile movingObj, Vector movingObjVel, Tile staticObj, out Point movingObjCollPos,
          out Vector movingObjCollDirection, out Point staticObjCollPos, out Vector staticObjCollDirection)
      {
          if (this.CheckAndResolveStaticTileStaticTileCollision(movingObj, staticObj, out movingObjCollPos, out movingObjCollDirection, out staticObjCollPos, out staticObjCollDirection))
          {
            return true;
          }

          Circle circle = (Circle)movingObj;
          Rectangle rec = (Rectangle)staticObj;
          staticObjCollDirection = null;
          Point a = new Point(rec.Pos.X - rec.XW, rec.Pos.Y - rec.YW);
          Point b = new Point(a.X, rec.Pos.Y + rec.YW);
          Point c = new Point(rec.Pos.X + rec.XW, b.Y);
          Point d = new Point(c.X, a.Y);
          
          if (CollisionDetection.MovingCircleStaticLineIntersection(circle, movingObjVel, a, b))
          {
            staticObjCollPos = rec.Pos;
            CollisionDetection.MovingCircleStaticLineCollisionData(circle, movingObjVel, a, b);
            return true;
          }

          if (CollisionDetection.MovingCircleStaticLineIntersection(circle, movingObjVel, b, c))
          {
            staticObjCollPos = rec.Pos;
            CollisionDetection.MovingCircleStaticLineCollisionData(circle, movingObjVel, b, c);
            return true;
          }

          if (CollisionDetection.MovingCircleStaticLineIntersection(circle, movingObjVel, c, d))
          {
            staticObjCollPos = rec.Pos;
            CollisionDetection.MovingCircleStaticLineCollisionData(circle, movingObjVel, c, d);
            return true;
          }

          movingObjCollPos = null;
          movingObjCollDirection = null;
          staticObjCollPos = null;
          return false;
      }

      bool CheckAndResolveMovingTileMovingTileCollision(Tile obj1, Vector obj1Vel, Tile obj2, Vector obj2Vel, out Point obj1CollPos,
          out Vector obj1CollDirection, out Point obj2CollPos, out Vector obj2CollDirection)
      {
          //currently, there is no reason this should ever be called
          //when it is needed, it should be implemented
          throw new NotImplementedException();
      }*/
    }

    /*public class CirlceLineCollisionCheckTraits : CollisionCheckTraits
    {
      bool CheckAndResolveStaticTileStaticTileCollision(Tile obj1, Tile obj2, out Point obj1CollPos, out Vector obj1CollDirection,
          out Point obj2CollPos, out Vector obj2CollDirection)
      {
          throw new NotImplementedException();
          Circle circle = (Circle)obj1;
          //Line line = (Line)obj2;
      }

      bool CheckAndResolveMovingTileStaticTileCollision(Tile movingObj, Vector movingObjVel, Tile staticObj, out Point movingObjCollPos,
          out Vector movingObjCollDirection, out Point staticObjCollPos, out Vector staticObjCollDirection)
      {
          if (this.CheckAndResolveStaticTileStaticTileCollision(movingObj, staticObj, out movingObjCollPos, out movingObjCollDirection, out staticObjCollPos, out staticObjCollDirection))
          {
            return true;
          }

          Circle circle = (Cirlce)obj1;
          Line line = (Line)obj2;
          Point lineEnd = new Point(line.Pos.X + line.XW, line.Pos.Y + line.YW);

          staticObjCollDirection = null;

          if (CollisionDetection.MovingCircleStaticLineIntersection(circle, movingObjVel, line.Pos, lineEnd))
          {
            staticObjCollPos = line.Pos;
            CollisionDetection.MovingCircleStaticLineCollisionData(circle, movingObjVel, line.Pos, lineEnd);
            return true;
          }

          movingObjCollPos = null;
          movingObjCollDirection = null;
          staticObjCollPos = null;
          return false;
      }
    }*/

    public class CircleTriangleCollisionCheckTraits : CollisionCheckTraits 
    {
        public bool CheckAndResolveStaticTileStaticTileCollision(Tile obj1, Tile obj2, double px, double py, double dx, double dy,
                                                        out Vector obj1PosDp, out Vector obj1CollDirection,
                                                        out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            Circle circle = (Circle)obj1;
            Triangle triangle = (Triangle)obj2;

            double gridVert = 0;
            double gridHorz = 0;

            if (dx < obj2.XW)
            {
                gridHorz = -1; //circle1 is on left side of circle2
            }
            else if (obj2.XW < dx)
            {
                gridHorz = 1; //circle1 is on right side of circle2
            }

            if (dy < obj2.YW)
            {
                gridVert = -1; //circle1 is on top side of circle2
            }
            else if (obj2.YW < dy)
            {
                gridVert = 1; //circle1 is on bottom side of circle2
            }
            return CollisionDetection.ProjCircleTriangle(circle, triangle, px, py, gridHorz, gridVert,
              out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
        }

      /*bool CheckAndResolveMovingTileStaticTileCollision(Tile movingObj, Vector movingObjVel, Tile staticObj, out Point movingObjCollPos,
          out Vector movingObjCollDirection, out Point staticObjCollPos, out Vector staticObjCollDirection)
      {
          const double lengthScaleDC = .125;
          Circle circle = (Circle)movingObj;
          Triangle triangle = (Triangle)staticObj;
          double circleLenScale = Math.Sqrt(circle.XW * circle.XW + circle.YW * circle.YW) * lengthScaleDC;
      }*/
    }
}
