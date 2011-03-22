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

        //sprite is expected to be circular
        public Ball(Texture2D sprite, Point center)
            : base(new List<IObjectType>(){ new Circle(center, sprite.Width / 2) })
        {
            _sprite = sprite;
        }


        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            Circle circle = (Circle)CollidableObjects[0];
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)circle.Center.X, (int)circle.Center.Y,
                (int)circle.Radius, (int)circle.Radius),
                Microsoft.Xna.Framework.Color.White);
        }

        /* Collision checking is not handled here... just anything that needs updated
         * with association to time */
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Circle circle = (Circle)CollidableObjects[0];
            Vector dLoc = _speed.GetSpeed(gameTime.ElapsedGameTime);
            circle.Center.X += dLoc.X;
            circle.Center.Y += dLoc.Y;
        }
    }
}
