using System.Collections.Generic;
using System.Windows;
using Game.MapAndLine;

namespace Game.EnemyAndPlayer
{
    public class Enemy
    {
        public Vector Position { get; private set; }
        public double Direction { get; private set; }
        public List<Wall> EnemyWalls { get; private set; }

        private readonly int a = 64;

        public Enemy(Vector pos)
        {
            Direction = 0;
            Position = pos;
            EnemyWalls = new List<Wall>
            {
                new Wall(new Line(new Vector(pos.X - a, pos.Y - 1), new Vector(pos.X + a, pos.Y - 1)), "Enemy1Front"),
                new Wall(new Line(new Vector(pos.X - a, pos.Y + 1), new Vector(pos.X + a, pos.Y + 1)), "Enemy1Beak"),
                new Wall(new Line(new Vector(pos.X - a, pos.Y - 1), new Vector(pos.X - a, pos.Y + 1)), "Enemy1Side"),
                new Wall(new Line(new Vector(pos.X + a, pos.Y - 1), new Vector(pos.X + a, pos.Y + 1)), "Enemy1Side")
            };
        }

        public void Move()
        {
            foreach (var t in EnemyWalls)
            {
                t.line.A += new Vector(-1, -1);
                t.line.B += new Vector(-1, -1);
            }
        }
    }
}