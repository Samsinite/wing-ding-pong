using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wing_ding_pong.CollidableObjects
{
    public abstract class Collidable2DBase
    {
        private IList<Tile> _objects;
        private _2D.Speed _speed = new _2D.Speed(new _2D.Vector(0,0), new TimeSpan(1)); //distance over time

        public Collidable2DBase(Tile[] objects)
        {
            _objects = new List<Tile>(objects);
        }

        public Collidable2DBase(IList<Tile> objects)
        {
            _objects = objects;
        }

        public IList<Tile> CollidableObjects
        {
            get { return _objects; }
        }

        public _2D.Speed Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void Move(double dx, double dy)
        {
            foreach (Tile tile in _objects)
            {
                tile.Move(dx, dy);
            }
        }

        public virtual void MoveNoOldPosUpdate(double dx, double dy)
        {
            foreach (Tile tile in _objects)
            {
                tile.MoveNoOldPosUpdate(dx, dy);
            }
        }

        public abstract void Update(GameTime gameTime);

        public abstract string ObjectName { get; }
    }
}
