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
        private IList<Texture2D> _sprites;
        private Rectangle _wall;

        public ArenaWall(IList<Texture2D> sprites, Rectangle wallObj)
            : base(new List<Tile>() { wallObj })
        {
            _sprites = sprites;
            _wall = (Rectangle)CollidableObjects[0];
        }

        public bool HasOwner
        {
            get { return this.Owner != null; }
        }

        public Player Owner
        {
            set { _wallOwner = value; }
            get { return _wallOwner; }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprites[0], new Microsoft.Xna.Framework.Vector2((float)_wall.Min.X, (float)_wall.Min.Y),
                new Microsoft.Xna.Framework.Rectangle(0, 0, (int)_wall.Width, (int)_wall.Height),
                Microsoft.Xna.Framework.Color.White, 0, Microsoft.Xna.Framework.Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public override string ObjectName
        {
            get { return typeof(ArenaWall).Name; }
        }
    }
}
