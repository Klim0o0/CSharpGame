using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace Game
{
    public class Map
    {
        readonly public List<Wall> walls;
        readonly public List<Enemy> Enemys;

        public Map(List<Enemy> enemys, List<Wall> walls)
        {
            Enemys = enemys;
            this.walls = new List<Wall>();
            CreateMap();
            this.walls = this.walls.Concat(walls).ToList();
        }

        public Map(List<Enemy> enemys)
        {
            Enemys = enemys;
            this.walls = new List<Wall>();
            CreateMap();
        }

        private void CreateMap()
        {
            var texture = "wall";
            this.walls.Add(new Wall(new Line(new Vector(500, 200), new Vector(500, 400)), texture));
            this.walls.Add(new Wall(new Line(new Vector(600, 1), new Vector(600, 600)), texture));
            this.walls.Add(new Wall(new Line(new Vector(600, 600), new Vector(1, 600)), texture));
            this.walls.Add(new Wall(new Line(new Vector(1, 600), new Vector(1, 1)), texture));
            this.walls.Add(new Wall(new Line(new Vector(1, 1), new Vector(600, 1)), texture));
        }

        public void MoveEnemy()
        {
            foreach (var enemy in Enemys)
            {
                enemy.Move();
            }
        }
    }
}