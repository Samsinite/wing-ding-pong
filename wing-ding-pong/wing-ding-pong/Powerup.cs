using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
namespace wing_ding_pong
{
    class Powerup : Collidable2DBase, IDrawable, ICloneable
    {
        private Triangle _triangle;
        private Texture2D _sprite;
        private Speed _speed = new Speed(new Vector(0, 0), new TimeSpan(0)); //distance over time

        public Powerup(Texture2D sprite, Point center)
            : base(new List<IObjectType>(){ new Triangle(new Point(0.0,0.0),new Point(1.0,1.0),new Point(2.0,2.0))})
        {
            _sprite = sprite;
            _triangle = (Triangle)CollidableObjects[0];
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
           
        }
    }
}
