using System;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
  public class AABB 
  {
    private Point _pos;
    private Point _oldPos;
    private double _xw; //x width
    private double _yw; //y width

    public AABB(Point pos, double xw, double yw)
    {
      _pos = pos;
      _oldPos = (Point)pos.Clone();
      _xw = xw;
      _yw = yw;
    }

    public Point Pos
    {
      get { return _pos; }
    }

    public Point OldPos
    {
      get { return _oldPos; }
    }

    public double XW
    {
      get { return _xw; }
    }

    public double YW
    {
      get { return _yw; }
    }
  }
}
