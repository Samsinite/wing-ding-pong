using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wing_ding_pong
{
    public class Player
    {
        private int _score = 0;
        PlayerIndex _playerIndex;

		// Constructor.
		public Player(PlayerIndex playerIndex)
		{
            _score = 0;
            _playerIndex = playerIndex;
		}

        public int Score
        {
            set {_score = value;}
            get {return _score;}
        }

        public PlayerIndex Player_Index
        {
            get { return _playerIndex; }
        }
    }
}
