using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    class Triangle : IObjectType
    {
        private Point _p1;
        private Point _p2;
        private Point _p3;

        public Triangle(Point p1, Point p2, Point p3)
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
        }

        public Point[] Points
        {
            get { return new Point[] { _p1, _p2, _p3 }; }
        }

        public ObjectType GeometryType
        {
            get { return ObjectType.Triangle; }
        }
    }
}
