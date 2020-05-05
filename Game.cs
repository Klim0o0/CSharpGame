using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Game.EnemyAndPlayer;
using Game.MapAndLine;
using Point = System.Drawing.Point;

namespace Game
{
    public class Game
    {
        private readonly Player _player;
        private readonly Map _map;
        public List<Tuple<Line, Wall>>[] CastedRays { get; }
        public Vector PlayerPos => new Vector(_player.X, _player.Y);
        public double PlayerDirection => _player.Direction;

        public Game(Map map, Player player)
        {
            _map = map;
            _player = player;
            CastedRays = new List<Tuple<Line, Wall>>[_player.Rays.Count];
        }

        private Vector CorrectVector(Vector moveVector)
        {
            foreach (var wall in _map.Walls)
            {
                var t = new Vector(moveVector.X, moveVector.Y);
                t.Normalize();
                var playerRay = new Ray(new Vector(_player.X, _player.Y), 0) {Direction = t};
                var distanceToWall = playerRay.Cast(wall);
                if (distanceToWall.X >= 0 && distanceToWall.Y >= 0 && moveVector.Length >
                    Utils.GetDist(new Vector(_player.X, _player.Y), distanceToWall))
                {
                    var x1 = wall.line.A.X;
                    var x2 = wall.line.B.X;
                    var y1 = wall.line.A.Y;
                    var y2 = wall.line.B.Y;
                    var normalX = y2 - y1;
                    var normalY = x1 - x2;
                    var c = x1 * (y1 - y2) + y1 * (x2 - x1);
                    var px = moveVector.X + _player.X;
                    var py = moveVector.Y + _player.Y;
                    var distance = Math.Abs(normalX * px + normalY * py + c) / Math.Sqrt(normalX * normalX + normalY * normalY) + 0.1;
                    var normalVector = new Vector(normalX, normalY);
                    normalVector.Normalize();
                    normalVector *= distance;
                    moveVector += normalVector.X * moveVector.X + normalVector.Y * moveVector.Y >= 0
                        ? -normalVector
                        : normalVector;
                }
            }

            return moveVector;
        }

        public void TurnIn(double angle) => _player.TurnAt(angle);
        public void MoveTo(Vector newMoveVector) => _player.Move(CorrectVector(CorrectVector(newMoveVector)));

        public void CastRays()
        {
            for (var i = 0; i < CastedRays.Length; i++)
                CastedRays[i] = new List<Tuple<Line, Wall>>();

            for (var i = 0; i < _player.Rays.Count; i++)
            {
                var ray = _player.Rays[i];
                foreach (var wall in _map.Walls.Concat(_map.Enemies.SelectMany(x => x.EnemyWalls)))
                {
                    var currentPoint = ray.Cast(wall);
                    if (currentPoint.X > 0 && currentPoint.Y > 0)
                        CastedRays[i].Add(Tuple.Create(
                                              new Line(new Vector(ray.Pos.X, ray.Pos.Y), new Vector(currentPoint.X, currentPoint.Y)),
                                              wall));
                }

                CastedRays[i] = CastedRays[i].OrderBy(x => -Utils.GetDist(x.Item1.B, ray.Pos)).ToList();
            }
        }

        public void MoveEnemys()
        {
            var playerPos = new Vector(_player.X, _player.Y);
            foreach (var enemy in _map.Enemies)
            {
                var vec = playerPos - enemy.Position;
                vec.Normalize();
                enemy.Move(vec);
            }
        }
    }
}