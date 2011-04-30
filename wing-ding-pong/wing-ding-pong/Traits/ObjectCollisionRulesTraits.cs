using System;
using System.Collections.Generic;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong.Traits
{
    public interface ObjectCollisionRulesTraits
    {
      void ResolveStaticObjectStaticObjectCollision(Collidable2DBase obj1, Collidable2DBase obj2, 
                                                        Vector obj1PosDp, Vector obj1CollDirection,
                                                        Vector obj2PosDp, Vector obj2CollDirection);
    }

    public class BallPaddleCollisionRules : ObjectCollisionRulesTraits
    {
        public void ResolveStaticObjectStaticObjectCollision(Collidable2DBase obj1, Collidable2DBase obj2, Vector obj1PosDp, Vector obj1CollDirection, Vector obj2PosDp, Vector obj2CollDirection)
        {
            Ball ball = (Ball)obj1;
            Paddle paddle = (Paddle)obj2;
            ball.Owner = paddle.Owner;
            double movementDistance;
            movementDistance = Math.Sqrt(Math2D.DistanceSquared(ball.Speed.Distance));
            ball.MoveNoOldPosUpdate(obj1PosDp.X, obj1PosDp.Y);
            ball.Speed.Distance.X = obj1CollDirection.X * movementDistance;
            ball.Speed.Distance.Y = obj1CollDirection.Y * movementDistance;
        }
    }

    public class BallArenaWallCollisionRules : ObjectCollisionRulesTraits
    {
        private Point _centerOfArena = null;

        public BallArenaWallCollisionRules(Point centerOfArena)
        {
            _centerOfArena = centerOfArena;
        }

        public void ResolveStaticObjectStaticObjectCollision(Collidable2DBase obj1, Collidable2DBase obj2, Vector obj1PosDp, Vector obj1CollDirection, Vector obj2PosDp, Vector obj2CollDirection)
        {
            Ball ball = (Ball)obj1;
            ArenaWall wall = (ArenaWall)obj2;
            double movementDistance;
            if (wall.HasOwner)
            {
                wall.Owner.Score += 1;
                ball.X = _centerOfArena.X;
                ball.Y = _centerOfArena.Y;
            }
            else
            {
                movementDistance = Math.Sqrt(Math2D.DistanceSquared(ball.Speed.Distance));
                ball.MoveNoOldPosUpdate(obj1PosDp.X, obj1PosDp.Y);
                ball.Speed.Distance.X = obj1CollDirection.X * movementDistance;
                ball.Speed.Distance.Y = obj1CollDirection.Y * movementDistance;
            }
        }
    }

    public class PaddleArenaWallCollisionRules : ObjectCollisionRulesTraits
    {
        public void ResolveStaticObjectStaticObjectCollision(Collidable2DBase obj1, Collidable2DBase obj2, Vector obj1PosDp, Vector obj1CollDirection, Vector obj2PosDp, Vector obj2CollDirection)
        {
            Paddle paddle = (Paddle)obj1;
            ArenaWall wall = (ArenaWall)obj2;

            paddle.MoveNoOldPosUpdate(0, obj1PosDp.Y);
        }
    }

}
