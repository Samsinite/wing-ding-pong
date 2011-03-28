using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
=======
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.User;
>>>>>>> eb455e550da8189cc003d5f353e2879ddec7b706

namespace wing_ding_pong.Arena
{
<<<<<<< HEAD
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
=======
    public class Wall : Collidable2DBase, IDrawable, ICloneable
    {
        private Player _wallOwner = null;
        private Rectangle _rec;
        private Texture2D _sprite;

        public Wall(Texture2D sprite, Rectangle wallObj)
            : base(new List<IObjectType>() {wallObj} )
        {
            _sprite = sprite;
            _rec = (Rectangle)CollidableObjects[0];
        }

        //so we know if a wall is actually being guarded by a player?
        public Player Owner
        {
            set{_wallOwner =  value;}
            get{return _wallOwner;}
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_rec.Center.X, (int)_rec.Center.Y,
                (int)_rec.Width, (int)_rec.Height),
                Microsoft.Xna.Framework.Color.White);
        }
    }
}
>>>>>>> eb455e550da8189cc003d5f353e2879ddec7b706
