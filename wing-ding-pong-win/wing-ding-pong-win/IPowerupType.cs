using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wing_ding_pong.Powerups
{
    public enum PowerupType
    {
        Multiball = 0,
        Boomball = 1,
        Boblball = 2,
        PaddleSizeIncrease = 3,
        PaddleSizeDecrease = 4,
        BallSpeedIncrease = 5,
        BallSpeedDecrease = 6
    }

    public interface IPowerupType
    {
        PowerupType getpowerup { get; }
    }
}
