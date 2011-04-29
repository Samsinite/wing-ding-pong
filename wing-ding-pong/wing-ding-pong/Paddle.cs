using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Input;

namespace wing_ding_pong
{
	public class Paddle : Collidable2DBase, IDrawable, ICloneable<Paddle>
	{
        private Texture2D _sprite;
        private Rectangle _rec;
        private Player _owner = null;
        

        public Paddle(Texture2D sprite, Rectangle paddleObj, Player player)
            : base(new List<Tile>() {paddleObj} )
        {
            _owner = player;
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
           if (GamePad.GetState(_owner.Player_Index).DPad.Up == ButtonState.Pressed
                || GamePad.GetState(_owner.Player_Index).ThumbSticks.Left.Y >= 0.5f ||
                Keyboard.GetState(_owner.Player_Index).IsKeyDown(Keys.Up))
            {
                this.Move(0, -10);
            }
            else if (GamePad.GetState(_owner.Player_Index).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(_owner.Player_Index).ThumbSticks.Left.Y <= -0.5f ||
                Keyboard.GetState(_owner.Player_Index).IsKeyDown(Keys.Down))
            {
                this.Move(0, 10);
            }
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

        public override string ObjectName
        {
            get { return typeof(Paddle).Name; }
        }
    }
}
