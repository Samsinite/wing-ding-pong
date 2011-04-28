using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Paddle : Collidable2DBase, IDrawable, ICloneable<Paddle>
	{
        private Texture2D _sprite;
        private Rectangle _rec;
        private Player _owner = null;
        

        public Paddle(Texture2D sprite, Rectangle paddleObj)
            : base(new List<Tile>() {paddleObj} )
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
            set { _rec.Pos.X = value; }
            get { return _rec.Pos.X; }
        }

        public double Y
        {
            set { _rec.Pos.Y = value; }
            get { return _rec.Pos.Y; }
        }

        public double Height
        {
            set{ _rec.Height = value; }
            get{ return _rec.Height; }
        }

        public double Width
        {
            set { _rec.Width = value; }
            get { return _rec.Width; }
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
                (int)_rec.Min.X, (int)_rec.Min.Y,
                (int)this.Width, (int)this.Height),
                Microsoft.Xna.Framework.Color.White);
        }

        Paddle ICloneable<Paddle>.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
