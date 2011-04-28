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

        public double RectangleSize
        {
            set{ _rec.YW = value; }
            get{ return this._rec.YW; }
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
                (int)_rec.Pos.X, (int)_rec.Pos.Y,
                (int)_rec.XW, (int)_rec.YW),
                Microsoft.Xna.Framework.Color.White);
        }

        Paddle ICloneable<Paddle>.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
