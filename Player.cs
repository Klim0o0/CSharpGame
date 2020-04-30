using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game
{
    public class Player
    {
        public float X { get; set; }
        public float Y{get; set;}
        public const double RayStep = 0.005;
        private const float Speed = 3;
        private readonly double vsize =Math.PI / 4;
        public List<Ray> Rays { get; private set; }
        private double Direction{get; set;}
        public  Player(float x, float y, double direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            Rays= new List<Ray>();
            for (var i = Direction-vsize; i <= Direction+vsize; i += RayStep)
            {
                Rays.Add(new Ray(new Vector(x, y ), i));
            }
        }

        private void Move(Vector m, List<Wall> walls)
        {
            var f = m * Speed;
            foreach (var wall in walls)
            {
                var t = new Vector(f.X,f.Y);
                t.Normalize();
                var r = new Ray(new Vector(X, Y), 0) {Direction = t};
                var d = r.Cast(wall);
                if (d.X >= 0 && d.Y >= 0&&f.Length> Utils.GetDist(new Vector(X,Y),d))
                {
                    var x1 = wall.line.A.X;
                    var x2 = wall.line.B.X;
                    var y1 = wall.line.A.Y;
                    var y2 = wall.line.B.Y;
                    var a = y2 - y1;
                    var b = x1 - x2;
                    var c = x1 * (y1 - y2) + y1 * (x2 - x1);
                    var px = f.X + X;
                    var py = f.Y + Y;
                    var dist = Math.Abs(a * px + b * py + c) / Math.Sqrt(a * a + b * b)+0.5;
                    var norm = new Vector(a,b);
                    norm.Normalize();
                    norm *= dist;
                    if (norm.X * f.X+ norm.Y * f.Y>= 0)
                        f -= norm;
                    else
                        f += norm;
                    //return;
                    //break;
                }
            }


            X += (float)f.X;
            Y += (float)f.Y;
            foreach (var ray in Rays)
                ray.Pos = new Vector(X, Y);
        }
        public void TurnLeft()
        {
            Direction = (Direction - 0.05);
            ChangeRays();
        }
        public void TurnRight()
        {
            Direction = (Direction + 0.05);
            ChangeRays();
        }

        private void ChangeRays()
        {
            for (double i = 0, j = Direction-Math.PI/4; i <Rays.Count; i++,j+=RayStep)
            {
                Rays[(int) i].Direction =new Vector(Math.Cos(j),-Math.Sin(j));
            }
        }
        
        public void MoveForward( List<Wall> walls)
        {
            Move(new Vector(Math.Cos(Direction),-Math.Sin(Direction)),walls);
        }
        public void MoveBeak( List<Wall> walls)
        {
            Move(new Vector(-Math.Cos(Direction),Math.Sin(Direction)),walls);
        }
        public void MoveLeft( List<Wall> walls)
        {
            Move(new Vector(-Math.Cos(Direction+Math.PI/2),Math.Sin(Direction+Math.PI/2)),walls);
        }
        public void MoveRight( List<Wall> walls)
        {
            Move(new Vector(Math.Cos(Direction+Math.PI/2),-Math.Sin(Direction+Math.PI/2)),walls);
        }
    }
}
