using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class Circle : Tile
    {
        private double _radius;

        public Circle(double x, double y, double radius)
          : base(x, y, Math.Abs(radius), Math.Abs(radius))
        {
            _radius = Math.Abs(radius);
        }

        public override string ObjectName
        {
            get { return typeof(Circle).Name; }
        }

        public double Radius
        {
            get { return _radius; }
        }
    }
}
