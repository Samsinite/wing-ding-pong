using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Graphics;
using wing_ding_pong.User;

namespace wing_ding_pong.Arena
{
    public class Wall : Collidable2DBase, IDrawable, ICloneable
    {
        private Player _wallOwner = null;
        private Rectangle _rec;
        private Texture2D _sprite;

        public Wall(Texture2D sprite, Rectangle wallObj)
            : base(new List<IObjectType>() {wallObj} )
        {
            _sprite = sprite;
            _rec = (Rectangle)CollidableObjects[0];
        }

        //so we know if a wall is actually being guarded by a player?
        public Player Owner
        {
            set{_wallOwner =  value;}
            get{return _wallOwner;}
        }

        public void Clone()
        {
            this.MemberwiseClone();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Add some controller logic here
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, new Microsoft.Xna.Framework.Rectangle(
                (int)_rec.Center.X, (int)_rec.Center.Y,
                (int)_rec.Width, (int)_rec.Height),
                Microsoft.Xna.Framework.Color.White);
        }
    }
}
