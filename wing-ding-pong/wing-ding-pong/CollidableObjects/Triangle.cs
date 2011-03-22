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

        public Rectangle BoundingBox
        {
            get
            {
                double minX = _p1.X, minY = _p1.Y;
                double maxX = _p1.X, maxY = _p1.Y;

                if (minX > _p2.X)
                    minX = _p2.X;
                if (minY > _p2.Y)
                    minY = _p2.Y;
                if (minX > _p3.X)
                    minX = _p3.X;
                if (minY > _p3.Y)
                    minY = _p3.Y;

                if (maxX < _p2.X)
                    maxX = _p2.X;
                if (maxY < _p2.Y)
                    maxY = _p2.Y;
                if (maxX < _p3.X)
                    maxX = _p3.X;
                if (maxY < _p3.Y)
                    maxY = _p3.Y;

                return new Rectangle(minX, maxY, maxX, minY);
            }
        }
    }
}
