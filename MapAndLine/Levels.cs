using System;
using System.Collections.Generic;
using System.Windows;
using Game.EnemyAndPlayer;

namespace Game.MapAndLine
{
    public static class Levels
    {
        public static Dictionary<int, Level> level = new Dictionary<int, Level>()
        {
            [1] = new Level()
            {
                Walls =new List<Wall>()
                {
                new Wall(new Line(new Vector(5, 5), new Vector(5, 2000)), "wall"),
                new Wall(new Line(new Vector(5, 5), new Vector(2000, 5)), "wall"),
                new Wall(new Line(new Vector(2000, 2000), new Vector(2000, 5)), "wall"),
                new Wall(new Line(new Vector(2000, 2000), new Vector(5, 2000)), "wall"),
                
                new Wall(new Line(new Vector(5, 100), new Vector(500, 100)), "wall"),
                new Wall(new Line(new Vector(500, 100), new Vector(500, 700)), "wall"),
                new Wall(new Line(new Vector(500, 700), new Vector(105, 700)), "wall"),
                
                new Wall(new Line(new Vector(800, 100), new Vector(800, 1300)), "wall"),
                new Wall(new Line(new Vector(700, 1000), new Vector(105, 1000)), "wall"),
                new Wall(new Line(new Vector(105, 1000), new Vector(105, 1300)), "wall"),
                new Wall(new Line(new Vector(105, 1300), new Vector(1500, 1300)), "wall"),
                new Wall(new Line(new Vector(1200, 1300), new Vector(1200, 2000)), "wall"),
                new Wall(new Line(new Vector(1500, 1300), new Vector(1500, 900)), "wall"),
                new Wall(new Line(new Vector(1500, 800), new Vector(1500, 100)), "wall"),
                new Wall(new Line(new Vector(1500, 100), new Vector(800, 100)), "wall"),
                new Wall(new Line(new Vector(1500, 900), new Vector(1700, 900)), "wall"),
                new Wall(new Line(new Vector(1800, 900), new Vector(2000, 900)), "wall"),
                },
                Enemies = new HashSet<Enemy>()
                {
                    new Enemy(new Vector(200, 200))
                },
                Doors = new HashSet<Door>()
                {
                    new Door(new Vector( 55,700),1,Math.PI/2),
                    new Door(new Vector( 750,1000),2,Math.PI/2),
                    new Door(new Vector( 1500,850), 3,0),
                    new Door(new Vector( 1750,900), 4,Math.PI/2),
                }
            }
        };
    }
}