using System.Collections.Generic;
using System;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using wing_ding_pong.Traits;

namespace wing_ding_pong
{
    class CollisionDetection
    {
        private static IDictionary<string, object> _collisionTraitsRegister;
        
        public static void RegisterCollisionTrait<T1, T2>(CollisionCheckTraits trait)
        {
          if (_collisionTraitsRegister == null)
          {
            _collisionTraitsRegister = new Dictionary<string, object>();
          }
          _collisionTraitsRegister[typeof(T1).Name + typeof(T2).Name] = trait;
        }

        public static CollisionCheckTraits GetCollisionTrait(string className1, string className2)
        {
          if (_collisionTraitsRegister.ContainsKey(className1 + className2))
          {
            return _collisionTraitsRegister[className1 + className2] as CollisionCheckTraits;
          }
          else
          {
            return null;
          }
        }

        public static bool isCollision(Tile obj1, Vector obj1Vel, Tile obj2, Vector obj2Vel, out Vector obj1PosDp,
            out Vector obj1CollDirection, out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            return StaticTileStaticTileCollision(obj1, obj2, out obj1PosDp, out obj1CollDirection, out obj2PosDp,
                out obj2CollDirection);
        }

        public static bool StaticTileStaticTileCollision(Tile obj1, Tile obj2, out Vector obj1PosDp, out Vector obj1CollDirection,
                                                        out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            double dx, dy, px, py;
            CollisionCheckTraits collTrait;

            collTrait = CollisionDetection.GetCollisionTrait(obj1.ObjectName, obj2.ObjectName);
            if (collTrait != null)
            {
                dx = obj1.Pos.X - obj2.Pos.X;
                dy = obj1.Pos.Y - obj2.Pos.Y;
                px = (obj1.XW + obj2.XW) - Math.Abs(dx);
                py = (obj1.YW + obj2.YW) - Math.Abs(dy);
    
                if (0 < px && 0 < py)
                {
                    return collTrait.CheckAndResolveStaticTileStaticTileCollision(obj1, obj2, px, py, dx, dy, out obj1PosDp, out obj1CollDirection,
                        out obj2PosDp, out obj2CollDirection);
                }
            }
            collTrait = CollisionDetection.GetCollisionTrait(obj2.ObjectName, obj1.ObjectName);
            if (collTrait != null)
            {
                dx = obj2.Pos.X - obj1.Pos.X;
                dy = obj2.Pos.Y - obj1.Pos.Y;
                px = (obj2.XW + obj1.XW) - Math.Abs(dx);
                py = (obj2.YW + obj1.YW) - Math.Abs(dy);

                if (0 < px && 0 < py)
                {
                    return collTrait.CheckAndResolveStaticTileStaticTileCollision(obj2, obj1, px, py, dx, dy, out obj2PosDp, out obj2CollDirection,
                        out obj1PosDp, out obj1CollDirection);
                }
            }

            obj1PosDp = null;
            obj1CollDirection = null;
            obj2PosDp = null;
            obj2CollDirection = null;
            return false;
        }

        public static bool ProjCircleCircle(Circle obj1, Circle obj2, double x, double y, double gridHorz, double gridVert,
                                out Vector obj1PosDp, out Vector obj1CollDirection,
                                out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            double distSqr = (obj1.Pos.X - obj1.Pos.X) * (obj1.Pos.X - obj2.Pos.X) +
                            (obj1.Pos.Y - obj2.Pos.Y) * (obj1.Pos.Y - obj2.Pos.Y);
            if (distSqr < obj1.Radius * obj2.Radius)
            {
                //collision
                if (obj1.Pos.X != obj2.Pos.X && obj1.Pos.Y != obj2.Pos.Y)
                {
                    obj1PosDp = new Vector((obj1.Pos.X - obj2.Pos.X) / 2, (obj1.Pos.Y - obj2.Pos.Y) / 2);
                    obj2PosDp = new Vector(obj1PosDp.X * -1, obj1PosDp.Y * -1);
                    obj1CollDirection = Math2D.UnitVector(obj1PosDp);
                    obj2CollDirection = Math2D.UnitVector(obj2PosDp);
                }
                else
                {
                    //let collision fail, this is probably itself
                    obj1PosDp = null;
                    obj1CollDirection = null;
                    obj2PosDp = null;
                    obj2CollDirection = null;
                    return false;
                }
                return true;
            }
            obj1PosDp = null;
            obj1CollDirection = null;
            obj2PosDp = null;
            obj2CollDirection = null;
            return false;
        }

