using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong.Builder
{
	/// <summary>
	///		This is just an example concrete builder; we can change this
	///		to be anything we want, but I added it so that there's at
	///		least a template to go by.
	///		
	///		Essentially this code, as it is, would represent building  a
	///		standard rectangular game level with all of the objects in it.
	/// </summary>
	public class SquareLevelBuilder : GameBuilder
	{
		public SquareLevelBuilder()
		{
			// I think this should be something other than 
			// "wingdingpong", but I can't be sure yet since
			// we don't have all of our code in objects yet.
			//
			// My assumption is that once we do, it'll be of
			// whatever type the game itself is; if that ends
			// up being this type, then cool.
			game = new wingdingpong();
		}

		// All methods below this point can be called in any
		// order and with whatever attributes/arguments will
		// fit; if there is going to be customization done in
		// the builder, do it here in the concrete builder, not
		// in the abstract builder "GameBuilder".
		//
		// This applies to all concrete builders.
        public override void buildWall()
        {
            // Build wall code, such as draw and
            // other settigs.
            throw new NotImplementedException();
        }

        public override void buildPaddle()
        {
            // Build paddle code, such as draw,
            // position and owner.
            throw new NotImplementedException();
        }

        public override void buildBall()
        {
            // Build ball code.
            throw new NotImplementedException();
        }

        public override void buildTeam()
        {
            // Build team code, such as setting
            // members and what-not.
            throw new NotImplementedException();
        }

        public override void buildScore()
        {
            // Build score code.
            throw new NotImplementedException();
        }
	}
}
