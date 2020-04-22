using System;
using System.Collections.Generic;
using System.Windows;

namespace Game
{
    public class Map
    {
        readonly public List<Line> walls;
        public Player player;
        public Dictionary<Ray, Line> CastetReys;

        public Map(Player player)
        {
            this.player = player;
            CastetReys = new Dictionary<Ray, Line>();
            walls = new List<Line>();
            walls.Add(new Line(new Vector(500, 200), new Vector(500, 400)));
            walls.Add(new Line(new Vector(600, 5), new Vector(600, 600)));
            walls.Add(new Line(new Vector(600, 600), new Vector(5, 600)));
            walls.Add(new Line(new Vector(5, 600), new Vector(5, 5)));
            walls.Add(new Line(new Vector(5, 5), new Vector(600, 5)));
            var random = new Random();
            for (var i = 0; i < 5; i++)
            {
                walls.Add(new Line(new Vector(random.Next(5, 600), random.Next(5, 600)), new Vector(random.Next(5, 600), random.Next(5, 600))));
            }
        }

        public void CastRays()
        {
            foreach (var ray in player.Rays)
            {
                var midDist = double.MaxValue;
                var bestPoint = new Vector();
                foreach (var wall in walls)
                {
                    var currentPoint = ray.Cast(wall);
                    if (!(currentPoint.X > 0) || !(currentPoint.Y > 0) || !(Utils.GetDist(currentPoint, ray.Pos) < midDist))
                    {
                        continue;
                    }

                    midDist = Utils.GetDist(currentPoint, ray.Pos);
                    bestPoint = currentPoint;
                }

                CastetReys[ray] = new Line(new Vector(ray.Pos.X, ray.Pos.Y), new Vector(bestPoint.X, bestPoint.Y));
            }
        }
    }
}