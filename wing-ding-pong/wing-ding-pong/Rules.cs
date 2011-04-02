using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using wing_ding_pong.Powerups;

namespace wing_ding_pong
{
	public class Rules
	{
        /*change the score if the wall is owned by someone otherwise it would
        bounce.  If the wall is owned by a player then it is a goal so we check
        */
        public void ChangeScore(Player player, Ball ball, List<ArenaWall> walls)
        {
            foreach(ArenaWall wall in walls)
            {
                if(wall.Owner != null)
                {
                    if (ball.Owner != player)
                        player.Score++;
                    else
                        player.Score--;

                    break;
                }
            }
        }

        public void BallToWall(Ball ball, List<ArenaWall> walls)
        {
            //ball collides off wall
        }

        public void IncreasePaddleSize(List<Paddle> paddles, Ball ball)
        {
            foreach (Paddle paddle in paddles)
            {
                if (paddle.Owner == ball.Owner)
                {
                    paddle.RectangleSize += .5;
                }
            }
        }

        public void DecreasePaddleSize(List<Paddle> paddles, Ball ball)
        {
            foreach (Paddle paddle in paddles)
            {
                if (paddle.Owner == ball.Owner)
                {
                    paddle.RectangleSize -= .5;
                }
            }
        }
        //need to get the velocity of the wall to change it but have the increase/decrease
        //random
        public void IncreaseBallSpeed(Ball ball)
        {
            //get speed and increase           
        }

        public void DecreaseBallSpeed(Ball ball)
        {
            //get speed and decrease
        }

        public void BallToBallCollision(List<Ball> balls)
        {
            //change ball directions
        }

        public void RandomPowerup(List<Player> players, List<Ball> balls, List<IPowerupType> powerups)
        {
            //check owner of ball, randomize powerup, apply to ball and player if neccessary
        }

        public void RandomPowerupPostion(IPowerupType powerup)
        {
            //add powerup to stack
        }   

	}
}
