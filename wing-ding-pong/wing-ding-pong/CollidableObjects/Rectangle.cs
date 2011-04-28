using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class Rectangle : Tile
    {

        //Expected to be Normalized (right bigger than left, bottom less than top)
        public Rectangle(double x, double y, double xw, double yw) //width = half width
          : base(x, y, (xw / 2), (yw / 2))
        {
        }

        public double Width
        {
            get { return this.Max.X - this.Min.X; }
        }

        public double Height
        {
            get { return this.Max.Y - this.Min.Y; }
        }

        public Point UpperLeft
        {
            get { return new Point(this.Min.X, this.Min.Y); }
        }

        public Point LowerRight
        {
            get { return new Point(this.Max.X, this.Max.Y); }
        }

        public override string ObjectName
        {
            get { return typeof(Rectangle).Name; }
        }
    }
}
