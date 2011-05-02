using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
namespace wing_ding_pong
{
    public enum PowerupType
    {
        SmallerPaddle = 0,
        LargerPaddle = 1,
        MultiBall = 2
    }

    class Powerup : Collidable2DBase, IDrawable, ICloneable<Powerup>
    {
        private Triangle _triangle;
        private Texture2D _sprite;
        private Speed _speed = new Speed(new Vector(0, 0), new TimeSpan(0)); //distance over time
        private PowerupType _powerupType;

        public Powerup(Texture2D sprite, PowerupType type, Point center, double width, double height)
            : base(new List<Tile>(){ new Triangle(TriangleType.Triangle45DegNN, center.X, center.Y, width / 2, height / 2)})
        {
            _sprite = sprite;
            _triangle = (Triangle)CollidableObjects[0];
            _powerupType = type;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

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
