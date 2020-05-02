using System.Collections.Generic;
using System.Windows;
using Point = System.Drawing.Point;

namespace Game
{
    public class Enemy
    {
        public Vector Position { get; private set; }
        public double Direction { get; private set; }
        public List<Wall> w { get; private set; }

        private readonly int a = 20;

        public Enemy(Vector pos)
        {
            Direction = 0;
            Position = pos;
            w = new List<Wall>
            {
                new Wall(new Line(new Vector(pos.X - a, pos.Y - 1), new Vector(pos.X + a, pos.Y - 1)), "Enemy1Front"),
                new Wall(new Line(new Vector(pos.X - a, pos.Y + 1), new Vector(pos.X + a, pos.Y + 1)), "Enemy1Beak"),
                new Wall(new Line(new Vector(pos.X - a, pos.Y - 1), new Vector(pos.X - a, pos.Y + 1)), "Enemy1Side"),
                new Wall(new Line(new Vector(pos.X + a, pos.Y - 1), new Vector(pos.X + a, pos.Y + 1)), "Enemy1Side")
            };
        }

        public void Move()
        {
            for (var i = 0; i < w.Count; i++)
            {
                w[i].line.A += new Vector(-1, -1);
                w[i].line.B += new Vector(-1, -1);
            }
        }
    }
}