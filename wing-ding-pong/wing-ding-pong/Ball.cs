using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Ball : Collidable2DBase, IDrawable, ICloneable<Ball>
	{
        private Texture2D _sprite;
        private Circle _circle;
        private Player _owner = new Player();

        //sprite is expected to be circular
        public Ball(Texture2D sprite, Point center)
            : base(new List<Tile>(){ new Circle(center.X, center.Y, sprite.Width / 2) })
        {
            _sprite = sprite;
            _circle = (Circle)CollidableObjects[0];
        }


        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_circle.Pos.X, (int)_circle.Pos.Y,
                (int)_circle.Radius, (int)_circle.Radius),
                Microsoft.Xna.Framework.Color.White);
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        //to know who hit the ball last
		public Player Owner
		{
			set { _owner = value; }
			get { return _owner; }
		}

        public Speed BallSpeed
        {
            set { _speed = value; }
            get { return _speed; }
        }

        public double X
        {
            set { _circle.Center.X = value; }
            get { return _circle.Center.X; }
        }

        public double Y
        {
            set { _circle.Center.Y = value; }
            get { return _circle.Center.Y; }
        }

        public double Radius
        {
            get { return _circle.Radius; }
        }
        /* Collision checking is not handled here... just anything that needs updated
         * with association to time */
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Vector dLoc = this.Speed.GetVector(gameTime.ElapsedGameTime);
            _circle.Move(dLoc.X, dLoc.Y);
        }

        Ball ICloneable<Ball>.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
