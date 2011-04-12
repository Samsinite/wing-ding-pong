using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Paddle : Collidable2DBase, IDrawable, ICloneable
	{
        private Texture2D _sprite;
        private Rectangle _rec;
        private Player _owner = null;
        

        public Paddle(Texture2D sprite, Rectangle paddleObj)
            : base(new List<IObjectType>() {paddleObj} )
        {
            _sprite = sprite;
            _rec = (Rectangle)CollidableObjects[0];
        }

        //so resizing the paddle is possible
        public Player Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }

        public double X
        {
            set { _rec.X = value; }
            get { return _rec.X; }
        }

        public double Y
        {
            set { _rec.Y = value; }
            get { return _rec.Y; }
        }

        public double Height
        {
            set{ _rec.Height = value; }
            get{ return _rec.Height; }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_rec.X, (int)_rec.Y,
                (int)_rec.Width, (int)_rec.Height),
                Microsoft.Xna.Framework.Color.White);
        }
    }
}
