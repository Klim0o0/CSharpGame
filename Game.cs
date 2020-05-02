using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Point = System.Drawing.Point;

namespace Game
{
    public class Game
    {
        private readonly Player player;
        private readonly Map map;
        public readonly List<Tuple<Line, Wall>>[] CastetReys;
        public Vector PlayerPos => new Vector(player.X, player.Y);

        public Game(Map map)
        {
            this.map = map;
            player = new Player(10, 10, 0);
            CastetReys = new List<Tuple<Line, Wall>>[player.Rays.Count];
        }

        public void TurnLeft()
        {
            player.TurnAt(0.05);
        }

        public void TurnRight()
        {
            player.TurnAt(-0.05);
        }

        private Vector CorrectVector(Vector f)
        {
            foreach (var wall in map.walls)
            {
                var t = new Vector(f.X, f.Y);
                t.Normalize();
                var r = new Ray(new Vector(player.X, player.Y), 0) {Direction = t};
                var d = r.Cast(wall);
                if (d.X >= 0 && d.Y >= 0 && f.Length > Utils.GetDist(new Vector(player.X, player.Y), d))
                {
                    var x1 = wall.line.A.X;
                    var x2 = wall.line.B.X;
                    var y1 = wall.line.A.Y;
                    var y2 = wall.line.B.Y;
                    var a = y2 - y1;
                    var b = x1 - x2;
                    var c = x1 * (y1 - y2) + y1 * (x2 - x1);
                    var px = f.X + player.X;
                    var py = f.Y + player.Y;
                    var dist = Math.Abs(a * px + b * py + c) / Math.Sqrt(a * a + b * b) + 0.1;
                    var norm = new Vector(a, b);
                    norm.Normalize();
                    norm *= dist;
                    if (norm.X * f.X + norm.Y * f.Y >= 0)
                        f -= norm;
                    else
                        f += norm;
                }
            }

            return f;
        }

        public void MoveForward()
        {
            var t = new Vector(Math.Cos(player.Direction), -Math.Sin(player.Direction)) * player.Speed;
            player.Move(CorrectVector(CorrectVector(t)));
        }

        public void MoveBeak()
        {
            var t = new Vector(-Math.Cos(player.Direction), Math.Sin(player.Direction)) * player.Speed;
            player.Move(CorrectVector(CorrectVector(t)));
        }

        public void MoveLeft()
        {
            var t = new Vector(Math.Cos(player.Direction + Math.PI / 2), -Math.Sin(player.Direction + Math.PI / 2)) * player.Speed;
            player.Move(CorrectVector(CorrectVector(t)));
        }

        public void MoveRight()
        {
            var t = new Vector(-Math.Cos(player.Direction + Math.PI / 2), Math.Sin(player.Direction + Math.PI / 2)) * player.Speed;
            player.Move(CorrectVector(CorrectVector(t)));
        }

        public void CastRays()
        {
            for (var i = 0; i < CastetReys.Length; i++)
            {
                CastetReys[i] = new List<Tuple<Line, Wall>>();
            }

            for (var i = 0; i < player.Rays.Count; i++)
            {
                var ray = player.Rays[i];
                foreach (var wall in map.walls.Concat(map.Enemys.SelectMany(x => x.w)))
                {
                    var currentPoint = ray.Cast(wall);
                    if (!(currentPoint.X > 0) || !(currentPoint.Y > 0))
                    {
                        continue;
                    }

                    CastetReys[i].Add(Tuple.Create(new Line(new Vector(ray.Pos.X, ray.Pos.Y), new Vector(currentPoint.X, currentPoint.Y)), wall));
                }

                CastetReys[i] = CastetReys[i].OrderBy(x => -Utils.GetDist(x.Item1.B, ray.Pos)).ToList();
            }
        }
    }
}