using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Game
{
    public class Ray
    {
        public Vector Pos { get;  set; }
        public Vector Direction { get;  set; }

        public Ray(Vector pos, double direction)
        {
            Pos = pos;
            Direction = new Vector(Math.Cos(direction),-Math.Sin(direction));
        }
        public Vector Cast(Wall wall) {
            var x1 = wall.line.A.X;
            var y1 = wall.line.A.Y;
            var x2 = wall.line.B.X;
            var y2 = wall.line.B.Y;

            var x3 = Pos.X;
            var y3 = Pos.Y;
            var x4 = Pos.X + Direction.X;
            var y4 = Pos.Y + Direction.Y;

            var den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0) {
                return new Vector(-85,-85);
            }

            var t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            var u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;
            if (t > 0 && t < 1 && u > 0)
            {
                var pt = new Vector(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
                return pt;
            }
            else
            {
                return new Vector(-85,-85);
            }
        }
    }
}