using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wing_ding_pong.CollidableObjects
{
    public abstract class Collidable2DBase
    {
        private IList<Tile> _objects;
        private _2D.Speed _speed = new _2D.Speed(new _2D.Vector(0,0), new TimeSpan(1)); //distance over time
        protected Queue<Microsoft.Xna.Framework.Rectangle> _redrawBounds = new Queue<Microsoft.Xna.Framework.Rectangle>();

        public Collidable2DBase(Tile[] objects)
        {
            _objects = new List<Tile>(objects);
            this.AddDrawBounds();
        }

        public Collidable2DBase(IList<Tile> objects)
        {
            _objects = objects;
        }

        public Queue<Microsoft.Xna.Framework.Rectangle> RedrawBounds
        {
            get { return _redrawBounds; }
        }

        public void AddDrawBounds()
        {
            double xMin = this.CollidableObjects[0].Min.X, yMin = this.CollidableObjects[0].Min.Y,
                    xMax = this.CollidableObjects[0].Max.X, yMax = this.CollidableObjects[0].Max.Y;
            for (int i = 1; i < this.CollidableObjects.Count; i++)
            {
                if (this.CollidableObjects[i].Min.X < xMin)
                    xMin = this.CollidableObjects[i].Min.X;
                if (this.CollidableObjects[i].Min.Y < yMin)
                    yMin = this.CollidableObjects[i].Min.Y;
                if (this.CollidableObjects[i].Max.X > xMax)
                    xMax = this.CollidableObjects[i].Max.X;
                if (this.CollidableObjects[i].Max.Y > yMax)
                    yMax = this.CollidableObjects[i].Max.Y;
            }
            this.RedrawBounds.Enqueue(new Microsoft.Xna.Framework.Rectangle((int)xMin, (int)yMin,
                (int)(xMax - xMin), (int)(yMax - yMin)));
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
        
        public virtual void MoveAbsolute(double x, double y)
        {
            foreach (Tile tile in _objects)
            {
                tile.MoveAbsolute(x, y);
            }
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
