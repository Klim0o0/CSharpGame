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

        public Map(List<Enemy> enemies, List<Wall> walls)
        {
            Enemies = enemies;
            Walls = walls;
        }
    }
}