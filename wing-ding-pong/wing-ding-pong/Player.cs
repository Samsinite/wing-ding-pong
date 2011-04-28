using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong
{
    public class Player
    {
        private int _score = 0;
        Microsoft.Xna.Framework.PlayerIndex _playerIndex;

		// Constructor.
		public Player(Microsoft.Xna.Framework.PlayerIndex playerIndex)
		{
            _playerIndex = playerIndex;
		}

        public int Score
        {
            set {_score = value;}
            get {return _score;}
        }
    }
}
