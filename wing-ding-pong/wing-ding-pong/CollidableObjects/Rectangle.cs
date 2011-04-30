using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class Rectangle : Tile
    {
        public Rectangle(double x, double y, double xw, double yw) //width = half width
          : base(x, y, xw, yw)
        {
        }

        public double Width
        {
            get { return this.Max.X - this.Min.X; }
            set
            {
                this.XW = value / 2;
                this.Min.X = this.Pos.X - this.XW;
                this.Max.X = this.Pos.X + this.XW;
            }
        }

        public double Height
        {
            get { return this.Max.Y - this.Min.Y; }
            set
            {
                this.YW = value / 2;
                this.Min.Y = this.Pos.Y - this.YW;
                this.Max.Y = this.Pos.Y + this.YW;
            }
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
