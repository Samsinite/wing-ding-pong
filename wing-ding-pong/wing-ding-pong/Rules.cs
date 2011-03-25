using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;

namespace wing_ding_pong
{
	public class Rules
	{
        public void changeScore(Player player, Ball ball, List<Wall> walls)
        {
            foreach(Wall wall in walls)
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

        public void multiball(Ball ball)
        {
            ball.Clone();
            //ball.xCoord && ball.yCoord = rand();
            ball.Clone();
            //ball.xCoord && ball.yCoord = rand();
        }

        public void resizePaddle(List<Paddle> paddles, Ball ball)
        {
            int paddleSizeVal = 0;

            foreach (Paddle paddle in paddles)
            {
                if (paddle.Owner == ball.Owner)
                {
                    //rand between 1 and 2
                    if (paddleSizeVal == 1)
                    {
                        // paddle.size += .5;
                    }
                    else
                    {
                        //   paddle.size -= .5;
                    }
                }
            }
        }

        public void changeBallSpeed(Ball ball)
        {
            int ballRandVal = 0;
            //rand() between 1 and 2
            if (ballRandVal == 1)
            {
                //ball.velocity+=.5;
            }
            else
            {
                //ball.velocity-=.5;
            }
        }

        public void boblBall(Ball ball)
        {

        }

        public void boomBall(Ball ball)
        {

        }

	}
}
