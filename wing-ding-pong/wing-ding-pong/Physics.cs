using System;
using System.Collections.Generic;

namespace wing_ding_pong
{
    public class Physics
    {
        public static _2D.Point LineIntersection(_2D.Point line1Start, _2D.Point line1End, _2D.Point line2Start, _2D.Point line2End)
        {
            _2D.Point p;
            double a1 = line1End.X - line1Start.X, a2 = line2End.Y - line2Start.Y;
            double b1 = line1Start.X - line1End.X, b2 = line2Start.X - line2End.X;
            double c1, c2;
            double det = a1 * b2 - a2 * b1;
            if (det == 0)
                return null;
            c1 = a1 * line1Start.X + b1 * line1Start.Y;
            c2 = a2 * line2Start.X + b2 * line2Start.Y;
            p = new _2D.Point((b2 * c1 - b1 * c2) / det, (a1 * c2 - a2 * c1) / det);
            if (p.X >= Math.Min(line1Start.X, line1End.X) && p.X >= Math.Min(line2Start.X, line2End.X) &&
                p.X <= Math.Max(line1Start.X, line1End.X) && p.X <= Math.Max(line2Start.X, line2End.X) &&
                p.Y >= Math.Min(line1Start.Y, line1End.Y) && p.Y >= Math.Min(line2Start.Y, line2End.Y) &&
                p.Y <= Math.Max(line1Start.Y, line1End.Y) && p.Y <= Math.Max(line2Start.Y, line2End.Y))
                return p;
            return null;
        }

        public static _2D.Point LineWithVectorIntersection(_2D.Point line1Start, _2D.Point line1End, _2D.Point line2Start, _2D.Point line2End)
        {
            _2D.Point p;
            double a1 = line1End.X - line1Start.X, a2 = line2End.Y - line2Start.Y;
            double b1 = line1Start.X - line1End.X, b2 = line2Start.X - line2End.X;
            double c1, c2;
            double det = a1 * b2 - a2 * b1;
            if (det == 0)
                return null;
            c1 = a1 * line1Start.X + b1 * line1Start.Y;
            c2 = a2 * line2Start.X + b2 * line2Start.Y;
            p = new _2D.Point((b2 * c1 - b1 * c2) / det, (a1 * c2 - a2 * c1) / det);
            if (p.X >= Math.Min(line1Start.X, line1End.X) && p.X <= Math.Max(line1Start.X, line1End.X) &&
                p.Y >= Math.Min(line1Start.Y, line1End.Y) && p.Y <= Math.Max(line1Start.Y, line1End.Y))
                return p;
            return null;
        }


        public static bool IsPointOnLine(_2D.Point point, _2D.Point lineStart, _2D.Point lineEnd)
        {
            double crossProd = (point.Y - lineStart.Y) * (lineEnd.X - point.X) - (point.X - lineStart.X) * (lineEnd.Y - lineStart.Y);
            if (Math.Abs(crossProd) > 0)
                return false;

            double dotProd = (point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y);
            if (dotProd < 0)
                return false;

            double lineLenSqr = _2D.Math2D.DistanceSquared(lineStart, lineEnd);
            if (dotProd > lineLenSqr)
                return false;

            return true;
        }

        public static _2D.Point ClosestPointOnLineFromPoint(_2D.Point point, _2D.Point lineStart, _2D.Point lineEnd)
        {
            double a1 = lineEnd.Y - lineStart.Y, b1 = lineStart.X - lineEnd.X;
            double c1 = (lineEnd.Y - lineStart.Y) * lineStart.X + (lineStart.X - lineEnd.X) * lineStart.Y;
            double c2 = (-1 * b1) * point.X + a1 * point.Y;
            double det = a1 * a1 + b1 * b1;
            double cx, cy;
            if (det != 0)
            {
                cx = (a1 * c1 - b1 * c2) / det;
                cy = (a1 * c2 + b1 * c1) / det;
            }
            else
            {
                cx = point.X;
                cy = point.Y;
            }
            return new _2D.Point(cx, cy);
        }
    }
}
