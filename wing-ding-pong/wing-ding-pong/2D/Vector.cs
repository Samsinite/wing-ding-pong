using System;
using System.Collections.Generic;

namespace wing_ding_pong._2D
{
    public class Vector : ICloneable<Vector>
    {
        double _x = 0, _y = 0;

        public Vector(Point p1, Point p2)
        {
            _x = p2.X - p1.X;
            _y = p2.Y - p1.Y;
        }

        public Vector(double dX, double dY)
        {
            _x = dX;
            _y = dY;
        }

        public Vector()
        { }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public void Mult(double scalar)
        {
            X = X * scalar;
            Y = Y * scalar;
        }

        public Vector Clone()
        {
            return new Vector(this.X, this.Y);
        }
    }
}
