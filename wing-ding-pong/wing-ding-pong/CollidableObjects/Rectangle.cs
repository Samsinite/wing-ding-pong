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
        private double _xCoord;
        private double _yCoord;

        //Expected to be Normalized (right bigger than left, bottom less than top)
        public Rectangle(double x, double y, double width, double height)
        {
            //_center = new Point((right - left) / 2,
            //(top - bottom) / 2);
            ////_center = new Point(right-top, bottom - left);
            //_width = right - left;
            //_height = top - bottom;
            _width = width;
            _height = height;
            _xCoord = x;
            _yCoord = y;

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

        public double X
        {
            get { return _xCoord; }
            set { _xCoord = value; }
        }

        public double Y
        {
            get { return _yCoord; }
            set { _yCoord = value; }
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
    }
}
