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

    }

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
            return CollisionDetection.ProjCircleTriangle(circle, triangle, px, py, gridHorz, gridVert,
              out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
        }
    }
}
