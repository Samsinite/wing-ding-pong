using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong.User
{
    public class Player
    {
        private int _score = 0;

        public Player()
        {
        }

        public int Score
        {
            set {_score = value;}
            get {return _score;}
        }
    }
}
