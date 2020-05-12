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

        public HashSet<double> directions;
        public List<Tuple<Line, Wall>>[] CastedRays { get; }
        public Vector PlayerPos => new Vector(_player.X, _player.Y);
        public double PlayerDirection => _player.Direction;

        public Game(Map map, Player player)
        {
            _map = map;
            _player = player;
            CastedRays = new List<Tuple<Line, Wall>>[_player.Rays.Count];
            directions = new HashSet<double>();
        }

        private Vector CorrectVector(Vector moveVector)
        {
            foreach (var wall in _map.Walls.Concat(_map.Doors.Select(x=>x.DorWall)))
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

                    moveVector += normalVector.X * moveVector.X + normalVector.Y * moveVector.Y > 0
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
                foreach (var wall in _map.Walls.Concat(_map.Enemies.SelectMany(x => x.EnemyWalls).Concat(_map.Doors.Select(z=>z.DorWall))))
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
                var ray = new Ray(enemy.Position, 0) {Direction = vec};
                if (!_map.Walls.Select(x => ray.Cast(x)).Any(x => (enemy.Position - x).Length < (playerPos - enemy.Position).Length))
                {
                    var angle = Math.Asin(Math.Abs(vec.Y / vec.Length));
                    if (vec.X < 0 && vec.Y >= 0)
                        angle = Math.PI - angle;
                    else if(vec.X < 0 && vec.Y < 0)
                        angle = Math.PI + angle;
                    else if(vec.X > 0 && vec.Y < 0)
                        angle = 2* Math.PI - angle;
                    enemy.TurnTo(angle-Math.PI);
                    enemy.Move(vec);
                }
            }
        }

        public void Shot()
        {
            var ray = new Ray(PlayerPos, _player.Direction);
            var minDistance = double.MaxValue;
            var s = new Vector(-85, -85);
            foreach (var w in _map.Walls.Select(wall => ray.Cast(wall)).Where(w => minDistance > Utils.GetDist(w, PlayerPos) && w.X > 0))
            {
                minDistance = Utils.GetDist(w, PlayerPos);
            }

            var bestEnemy = new Enemy(s);

            foreach (var enemy in _map.Enemies)
            {
                foreach (var w in enemy.EnemyWalls.Select(wall => ray.Cast(wall)).Where(w => minDistance > Utils.GetDist(w, PlayerPos) && w.X > 0))
                {
                    bestEnemy = enemy;
                    minDistance = Utils.GetDist(w, PlayerPos);
                }
            }

            if (bestEnemy.Position.X != -85)
                _map.Enemies.Remove(bestEnemy);
        }

        public void PlayerMove()
        {
            var vec = new Vector(0, 0);
            foreach (var direction in directions)
            {
                vec += new Vector(Math.Cos(direction + _player.Direction), -Math.Sin(direction + _player.Direction));
            }
            if (vec.X != 0 && vec.Y != 0)
            {
                vec.Normalize();

                MoveTo(vec * _player.Speed);
            }
        }

        public void OpenDor()
        {
            foreach (var door in _map.Doors)
            {
                if ((PlayerPos - door.Position).Length<=50)
                {
                    _map.Doors.Remove(door);
                    break;
                }
            }
        }
    }
}