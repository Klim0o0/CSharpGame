using System.Drawing;

namespace Game
{
    public class Wall
    {
        public readonly Line line;
        public readonly string Name;
        public Wall(Line line,string name)
        {
            this.line = line;
            Name = name;
        }
        public Wall()
        {
        }
    }
}