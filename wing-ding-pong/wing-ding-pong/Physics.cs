using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wing_ding_pong
{
	public class Physics
	{
        public bool isCollision(GameTime time, CollidableObjects.Circle circle, CollidableObjects.Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public bool isCollision(GameTime time, CollidableObjects.Circle c1, CollidableObjects.Circle c2)
        {
            throw new NotImplementedException();
        }

        public bool isCollision(GameTime time, CollidableObjects.Circle circle, CollidableObjects.Triangle triangle)
        {
            throw new NotImplementedException();
        }

        private bool isCollision(GameTime time, CollidableObjects.Circle circle, _2D.Point lineStart, _2D.Point lineEnd)
        {
            throw new NotImplementedException();
        }

        /* This function returns the distance of intersection relative to v1.
         * So .5 would mean that the intersection of the two lines would be
         * halfway along the vector v1 that starts a v1Pos.
         * If the return value is > 1 than the intersection is beyond v1
         * If the return value is < 0 than the intersection is behind v1
        */
        private double intersectionDistance(_2D.Point v1Pos, _2D.Vector v1, _2D.Point v2Pos, _2D.Vector v2)
        {
            double conX;
            double conY;
            double con;
            double det = (v1.Y * v2.X) - (v1.X * v2.Y);
            if (det == 0) //area is 0 so they are facing the same or opposite directions
            {
                double v1Dist = Math.Sqrt(_2D.Math2D.DistanceSquared(v1));
                if (((v1.X >= 0 && v2.X >= 0) || (v1.X < 0 && v2.X < 0)) && ((v1.Y >= 0 && v2.Y >= 0) || (v1.Y < 0 && v2.Y < 0)))
                {
                    //vectors point in same directions
                    return v1Dist - Math.Sqrt(_2D.Math2D.DistanceSquared(v2Pos, v1Pos));
                }
                else
                {
                    //vectors point in opposite directions
                    double v2Dist = Math.Sqrt(_2D.Math2D.DistanceSquared(v2));
                    return v1Dist - (Math.Sqrt(_2D.Math2D.DistanceSquared(v2Pos, v1Pos)) + v2Dist);
                }
            }
            conX = v2Pos.X - v1Pos.X;
            conY = v2Pos.Y - v1Pos.Y;
            con = (v2.X * conY) - (v2.Y * conX);
            return con / det;
        }
	}
}