        public static bool ProjCircleRec(Circle obj1, Rectangle obj2, double x, double y, double gridHorz, double gridVert,
                                out Vector obj1PosDp, out Vector obj1CollDirection,
                                out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            double dx, dy, vx, vy, len, pen;
            if (gridHorz == 0)
            {
                if (gridVert == 0)
                {
                    if (x < y)
                    {
                        dx = obj1.Pos.X - obj2.Pos.X;

                        if (dx < 0)
                        {
                            CollisionVsWorldObj(obj1, obj2, -1 * x, 0, -1, 0, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                        else
                        {
                            CollisionVsWorldObj(obj1, obj2, x, 0, 1, 0, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                    }
                    else
                    {
                        dy = obj1.Pos.Y - obj2.Pos.Y;

                        if (dy < 0)
                        {
                            CollisionVsWorldObj(obj1, obj2, 0, -1 * y, 0, -1, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                        else
                        {
                            CollisionVsWorldObj(obj1, obj2, 0, y, 0, 1, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                    }
                }
                else
                {
                    CollisionVsWorldObj(obj1, obj2, 0, y * gridVert, 0, gridVert, out obj1PosDp,
                                    out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                    return true;
                }
            }
            else if (gridVert == 0)
            {
                CollisionVsWorldObj(obj1, obj2, x * gridHorz, 0, gridHorz, 0, out obj1PosDp,
                                out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                return true;
            }
            else
            {
                vx = obj2.Pos.X + (gridHorz * obj2.XW);
                vy = obj2.Pos.Y + (gridVert * obj2.YW);

                dx = obj1.Pos.X - vx;
                dy = obj1.Pos.Y - vy;

                len = Math.Sqrt(dx * dx + dy * dy);
                pen = obj1.Radius - len;

                if (0 < pen)
                {
                    if (len == 0)
                    {
                        dx = gridHorz / Math.Sqrt(2);
                        dy = gridVert / Math.Sqrt(2);
                    }
                    else
                    {
                        dx /= len;
                        dy /= len;
                    }

                    CollisionVsWorldObj(obj1, obj2, dx * pen, dy * pen, dx, dy, out obj1PosDp,
                                    out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                    return true;
                }
            }

            obj1PosDp = null;
            obj1CollDirection = null;
            obj2PosDp = null;
            obj2CollDirection = null;
            return false;
        }

        public static bool ProjCircleTriangle(Circle obj1, Triangle obj2, double x, double y, double gridHorz, double gridVert,
                                out Vector obj1PosDp, out Vector obj1CollDirection,
                                out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            double ox, oy, dp, sx, sy, lenP, lenN, perp, len, pen, vx, vy, dx, dy;
            if (gridHorz == 0)
            {
                if (gridVert == 0)
                {
                    //ox, and oy are the cooridnates of the innermost point on the circle relative to the tile
                    ox = (obj1.Pos.X - (obj2.SX * obj1.Radius)) - obj2.Pos.X;
                    oy = (obj1.Pos.Y - (obj2.SY * obj1.Radius)) - obj2.Pos.Y;
                    dp = (ox * obj2.SX) + (oy * obj2.SY);
                    if (dp < 0)
                    {
                        sx = -1 * dp * obj2.SX;
                        sy = -1 * dp * obj2.SY;

                        if (x < y)
                        {
                            lenP = x;
                            y = 0;
                            if ((obj1.Pos.X - obj2.Pos.X) < 0)
                            {
                                x *= -1;
                            }
                        }
                        else
                        {
                            lenP = y;
                            x = 0;

                            if ((obj1.Pos.Y - obj2.Pos.Y) < 0)
                            {
                                y *= -1;
                            }
                        }

                        lenN = Math.Sqrt(sx * sx + sy * sy);

                        if (lenP < lenN)
                        {
                            CollisionVsWorldObj(obj1, obj2, x, y, x / lenP, y / lenP, out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                        else
                        {
                            CollisionVsWorldObj(obj1, obj2, sx, sy, obj2.SX, obj2.SY, out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                    }
                }
                else
                {
                    if ((obj2.SignY * gridVert) < 0)
                    {
                        CollisionVsWorldObj(obj1, obj2, 0, y * gridVert, 0, gridVert, out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                        return true;
                    }
                    else
                    {
                        sx = obj2.SX;
                        sy = obj2.SY;
                        ox = obj1.Pos.X - (obj2.Pos.X - (obj2.SignX * obj2.XW));
                        oy = obj1.Pos.Y - (obj2.Pos.Y - (gridVert * obj2.YW));

                        perp = (ox * -1 * sy) + (oy * sx);
                        if (0 < (perp * obj2.SignX * obj2.SignY))
                        {
                            len = Math.Sqrt(ox * ox + oy * oy);
                            pen = obj1.Radius - len;
                            if (0 < pen)
                            {
                                ox /= len;
                                oy /= len;
                                CollisionVsWorldObj(obj1, obj2, ox * pen, oy * pen, ox, oy, out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                                return true;
                            }
                        }
                        else
                        {
                            dp = (ox * sx) + (oy * sy);
                            pen = obj1.Radius - Math.Abs(dp);

                            if (0 < pen)
                            {
                                CollisionVsWorldObj(obj1, obj2, sx * pen, sy * pen, sx, sy, out obj1PosDp, out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                                return true;
                            }
                        }
                    }
                }
            }
            else if (gridVert == 0)
            {
                if ((obj2.SignX * gridHorz) < 0)
                {
                    CollisionVsWorldObj(obj1, obj2, x * gridHorz, 0, gridHorz, 0, out obj1PosDp,
                                    out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                    return true;
                }
                else
                {
                    sx = obj2.SX;
                    sy = obj2.SY;
                    ox = obj1.Pos.X - (obj2.Pos.X + (gridHorz * obj2.XW));
                    oy = obj1.Pos.Y - (obj2.Pos.Y - (obj2.SignY * obj2.YW));
                    perp = (ox * -1 * sy) + (oy * sx);

                    if ((perp * obj2.SignX * obj2.SignY) < 0)
                    {
                        len = Math.Sqrt(ox * ox + oy * oy);
                        pen = obj1.Radius - len;
                        if (0 < pen)
                        {
                            ox /= len;
                            oy /= len;

                            CollisionVsWorldObj(obj1, obj2, ox * pen, oy * pen, ox, oy, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                    }
                    else
                    {
                        dp = (ox * sx) + (oy * sy);
                        pen = obj1.Radius - Math.Abs(dp);

                        if (0 < pen)
                        {
                            CollisionVsWorldObj(obj1, obj2, sx * pen, sy * pen, sx, sy, out obj1PosDp,
                                            out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (0 < ((obj2.SignX * gridHorz) + (obj2.SignY * gridVert)))
                {
                    obj1PosDp = null;
                    obj1CollDirection = null;
                    obj2PosDp = null;
                    obj2CollDirection = null;
                    return false;
                }
                else
                {
                    vx = obj2.Pos.X + (gridHorz * obj2.XW);
                    vy = obj2.Pos.Y + (gridVert * obj2.YW);
                    dx = obj1.Pos.X - vx;
                    dy = obj2.Pos.Y - vy;
                    len = Math.Sqrt(dx * dx + dy * dy);
                    pen = obj1.Radius - len;
                    if (0 < pen)
                    {
                        if (len == 0)
                        {
                            dx = gridHorz / Math.Sqrt(2);
                            dy = gridVert / Math.Sqrt(2);
                        }
                        else
                        {
                            dx /= len;
                            dy /= len;
                        }
                        CollisionVsWorldObj(obj1, obj2, dx * pen, dy * pen, dx, dy, out obj1PosDp,
                                        out obj1CollDirection, out obj2PosDp, out obj2CollDirection);
                        return true;
                    }
                }
            }
            obj1PosDp = null;
            obj1CollDirection = null;
            obj2PosDp = null;
            obj2CollDirection = null;
            return false;
        }

        public static void CollisionVsWorldObj(Tile obj, Tile worldObj, double px, double py, double dx, double dy,
                                                out Vector obj1PosDp, out Vector obj1CollDirection,
                                                out Vector obj2PosDp, out Vector obj2CollDirection)
        {
            double vx = obj.Pos.X - obj.OldPos.X;
            double vy = obj.Pos.Y - obj.OldPos.Y;
            double vx2 = worldObj.Pos.X - worldObj.OldPos.X;
            double vy2 = worldObj.Pos.Y - worldObj.OldPos.Y;

            double dp = (vx * dx + vy * dy);
            double nx = dp * dx;
            double ny = dp * dy;
            double tx = vx - nx;
            double ty = vy - ny;

            if (dp >= 0)
            {
                tx = ty = 1;
                nx = ny = 0;
            }
            //moving obj
            obj1PosDp = new Vector(px, py);
            obj1CollDirection = new Vector(tx - nx, ty - ny);
            obj1CollDirection = _2D.Math2D.UnitVector(obj1CollDirection);

            //world obj, not moving anywhere
            obj2PosDp = new Vector(0, 0);
            obj2CollDirection = new Vector(vx2, vy2);
            obj2CollDirection = _2D.Math2D.UnitVector(obj2CollDirection);
        }
    }
}
