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
        public readonly HashSet<Enemy> Enemies;
        public readonly HashSet<Door> Doors;
        public Map(Level level)
        {
            Enemies = level.Enemies;
            Walls = level.Walls;
            Doors = level.Doors;
        }
    }
}