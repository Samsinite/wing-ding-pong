using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
    class PaddleSizeDecrease : Collidable2DBase, IDrawable, ICloneable
    {
        private Rectangle _rec;
        private Texture2D _sprite;

        public PaddleSizeDecrease(Texture2D sprite, Triangle powerupObj)
            : base(new List<IObjectType>() { powerupObj })
        {
            _sprite = sprite;
            _rec = (Rectangle)CollidableObjects[0];
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_rec.Center.X, (int)_rec.Center.Y,
                (int)_rec.Width, (int)_rec.Height),
                Microsoft.Xna.Framework.Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }
    }
}
