using System;
using System.Collections.Generic;

namespace wing_ding_pong._2D
{
    class Math2D
    {
        #region Static Methods

        //Sqrt is slow so we don't want to use it
        //returns the distance^2
        public static double DistanceSquared(Point p1, Point p2)
        {
            return (p2.X - p1.X) * (p2.X = p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
        }

        public static double DistanceSquared(Vector v)
        {
            return (v.X * v.X) + (v.Y + v.Y);
        }

        public static double DotProduct(Vector v1, Vector v2)
        {
            return (v1.X * v1.X) + (v2.Y * v2.Y);
        }

        public static Vector Normal(Point p1, Point p2)
        {
            return new Vector(-1 * (p2.Y - p1.Y), (p2.X - p1.X));
        }

        public static Vector Normal(Vector v)
        {
            return new Vector(-1 * v.Y, v.X);
        }

        public static double NormSquared(Vector v)
        {
            return (v.X * v.X) + (v.Y + v.Y);
        }

        public static Vector UnitVector(Vector v)
        {
            return new Vector((v.X / Math.Sqrt(DistanceSquared(v))), (v.Y / Math.Sqrt(DistanceSquared(v))));
        }

        #endregion
    }
}
