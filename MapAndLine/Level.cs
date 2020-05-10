using System.Collections.Generic;
using System.Windows;

namespace Game.MapAndLine
{
    public static class Level
    {
        static string texture = "wall";

        public static Dictionary<int, List<Wall>> level = new Dictionary<int, List<Wall>>
        {
            [1] = new List<Wall>()
            {
                new Wall(new Line(new Vector(600, 200), new Vector(5, 200)), texture),
                new Wall(new Line(new Vector(2000, 5), new Vector(2000, 2000)), texture),
                new Wall(new Line(new Vector(600, 2000), new Vector(600, 200)), texture),
                new Wall(new Line(new Vector(2000, 2000), new Vector(5, 2000)), texture),
                new Wall(new Line(new Vector(5, 2000), new Vector(5, 5)), texture),
                new Wall(new Line(new Vector(2000, 300), new Vector(1500, 300)), texture),
                new Wall(new Line(new Vector(1500, 300), new Vector(1500, 600)), texture),
                new Wall(new Line(new Vector(1500, 600), new Vector(2000, 600)), texture),
                new Wall(new Line(new Vector(5, 5), new Vector(2000, 5)), texture),
                new Wall(new Line(new Vector(800, 2000), new Vector(800, 1200)), texture),
                new Wall(new Line(new Vector(800, 1200), new Vector(2000, 1200)), texture),
                new Wall(new Line(new Vector(1000, 400), new Vector(800, 500)), texture),
                new Wall(new Line(new Vector(800, 500), new Vector(750, 750)), texture),
                new Wall(new Line(new Vector(750, 750), new Vector(950, 850)), texture),
                new Wall(new Line(new Vector(950, 850), new Vector(1150, 800)), texture),
                new Wall(new Line(new Vector(1150, 800), new Vector(1150, 450)), texture),
                new Wall(new Line(new Vector(1150, 450), new Vector(1000, 400)), texture)
            }
        };
    }
}