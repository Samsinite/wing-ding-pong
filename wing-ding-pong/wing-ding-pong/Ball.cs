using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Ball : Collidable2DBase, IDrawable 
	{
        private Texture2D _sprite;
        private Speed _speed = new Speed(new Vector(0,0), new TimeSpan(0)); //distance over time
        private Circle _circle;

        //sprite is expected to be circular
        public Ball(Texture2D sprite, Point center)
            : base(new List<IObjectType>(){ new Circle(center, sprite.Width / 2) })
        {
            _sprite = sprite;
            _circle = (Circle)CollidableObjects[0];
        }


        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_circle.Center.X, (int)_circle.Center.Y,
                (int)_circle.Radius, (int)_circle.Radius),
                Microsoft.Xna.Framework.Color.White);
        }

        /* Collision checking is not handled here... just anything that needs updated
         * with association to time */
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Vector dLoc = _speed.GetSpeed(gameTime.ElapsedGameTime);
            _circle.Center.X += dLoc.X;
            _circle.Center.Y += dLoc.Y;
        }
    }
}
