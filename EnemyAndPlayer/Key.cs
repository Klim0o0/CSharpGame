using System.Windows;
using Game.MapAndLine;

namespace Game.EnemyAndPlayer
{
    public class Key
    {
        public Vector Position{ get; private set; }
        public Wall KeyWall { get; private set; }

        public Key(Vector position)
        {
            Position = position;
            
        }
    }
}