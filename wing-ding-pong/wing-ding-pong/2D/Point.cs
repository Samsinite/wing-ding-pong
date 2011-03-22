using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong._2D
{
    public class Point
    {
        private double _x = 0, _y = 0;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        #region Properties

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        #endregion

    }
}
