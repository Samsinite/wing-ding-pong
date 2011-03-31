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
            set { _center = value; }
            get { return _center; }
        }

        public double Width
        {
            set { _width = value; }
            get { return _width; }
        }

        public double Height
        {
            set { _height = value; } //used to change height for objects like the paddle
            get { return _height; }
        }

        public Rectangle BoundingBox
        {
            get { return this; }
        }

        public bool Intersects(Rectangle other)
        {
            double thisHalfWidth = Width / 2, thisHalfHeight = Height / 2;
            double otherHalfWidth = other.Width / 2, otherHalfHeight = other.Height / 2;

            return ((Center.X - thisHalfWidth >= other.Center.X - otherHalfWidth) &&
                (Center.X + thisHalfWidth < other.Center.X + otherHalfWidth) &&
                (Center.Y - thisHalfHeight >= other.Center.Y - otherHalfHeight) &&
                (Center.Y + thisHalfHeight < other.Center.Y + otherHalfHeight));
        }

    }
}
