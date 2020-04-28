using System.Drawing;

namespace Game
{
    public class Wall
    {
        public readonly Line line;
        public readonly Bitmap Textures;
        public Wall(Line line,Image texture)
        {
            this.line = line;
            Textures = new Bitmap(texture);
        }
        public Wall()
        {
        }
    }
}