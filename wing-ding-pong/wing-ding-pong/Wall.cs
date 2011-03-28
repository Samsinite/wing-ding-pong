using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Wall : IDrawable // Consider "Collidable2DBase as an inherited structure.
	{
		//private Texture2D _sprite;
		private Player _wallOwner = null;

		// You didn't have a constructor here. I've added the basic stuff,
		// but you'll need to think about how to implement the wall object
		// as a type. Is it a rectangle with no width? If so, then use a
		// rectangle object in the constructor in place of "Wall wallObj".
		//
		// If you need additional references, look at the Paddle.cs file
		// for it's constructor. It demonstrates what you'll need to do.
		//public Wall(Texture2D sprite, Wall wallObj) : base(new List<IObjectType>() {wallObj} )
		//{
		//    _sprite = sprite;
		//}

		//so we know if a wall is actually being guarded by a player?
		public Player Owner
		{
			set { _wallOwner = value; }
			get { return _wallOwner; }
		}

		// Add "Collidable2DBase" to the list of inherited classes
		// to make this work. We may or may not need it, but I've
		// added it here for completeness.
		//public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		//{
		//    throw new NotImplementedException();
		//}

		// This was missing.
		public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
		{
			// Draw code for walls here.
		}

	}
}