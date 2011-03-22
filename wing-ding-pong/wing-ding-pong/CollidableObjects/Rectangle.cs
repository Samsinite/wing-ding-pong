using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class Rectangle : IObjectType
    {
        private Point _center;
        private double _width;
        private double _height;

        //Expected to be Normalized (right bigger than left, bottom less than top)
        public Rectangle(double left, double top, double right, double bottom)
        {
            _center = new Point((right - left) / 2,
            (top - bottom) / 2);
            _width = right - left;
            _height = top - bottom;
        }

        public ObjectType GeometryType
        {
            get { return ObjectType.Rectangle; }
        }

        public Point Center
        {
            get { return _center; }
        }

        public double Width
        {
            get { return _width; }
        }

        public double Height
        {
            get { return _height; }
        }

        public Rectangle BoundingBox
        {
            get { return this; }
        }

        public bool Intersects(Rectangle other)
        {
            throw new NotImplementedException();
        }

    }
}
