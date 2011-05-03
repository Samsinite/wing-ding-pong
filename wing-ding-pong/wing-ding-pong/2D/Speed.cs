using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong._2D
{
    public class Speed
    {
        private Vector _distance;
        private TimeSpan _dTime;

        public Speed(Vector distance, TimeSpan dTime)
        {
            _distance = distance;
            _dTime = dTime;
        }

        public Vector GetVector(TimeSpan elapsedTime)
        {
            double changeInTime;
            if (_dTime.Ticks == 0)
                return new Vector(0, 0);
            changeInTime = ((double)elapsedTime.Ticks) / ((double)_dTime.Ticks); //unitless
            return new Vector(_distance.X * changeInTime, _distance.Y * changeInTime);
        }

        public Vector Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public TimeSpan DTime
        {
            get { return _dTime; }
        }
    }
}
