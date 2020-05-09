using System;
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

        private readonly int a = 32;
        private readonly int speed = 1;

        public Enemy(Vector pos)
        {
            Direction = Math.PI / 4;
            Position = pos;
            
            var a2 = Math.Sqrt(a * a + 1);
            var ofset = Math.Asin(a / a2);

            EnemyWalls = new List<Wall>
            {
                new Wall(
                    new Line(new Vector(Position.X + a * Math.Cos(Direction + Math.PI / 2), Position.Y + a * Math.Sin(Direction + Math.PI / 2)),
                             new Vector(Position.X + a * Math.Cos(Direction - Math.PI / 2), Position.Y + a * Math.Sin(Direction - Math.PI / 2))), "Enemy1Front"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction + ofset), Position.Y + a2 * Math.Sin(Direction + ofset)),
                             new Vector(Position.X + a2 * Math.Cos(Direction - ofset), Position.Y + a2 * Math.Sin(Direction - ofset))), "Enemy1Beak"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction + ofset), Position.Y + a2 * Math.Sin(Direction + ofset)),
                             new Vector(Position.X + a * Math.Cos(Direction + Math.PI / 2), Position.Y + a * Math.Sin(Direction + Math.PI / 2))), "Enemy1Side"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction - ofset), Position.Y + a2 * Math.Sin(Direction - ofset)),
                             new Vector(Position.X + a * Math.Cos(Direction - Math.PI / 2), Position.Y + a * Math.Sin(Direction - Math.PI / 2))), "Enemy1Side"),
            };
        }

        public void Move(Vector dir)
        {
            Position += dir * speed;
            foreach (var t in EnemyWalls)
            {
                t.line.A += dir * speed;
                t.line.B += dir * speed;
            }
        }

        public void TurnTo(double direction)
        {
            Direction = direction;

            var a2 = Math.Sqrt(a * a + 1);
            var ofset = Math.Asin(a / a2);
            EnemyWalls = new List<Wall>
            {
                new Wall(
                    new Line(new Vector(Position.X + a * Math.Cos(Direction + Math.PI / 2), Position.Y + a * Math.Sin(Direction + Math.PI / 2)),
                             new Vector(Position.X + a * Math.Cos(Direction - Math.PI / 2), Position.Y + a * Math.Sin(Direction - Math.PI / 2))), "Enemy1Front"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction + ofset), Position.Y + a2 * Math.Sin(Direction + ofset)),
                             new Vector(Position.X + a2 * Math.Cos(Direction - ofset), Position.Y + a2 * Math.Sin(Direction - ofset))), "Enemy1Beak"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction + ofset), Position.Y + a2 * Math.Sin(Direction + ofset)),
                             new Vector(Position.X + a * Math.Cos(Direction + Math.PI / 2), Position.Y + a * Math.Sin(Direction + Math.PI / 2))), "Enemy1Side"),
                new Wall(
                    new Line(new Vector(Position.X + a2 * Math.Cos(Direction - ofset), Position.Y + a2 * Math.Sin(Direction - ofset)),
                             new Vector(Position.X + a * Math.Cos(Direction - Math.PI / 2), Position.Y + a * Math.Sin(Direction - Math.PI / 2))), "Enemy1Side"),
            };
        }
    }
}