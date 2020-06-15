using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Game.EnemyAndPlayer;
using Game.MapAndLine;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Game
{
    sealed class MyForm : Form
    {
        Dictionary<string, Bitmap> textures = new Dictionary<string, Bitmap>
        {
            {"wall", new Bitmap(Image.FromFile("wall.png"))},
            {"Enemy1Front", new Bitmap(Image.FromFile("matr.png"))},
            {"Enemy1Beak", new Bitmap(Image.FromFile("matr.png"))},
            {"Enemy1Side", new Bitmap(Image.FromFile("matr.png"))},
            {"Door", new Bitmap(Image.FromFile("Door.png"))}
        };

        public MyForm()
        {
            DoubleBuffered = true;
            
            ClientSize = new Size(950, 1000);
            var player = new Player(20, 20, 0.04);
            var map = new Map(Levels.level[1]);
            var game = new Game(map, player);
            const int distance = 20000;
            

            Paint += (sender, args) =>
                Drawer.DrawGame(args, game, textures, distance, map);

            var timer = new Timer {Interval = 10};
            timer.Tick += (sender, args) =>
            {
                game.MoveEnemies();
                game.PlayerMove();
                Invalidate();
            };
            timer.Start();
            
            KeyDown += (sender, args) => KeyDownAction(args, game);
            KeyUp += (sender, args) => KeyUpAction(args, game);
        }

        private static void KeyUpAction(KeyEventArgs args, Game game)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    game.Directions.Remove(0);
                    break;
                case Keys.S:
                    game.Directions.Remove(Math.PI);
                    break;
                case Keys.A:
                    game.Directions.Remove(Math.PI / 2);
                    break;
                case Keys.D:
                    game.Directions.Remove(Math.PI + Math.PI / 2);
                    break;
                case Keys.Left:
                    game.TurnAt-=0.05;
                    break;
                case Keys.Right:
                    game.TurnAt+=0.05;
                    break;
            }
        }

        private static void KeyDownAction(KeyEventArgs args, Game game)
        {
            switch (args.KeyCode)
            {
                case Keys.Right:
                    game.TurnAt=game.TurnAt!=-0.05?game.TurnAt-0.05:game.TurnAt;
                    break;
                case Keys.Left:
                    game.TurnAt =game.TurnAt!=0.05?game.TurnAt+0.05:game.TurnAt;
                    break;
                case Keys.E:
                    game.OpenDor();
                    break;
                case Keys.W:
                    game.Directions.Add(0);
                    break;
                case Keys.S:
                    game.Directions.Add(Math.PI);
                    break;
                case Keys.A:
                    game.Directions.Add(Math.PI / 2);
                    break;
                case Keys.D:
                    game.Directions.Add(Math.PI + Math.PI / 2);
                    break;
                case Keys.Space:
                    game.Shot();
                    break;
            }
        }

        
    }
}