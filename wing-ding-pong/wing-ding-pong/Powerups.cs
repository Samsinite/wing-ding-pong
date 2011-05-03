using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace wing_ding_pong
{
    class MultiBallPowerup : Powerup
    {
        public MultiBallPowerup(Texture2D sprite, CollidableObjects.TriangleType triangleType, _2D.Point center, double width, double height)
            : base(sprite, triangleType, center, width, height)
        {
            _powerupColor = Microsoft.Xna.Framework.Color.Red;
        }

        public override void Activate(Ball ball)
        {
            Ball newBall = ball.Clone();
            newBall.Move(newBall.Radius, newBall.Radius);
        }
    }

    class PowerupGenerator
    {
        List<_2D.Point> _startupPoints = new List<_2D.Point>();

        public PowerupGenerator(_2D.Point center, double arenaHeight)
        {

        }
    }
}
