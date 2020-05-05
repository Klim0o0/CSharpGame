using System.Drawing;
using Game.MapAndLine;

namespace Game.MapAndLine
{
    public class Wall
    {
        public readonly Line line;
        public readonly string Name;

        public Wall(Line line, string name)
        {
            this.line = line;
            Name = name;
        }
    }
}