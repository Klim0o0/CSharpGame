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
        private static float mast = 10;
        public MyForm()
        {
            DoubleBuffered = true;
            Cursor.Hide();
            var enemies = new List<Enemy> {new Enemy(new Vector(400, 400))};
            Cursor.Position = new Point((int) (Math.PI * 100), 300);

            var textures = new Dictionary<string, Bitmap>
            {
                {"wall", new Bitmap(Image.FromFile("wall.png"))},
                {"Enemy1Front", new Bitmap(Image.FromFile("matr.png"))},
                {"Enemy1Beak", new Bitmap(Image.FromFile("wall.png"))},
                {"Enemy1Side", new Bitmap(Image.FromFile("matr.png"))}
            };

            ClientSize = new Size(1900, 1200);
            var player = new Player(20, 20, 0.04);
            var map = new Map(enemies, Level.level[1]);
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
                var mousePosition = (int) (Math.PI * 100) - args.X;
                if (Math.Abs(mousePosition) > 20)
                {
                    game.TurnIn(mousePosition < 0 ? -0.05 : 0.05);
                    Cursor.Position = new Point((int) (Math.PI * 100), 300);
                }
            };

            MouseClick += (sender, args) => game.Shot();
        }

        private void DrawGame(Game game, Dictionary<string, Bitmap> textures, int distance, int heights, Map map)
        {
            Paint += (sender, args) =>
            {
                args.Graphics.FillRectangle(Brushes.White, 0, 0, ClientSize.Width, ClientSize.Height);
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                game.CastRays();
                
                DrawPlayerView(args, game, textures, distance, heights);
                DrawRays(game, args);

                DrawRayScreenWall(map, args);

                args.Graphics.FillEllipse(Brushes.Aqua,
                    new Rectangle((int) (game.PlayerPos.X/mast )- 2, (int) (game.PlayerPos.Y/mast )- 2, 4, 4));

                DrawAim(args);
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

        private static void DrawAim(PaintEventArgs args)
        {
            const float centreX =((float) Math.PI * 201) / 2;
            const int centre = 300;

            args.Graphics.FillRectangle(Brushes.GreenYellow, centreX - 1, centre - 1, 2, 2);
        }

        private static void DrawRayScreenWall(Map map, PaintEventArgs args)
        {
            foreach (var wall in map.Walls)
            {
                args.Graphics.DrawLine(Pens.Blue, (float) wall.line.A.X/mast, (float) wall.line.A.Y/mast,
                    (float) wall.line.B.X/mast, (float) wall.line.B.Y/mast);
            }
        }

        private static void DrawPlayerView(PaintEventArgs args, Game game, Dictionary<string, Bitmap> textures,
            int distance, int heights)
        {
            args.Graphics.FillRectangle(Brushes.CornflowerBlue, 0, 0, (float) Math.PI * 201, 300);
            args.Graphics.FillRectangle(Brushes.Gray, 0, 300, (float) Math.PI * 201, 300);

            var offset = 0;
            foreach (var tuples in game.CastedRays)
            {
                foreach (var (ray, wall) in tuples)
                {
                    if (ray.B.X > 0 && ray.B.Y > 0)
                    {
                        var bmp = textures[wall.Name];
                        var d1 = ray.Length;
                        var h1 = distance/ d1 * heights;
                        var width = bmp.Width;
                        var height = bmp.Height;
                        args.Graphics.DrawImage(
                            bmp, new RectangleF(0 + offset, 300 - (float) h1 / 2, 3, (float) h1),
                            new RectangleF((int) Utils.GetDist(wall.line.A, ray.B) % (width - 1), 0, 2, height),
                            GraphicsUnit.Pixel);
                    }
                }
                offset += 2;
            }

            args.Graphics.FillRectangle(Brushes.White, 0, 600, (float) Math.PI * 201, 800);
        }

        private static void DrawRays(Game game, PaintEventArgs args)
        {
            foreach (var tuple in game.CastedRays)
            {
                if (tuple.Count == 0)
                    continue;
                var ray = tuple.Last().Item1;
                if (ray.B.X > 0 && ray.B.Y > 0)
                    args.Graphics.DrawLine(Pens.Blue, (float) ray.A.X/mast, (float) ray.A.Y/mast, (float) ray.B.X/mast,
                        (float) ray.B.Y/mast);
            }
        }
    }
}