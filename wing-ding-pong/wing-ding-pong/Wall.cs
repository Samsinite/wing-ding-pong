using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong
{
    class Wall
    {
        private Player _wallOwner = null;

        public Player Owner
        {
            set{_wallOwner =  value;}
            get{return _wallOwner;}
        }
    }
}
