using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class Circle : IObjectType
    {
        private Point _center;
        private double _radius;

        public Circle(double centerX, double centerY, double radius)
        {
            _center = new Point(centerX, centerY);
            _radius = radius;
        }

        public Circle(Point center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public ObjectType GeometryType
        {
            get { return ObjectType.Circle; }
        }

        public double Radius
        {
            get { return _radius; }
        }

        public Point Center
        {
            get { return _center; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(Center.X - Radius, 
                Center.Y + Radius, Center.X + Radius, Center.Y - Radius); }
        }
    }
}
