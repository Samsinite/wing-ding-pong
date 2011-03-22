using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Paddle : Collidable2DBase, IDrawable
	{
        private Texture2D _sprite;

        public Paddle(Texture2D sprite, Rectangle paddleObj)
            : base(new List<IObjectType>() {paddleObj} )
        {
            _sprite = sprite;
        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle rec = (Rectangle)CollidableObjects[0];

            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)rec.Center.X, (int)rec.Center.Y,
                (int)rec.Width, (int)rec.Height),
                Microsoft.Xna.Framework.Color.White);
        }
    }
}
