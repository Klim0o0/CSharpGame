using System;
using System.Collections.Generic;
using System.Windows;
using Game.EnemyAndPlayer;
using Game.MapAndLine;
using NUnit.Framework;

namespace Game.Test
{
    [TestFixture]
    public class TestPlayerAndEnemy
    {
        static Player testPlayer = new Player(10, 10, 0);
        static List<Enemy> enemies = new List<Enemy> {new Enemy(new Vector(400, 400))};
        static Map map = new Map(Levels.level[1]);
        Game game = new Game(map, testPlayer);

        [Test]
        public void CreatePlayer() =>
            Assert.AreEqual(new Player(10, 10, 0).X, testPlayer.X);

        [Test]
        public void TurnPLayer()
        {
            game.TurnIn(-0.05);
            Assert.AreEqual(-0.05, testPlayer.Direction);
        }

        [Test]
        public void TestMovePlayer()
        {
            game.MoveTo(new Vector(Math.Cos(testPlayer.Direction),
                                   -Math.Sin(testPlayer.Direction)) * testPlayer.Speed);
            game.MoveTo(new Vector(Math.Cos(testPlayer.Direction + Math.PI / 2),
                                   -Math.Sin(testPlayer.Direction + Math.PI / 2)) * testPlayer.Speed);
            Assert.AreEqual(new Tuple<float, float>(13.0f, 7.0f), new Tuple<float, float>(testPlayer.X, testPlayer.Y));
        }

        [Test]
        public void TestChangeRay()
        {
            var expect = testPlayer.Rays;
            game.TurnIn(-0.05);
            game.TurnIn(0.05);
            Assert.AreEqual(expect, testPlayer.Rays);
        }
    }

    [TestFixture]
    public class TestObject
    {
        static readonly Line testLine = new Line(new Vector(1, 1), new Vector(5, 5));

        [Test]
        public void TestLength() => Assert.AreEqual(Math.Sqrt(32), testLine.Length);
    }
}