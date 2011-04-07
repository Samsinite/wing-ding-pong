using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Graphics;


namespace wing_ding_pong
{
    class Score : IDrawable
    {
        private Player _owner;
        private Texture2D _sprite;
        

        public Player Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
            //    (int)_circle.Center.X, (int)_circle.Center.Y,
            //    (int)_circle.Radius, (int)_circle.Radius),
            //    Microsoft.Xna.Framework.Color.White);
        }
    }
}
