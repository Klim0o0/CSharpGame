using System.Collections.Generic;
using System.Windows;

namespace Game.MapAndLine
{
    public static  class Level
    {
        static string texture = "wall";

        public static Dictionary<int, List<Wall>> level = new Dictionary<int, List<Wall>>
        {
            [1] = new List<Wall>()
            {
                new Wall(new Line(new Vector(500, 200), new Vector(500, 400)), texture),
                new Wall(new Line(new Vector(600, 5), new Vector(600, 600)), texture),
                new Wall(new Line(new Vector(600, 600), new Vector(5, 600)), texture),
                new Wall(new Line(new Vector(5, 600), new Vector(5, 5)), texture),
                new Wall(new Line(new Vector(5, 600), new Vector(5, 5)), texture),
                new Wall(new Line(new Vector(5, 5), new Vector(600, 5)), texture)
            }
        };
    }
}