using System;
using System.Collections.Generic;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using Microsoft.Xna.Framework.Audio;

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
        private SoundEffect _ballBounceSound;

        public BallPaddleCollisionRules(SoundEffect ballBounceSound)
        {
            _ballBounceSound = ballBounceSound;
        }

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
            _ballBounceSound.Play();
        }
    }

    public class BallArenaWallCollisionRules : ObjectCollisionRulesTraits
    {
        private SoundEffect _pointScoredSound;
        private SoundEffect _ballBounceSound;
        private Point _centerOfArena = null;

        public BallArenaWallCollisionRules(Point centerOfArena, SoundEffect ballBounceSound, SoundEffect pointScoredSound)
        {
            _centerOfArena = centerOfArena;
            _pointScoredSound = pointScoredSound;
            _ballBounceSound = ballBounceSound;
        }

        public void ResolveStaticObjectStaticObjectCollision(Collidable2DBase obj1, Collidable2DBase obj2, Vector obj1PosDp, Vector obj1CollDirection, Vector obj2PosDp, Vector obj2CollDirection)
        {
            Ball ball = (Ball)obj1;
            ArenaWall wall = (ArenaWall)obj2;
            double movementDistance;
            if (wall.HasOwner)
            {
                _pointScoredSound.Play();
                wall.Owner.Score += 1;
                ball.MoveAbsolute(_centerOfArena.X, _centerOfArena.Y);
            }
            else
            {
                _ballBounceSound.Play();
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
