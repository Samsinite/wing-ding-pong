using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wing_ding_pong.CollidableObjects
{
    public abstract class Collidable2DBase
    {
        private IList<IObjectType> _objects;

        public Collidable2DBase(IObjectType[] objects)
        {
            _objects = new List<IObjectType>(objects);
        }

        public Collidable2DBase(IList<IObjectType> objects)
        {
            _objects = objects;
        }

        public IList<IObjectType> CollidableObjects
        {
            get { return _objects; }
        }

        public abstract void Update(GameTime gameTime);
    }
}
