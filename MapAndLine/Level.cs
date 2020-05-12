using System.Collections.Generic;
using System.Windows;
using Game.EnemyAndPlayer;

namespace Game.MapAndLine
{
    public class Level
    {
        public List<Wall> Walls;
        public HashSet<Enemy> Enemies;
        public HashSet<Door> Doors;
    }
}