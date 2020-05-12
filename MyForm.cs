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
        public MyForm()
        {
            DoubleBuffered = true;
            Cursor.Hide();
            Cursor.Position = new Point((int) (Math.PI * 100), 300);

            var textures = new Dictionary<string, Bitmap>
            {
                {"wall", new Bitmap(Image.FromFile("wall.png"))},
                {"Enemy1Front", new Bitmap(Image.FromFile("matr.png"))},
                {"Enemy1Beak", new Bitmap(Image.FromFile("matr.png"))},
                {"Enemy1Side", new Bitmap(Image.FromFile("matr.png"))},
                {"Door", new Bitmap(Image.FromFile("Door.png"))}
                
            };
            ClientSize = new Size(950, 1000);
            var player = new Player(20, 20, 0.04);
            var map = new Map(Levels.level[1]);
            var game = new Game(map, player);
            const int heights = 100;
            const int distance = 200;
            DrawGame(game, textures, distance, heights, map);
            var timer = new Timer {Interval = 10};
            timer.Tick += delegate
            {
                game.MoveEnemys();
                game.PlayerMove();
                Invalidate();
            };
            timer.Start();
            MouseAction(game);
            KeyAction(game, player);
            
        }

        private void MouseAction(Game game)
        {
            MouseMove += (sender, args) =>
            {
                var mousePosition = 950/2 - args.X;
                if (Math.Abs(mousePosition) > 60)
                {
                    game.TurnIn(mousePosition < 0 ? -0.05 : 0.05);
                    Cursor.Position = new Point(950/2+400, 500);
                }
            };

            MouseClick += (sender, args) => game.Shot();
        }

        private void DrawGame(Game game, Dictionary<string, Bitmap> textures, int distance, int heights, Map map)
        {
            Paint += (sender, args) =>
            {
                Location = new Point(400,0);
                args.Graphics.FillRectangle(Brushes.White, 0, 0, ClientSize.Width, ClientSize.Height);
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                game.CastRays();
                
                Drawer.DrawPlayerView(args, game, textures, distance, heights);
                Drawer.DrawRays(game, args);
                Drawer.DrawRayScreenWall(map, args);
                Drawer.DrawPlayer(map,args,game.PlayerPos);
                Drawer.DrawAim(args);
            };
        }

        private void KeyAction(Game game, Player player)
        {
            KeyDown += (sender, args) =>
            {
                switch (args.KeyCode)
                {
                    case Keys.Right:
                        game.TurnIn(-0.05);
                        break;
                    case Keys.Left:
                        game.TurnIn((0.05));
                        break;
                    case Keys.E:
                        game.OpenDor();
                        break;
                    case Keys.W:
                        game.directions.Add(0);
                        break;
                    case Keys.S:
                        game.directions.Add(Math.PI);
                        break;
                    case Keys.A:
                        game.directions.Add(Math.PI/2);
                        break;
                    case Keys.D:
                        game.directions.Add(Math.PI+Math.PI/2);
                        break;
                }
            };
            KeyUp += (sender, args) =>
            {
                switch (args.KeyCode)
                {
                    case Keys.W:
                        game.directions.Remove(0);
                        break;
                    case Keys.S:
                        game.directions.Remove(Math.PI);
                        break;
                    case Keys.A:
                        game.directions.Remove(Math.PI/2);
                        break;
                    case Keys.D:
                        game.directions.Remove(Math.PI+Math.PI/2);
                        break;
                }
            };
        }
    }
}