using System.Drawing;

namespace Game
{
    public class Wall
    {
        public readonly Line line;
        public readonly Image[] Textures;
        public readonly int TextureWidth;
        public Wall(Line line,Image texture)
        {
            this.line = line;
            Textures = new Image[texture.Width/2];
        }
        public Wall(Line line)
        {
            this.line = line;
        }
        public Wall()
        {
        }
    }
}