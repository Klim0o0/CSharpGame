using System;
using System.Windows;
using Game.MapAndLine;
using NUnit.Framework;

namespace Game.EnemyAndPlayer
{
    public class Door
    {
        public int Key { get; private set; }
        public Vector Position{ get; private set; }
        //public bool IsOpen { get; private set; }
        
        public Wall DorWall { get; private set; }
        private double startPos;
        private double startPos2;
        
        public Door(Vector position, int key, double direction)
        {
            Key = key;
            Position = position;
            DorWall = new Wall(new Line(new Vector(Position.X + 50 * Math.Cos(direction + Math.PI / 2), Position.Y + 50 * Math.Sin(direction + Math.PI / 2)),
                                        new Vector(Position.X +50 * Math.Cos(direction - Math.PI / 2), Position.Y + 50 * Math.Sin(direction - Math.PI / 2))), "Door");
            //IsOpen = false;
            startPos = DorWall.line.A.X - DorWall.line.B.X == 0 ? position.Y : position.X;
            startPos2 = 0;
        }
    }
}