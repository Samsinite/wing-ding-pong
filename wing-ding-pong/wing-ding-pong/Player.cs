using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong
{
    public class Player
    {
        private int _score = 0;

		// Constructor.
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
