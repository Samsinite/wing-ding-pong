using System;
using System.Collections.Generic;
using wing_ding_pong._2D;

namespace wing_ding_pong.CollidableObjects
{
    public class InvalidTriangleException : Exception
    {
        public InvalidTriangleException()
          : base()
        {
        }
    }

    public enum TriangleType
    {
        Triangle45DegPN = 0, //normal = (+ve, -ve)
        Triangle45DegNN = 1, //normal = (+ve, +ve)
        Triangle45DegNP = 2, //normal = (-ve, +ve)
        Triangle45DegPP = 3 //normal = (-ve, -ve)
    }

    public class Triangle : Tile
    {
        private Point _p1;
        private Point _p2;
        private Point _p3;
        private TriangleType _triangleType;

        public Triangle(TriangleType triangleType, double x, double y, double xw, double yw)
          : base(x, y, xw, yw)
        {
            if (triangleType != TriangleType.Triangle45DegPP && triangleType != TriangleType.Triangle45DegPN &&
                triangleType != TriangleType.Triangle45DegNN && triangleType != TriangleType.Triangle45DegNP)
              throw new InvalidTriangleException();

            this.UpdateType(triangleType);
        }

        public Point[] Points
        {
            get { return new Point[] { _p1, _p2, _p3 }; }
        }

        public override string ObjectName
        {
            get { return typeof(Triangle).Name; }
        }

        public void UpdateType(TriangleType triangleType)
        {
            _triangleType = triangleType;
            switch(_triangleType)
            {
            case TriangleType.Triangle45DegPP:
                _signx = 1;
                _signy = 1;
                break;
            case TriangleType.Triangle45DegPN:
                _signx = 1;
                _signy = -1;
                break;
            case TriangleType.Triangle45DegNN:
                _signx = -1;
                _signy = -1;
                break;
            case TriangleType.Triangle45DegNP:
                _signx = -1;
                _signy = 1;
                break;
            default:
                  throw new InvalidTriangleException();
            }
            _sx = _signx / Math.Sqrt(2);
            _sy = _signy / Math.Sqrt(2);
        }
    }
}
