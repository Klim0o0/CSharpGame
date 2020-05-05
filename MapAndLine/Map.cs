using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using Game.EnemyAndPlayer;
using Game.MapAndLine;

namespace Game.MapAndLine
{
    public class Map
    {
        public readonly List<Wall> Walls;
        public readonly List<Enemy> Enemies;

        public Map(List<Enemy> enemies)
        {
            Enemies = enemies;
            Walls = new List<Wall>();
            CreateMap();
        }

        private void CreateMap()
        {
            Walls.Add(new Wall(new Line(new Vector(500, 200), new Vector(500, 400)), "wall"));
            Walls.Add(new Wall(new Line(new Vector(600, 1), new Vector(600, 600)), "wall"));
            Walls.Add(new Wall(new Line(new Vector(600, 600), new Vector(1, 600)), "wall"));
            Walls.Add(new Wall(new Line(new Vector(1, 600), new Vector(1, 1)), "wall"));
            Walls.Add(new Wall(new Line(new Vector(1, 1), new Vector(600, 1)), "wall"));
        }

        public void MoveEnemy()
        {
            foreach (var enemy in Enemies)
                enemy.Move();
        }
    }
}