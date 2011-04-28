using System;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public abstract class Tile
    {
        protected Point _pos;
        protected Point _oldPos;
        protected Point _min;
        protected Point _max;
        protected double _xw;
        protected double _yw;
        protected double _signx;
        protected double _signy;
        protected double _sx;
        protected double _sy;
    
        public Tile(double x, double y, double xw, double yw)
        {
            this.Pos = new Point(x, y);
            this.XW = xw;
            this.YW = yw;
            this.Min = new Point(this.Pos.X - this.XW, this.Pos.Y - this.YW);
            this.Max = new Point(this.Pos.X + this.XW, this.Pos.Y + this.YW);
            _signx = 0;
            _signy = 0;
            _sx = 0;
            _sy = 0;
        }
    
        public Point Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
    
        public Point OldPos
        {
            get { return _oldPos; }
            set { _oldPos = value; }
        }
    
        public Point Min
        {
            get { return _min; }
            set { _min = value; }
        }
    
        public Point Max
        {
            get { return _max; }
            set { _max = value; }
        }
    
        public double XW
        {
            get { return _xw; }
            set { _xw = value; }
        }
    
        public double YW
        {
            get { return _yw; }
            set { _yw = value; }
        }
    
        public double SignX
        {
            get { return _signx; }
        }
    
        public double SignY
        {
            get { return _signy; }
        }
    
        public double SX
        {
            get { return _sx; }
        }
    
        public double SY
        {
            get { return _sy; }
        }

        public void MoveNoOldPosUpdate(double dx, double dy)
        {
            this.Pos = new Point(this.Pos.X + dx, this.Pos.Y + dy);
            this.Min.X += dx;
            this.Max.X += dx;
            this.Min.Y += dy;
            this.Max.Y += dy;
        }
    
        public virtual void Move(double dx, double dy)
        {
            this.OldPos = this.Pos;
            this.Pos = new Point(this.Pos.X + dx, this.Pos.Y + dy);
            this.Min.X += dx;
            this.Max.X += dx;
            this.Min.Y += dy;
            this.Max.Y += dy;
        }
    
        public abstract string ObjectName
        {
            get;
        }
    }
}
