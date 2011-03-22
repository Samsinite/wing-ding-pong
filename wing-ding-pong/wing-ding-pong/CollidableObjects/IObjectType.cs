using System;
using System.Collections.Generic;

namespace wing_ding_pong.CollidableObjects
{
    public enum ObjectType
    {
        Triangle = 0,
        Rectangle = 1,
        Circle = 2
    }

    /* Helps the CollisionDetection determine how to calculate collsisions
     * because any object can be composed of circles, triangles, and squares */
    public interface IObjectType
    {
        ObjectType GeometryType { get; }
    }
}
