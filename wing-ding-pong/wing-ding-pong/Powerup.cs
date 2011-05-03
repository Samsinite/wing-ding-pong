using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
namespace wing_ding_pong
{
    abstract class Powerup : Collidable2DBase, IDrawable, ICloneable<Powerup>
    {
        private Triangle _triangle;
        private Texture2D _sprite;
        private Speed _speed = new Speed(new Vector(0, 0), new TimeSpan(0)); //distance over time
        protected Microsoft.Xna.Framework.Color _powerupColor;

        public Powerup(Texture2D sprite, TriangleType triangleType, Point center, double width, double height)
            : base(new List<Tile>(){ new Triangle(triangleType, center.X, center.Y, width / 2, height / 2)})
        {
            _sprite = sprite;
            _triangle = (Triangle)CollidableObjects[0];
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_triangle.Min.X, (int)_triangle.Min.Y,
                (int)_triangle.XW * 2, (int)_triangle.YW * 2),
                _powerupColor);
        }

        public abstract void Activate(Ball ball);

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        Powerup ICloneable<Powerup>.Clone()
        {
            throw new NotImplementedException();
        }

        public override string ObjectName
        {
            get { return typeof(Powerup).Name; }
        }
    }
}
