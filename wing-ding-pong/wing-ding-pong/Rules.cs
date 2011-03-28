using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using wing_ding_pong.User;
using wing_ding_pong.Arena;

namespace wing_ding_pong
{
	public class Rules
	{
        /*change the score if the wall is owned by someone otherwise it would
        bounce.  If the wall is owned by a player then it is a goal so we check
        */
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

        //clone the ball and randomize the position, not the way we want to but
        //throwing code in as a filler
        public void multiball(Ball ball)
        {
            ball.Clone();
            //ball.xCoord && ball.yCoord = rand();
            ball.Clone();
            //ball.xCoord && ball.yCoord = rand();
        }

        //get the paddle owners and change the size randomly 
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

        //need to get the velocity of the wall to change it but have the increase/decrease
        //random
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
