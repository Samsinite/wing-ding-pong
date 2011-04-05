using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
namespace wing_ding_pong
{
    class BallSpeedDecrease : Collidable2DBase, IDrawable, ICloneable
    {
        private Circle _circle;
        private Texture2D _sprite;
        private Speed _speed = new Speed(new Vector(0, 0), new TimeSpan(0)); //distance over time

        public BallSpeedDecrease(Texture2D sprite, Point center)
            : base(new List<IObjectType>(){ new Circle(center, sprite.Width / 2) })
        {
            _sprite = sprite;
            _circle = (Circle)CollidableObjects[0];
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_circle.Center.X, (int)_circle.Center.Y,
                (int)_circle.Radius, (int)_circle.Radius),
                Microsoft.Xna.Framework.Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Vector dLoc = _speed.GetSpeed(gameTime.ElapsedGameTime);
            _circle.Center.X += dLoc.X;
            _circle.Center.Y += dLoc.Y;
        }
    }
}
