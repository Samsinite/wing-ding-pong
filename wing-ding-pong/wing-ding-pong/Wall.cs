using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong
{
    class Wall
    {
        private Player _wallOwner = null;

        //so we know if a wall is actually being guarded by a player?
        public Player Owner
        {
            set{_wallOwner =  value;}
            get{return _wallOwner;}
        }
    }
}
