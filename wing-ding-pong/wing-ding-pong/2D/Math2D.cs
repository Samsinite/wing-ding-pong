using System;
using System.Collections.Generic;

namespace wing_ding_pong._2D
{
    class Math2D
    {
        #region Static Methods

        //Sqrt is slow so we don't want to use it
        //returns the distance^2
        static double DoubleDistance(Point p1, Point p2)
        {
            return (p2.X - p1.X) * (p2.X = p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
        }

        static double DotProduct(Vector v1, Vector v2)
        {
            return (v1.X * v1.X) + (v2.Y * v2.Y);
        }

        static double Norm(Point p1, Point p2)
        {
            return Math.Sqrt(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y)));
        }

        static double Norm(Vector v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        static Vector UnitVector(Vector v)
        {
            double normOfV = Norm(v);
            return new Vector(v.X / normOfV, v.Y / normOfV);
        }

        #endregion
    }
}
