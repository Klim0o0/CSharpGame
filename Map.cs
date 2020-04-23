using System;
using System.Collections.Generic;
using System.Windows;

namespace Game
{
    public class Map
    {
        readonly public List<Wall> walls;
        public Player player;
        public Tuple<Line,Wall>[] CastetReys;

        public Map(Player player)
        {
            this.player = player;
            CastetReys = new Tuple<Line,Wall>[player.Rays.Count];
            walls = new List<Wall>();
            walls.Add(new Wall(new Line(new Vector(500, 200), new Vector(500, 400))));
            walls.Add(new Wall(new Line(new Vector(600, 5), new Vector(600, 600))));
            walls.Add(new Wall(new Line(new Vector(600, 600), new Vector(5, 600))));
            walls.Add(new Wall(new Line(new Vector(5, 600), new Vector(5, 5))));
            walls.Add(new Wall(new Line(new Vector(5, 5), new Vector(600, 5))));
            var random = new Random();
            for (var i = 0; i < 5; i++)
            {
                walls.Add(new Wall(new Line(new Vector(random.Next(5, 600), random.Next(5, 600)), new Vector(random.Next(5, 600), random.Next(5, 600)))));
            }
        }

        public void CastRays()
        {
            for(var i =0;i<player.Rays.Count;i++)
            {
                var midDist = double.MaxValue;
                var bestPoint = new Vector();
                var bestWall = new Wall();
                var ray = player.Rays[i];
                foreach (var wall in walls)
                {
                    var currentPoint = ray.Cast(wall);
                    if (!(currentPoint.X > 0) || !(currentPoint.Y > 0) || !(Utils.GetDist(currentPoint, ray.Pos) < midDist))
                    {
                        continue;
                    }

                    midDist = Utils.GetDist(currentPoint, ray.Pos);
                    bestPoint = currentPoint;
                    bestWall = wall;
                }
                
                CastetReys[i] = Tuple.Create(new Line(new Vector(ray.Pos.X, ray.Pos.Y), new Vector(bestPoint.X, bestPoint.Y)),bestWall);
            }
        }
    }
}