using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong.Builder
{
	/// <summary>
	///		This is the abstracted builder; concrete builders
	///		will be added soon. Remember that before this can
	///		be used it must be instantiated by calling on a
	///		"GameBuilder builder;" line.
	///		
	///		After that, we'll need to instantiate the director
	///		with "GameDirector director = new GameDirector();"
	///		
	///		Once we've done that, we can go ahead and use the
	///		builder by using:
	///		
	///		"builder = new (gameTypeHereConstructorHere)();"
	///		"director.Construct(builder);"
	///		"builder.wingdingpong.Show();" (To show game settings)
	///		
	///		Where the argument passed to "Construct" is of type
	///		"GameBuilder", which has all of the appropriate
	///		attributes that we can use in the builder.
	/// </summary>
	public class GameBuilder
	{
		// The new game to be created.
		protected wingdingpong game;

		// Getting an instance of a game.
		public wingdingpong Game
		{
			get{ return game; }
		}

		// Some build methods; feel free to add anything I
		// missed in here.
        public abstract void buildWall();
        public abstract void buildPaddle();
        public abstract void buildBall();
        public abstract void buildTeam();
        public abstract void buildScore();
	}
}
