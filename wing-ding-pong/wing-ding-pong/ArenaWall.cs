using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.CollidableObjects;

namespace wing_ding_pong
{
    public class ArenaWall : Collidable2DBase, IDrawable
    {
        private Player _wallOwner = null;
        private Texture2D _sprite;
        private Rectangle _wall;

        public ArenaWall(Texture2D sprite, Rectangle wallObj)
            : base(new List<IObjectType>() { wallObj })
        {
            _sprite = sprite;
            _wall = (Rectangle)CollidableObjects[0];
        }

        public Player Owner
        {
            set { _wallOwner = value; }
            get { return _wallOwner; }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_wall.Center.X, (int)_wall.Center.Y,
                (int)_wall.Width, (int)_wall.Height),
                Microsoft.Xna.Framework.Color.White);
        }
    }
}