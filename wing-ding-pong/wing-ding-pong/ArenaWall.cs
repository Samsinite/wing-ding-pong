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
            : base(new List<Tile>() { wallObj })
        {
            _sprite = sprite;
            _wall = (Rectangle)CollidableObjects[0];
        }

        public bool HasOwner
        {
            get { return this.Owner == null; }
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
            
<<<<<<< HEAD
            
=======
>>>>>>> d2d46349a37b20a21a54b6cf80ec7250be93b163
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_wall.Pos.X, (int)_wall.Pos.Y,
                (int)_wall.Width, (int)_wall.Height),
                Microsoft.Xna.Framework.Color.White);
<<<<<<< HEAD
            
=======
>>>>>>> d2d46349a37b20a21a54b6cf80ec7250be93b163
        }

        public override string ObjectName
        {
            get { return typeof(ArenaWall).Name; }
        }
    }
}
