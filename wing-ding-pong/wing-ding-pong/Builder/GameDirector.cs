using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong.Builder
{
	/// <summary>
	///		The director class which will give instructions
	///		to the GameBuilder.
	/// </summary>
	public class GameDirector
	{
		// This method controls the default order in which 
		// items are built for the game.
		public void Construct(GameBuilder gameBuilder)
		{
            gameBuilder.buildWall();
            gameBuilder.buildPaddle();
            gameBuilder.buildBall();
            gameBuilder.buildTeam();
            gameBuilder.buildScore();
		}
	}
}
