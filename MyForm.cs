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
            var enemies = new List<Enemy> {new Enemy(new Vector(400, 400))};
            Cursor.Position = new Point((int) (Math.PI * 100 + 700), 300);

            var textures = new Dictionary<string, Bitmap>
            {
                {"wall", new Bitmap(Image.FromFile("wall.png"))},
                {"Enemy1Front", new Bitmap(Image.FromFile("matr.png"))},
                {"Enemy1Beak", new Bitmap(Image.FromFile("matr.png"))},
                {"Enemy1Side", new Bitmap(Image.FromFile("matr.png"))}
            };

            ClientSize = new Size(1900, 1200);
            var player = new Player(10, 10, 0);
            var map = new Map(enemies, Level.level[1]);
            var game = new Game(map, player);
            const int heights = 100;
            const int distance = 200;
            DrawGame(game, textures, distance, heights, map);
            var timer = new Timer {Interval = 10};
            timer.Tick += delegate
            {
                game.MoveEnemys();
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
                var mousePosition = (int) (Math.PI * 100 + 700) - args.X;
                if (Math.Abs(mousePosition) > 20)
                {
                    game.TurnIn(mousePosition < 0 ? -0.05 : 0.05);
                    Cursor.Position = new Point((int) (Math.PI * 100 + 700), 300);
                }
            };
        }

        private void DrawGame(Game game, Dictionary<string, Bitmap> textures, int distance, int heights, Map map)
        {
            Paint += (sender, args) =>
            {
                args.Graphics.FillRectangle(Brushes.White, 0, 0, ClientSize.Width, ClientSize.Height);
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                game.CastRays();
                DrawRays(game, args);

                DrawPlayerView(args, game, textures, distance, heights);
                DrawRayScreenWall(map, args);

                args.Graphics.FillEllipse(Brushes.Aqua,
                    new Rectangle((int) game.PlayerPos.X - 5, (int) game.PlayerPos.Y - 5, 10, 10));

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
                        game.MoveTo(new Vector(Math.Cos(player.Direction),
                            -Math.Sin(player.Direction)) * player.Speed);
                        break;
                    case Keys.S:
                        game.MoveTo(new Vector(-Math.Cos(player.Direction),
                            Math.Sin(player.Direction)) * player.Speed);
                        break;
                    case Keys.A:
                        game.MoveTo(new Vector(Math.Cos(player.Direction + Math.PI / 2),
                            -Math.Sin(player.Direction + Math.PI / 2)) * player.Speed);
                        break;
                    case Keys.D:
                        game.MoveTo(new Vector(-Math.Cos(player.Direction + Math.PI / 2),
                            Math.Sin(player.Direction + Math.PI / 2)) * player.Speed);
                        break;
                }
            };
        }

        private static void DrawAim(PaintEventArgs args)
        {
            const float centreX = 699 + ((float) Math.PI * 201) / 2;
            const int centre = 300;

            args.Graphics.FillRectangle(Brushes.GreenYellow, centreX - 1, centre - 1, 2, 2);
        }

        private static void DrawRayScreenWall(Map map, PaintEventArgs args)
        {
            foreach (var wall in map.Walls)
            {
                args.Graphics.DrawLine(Pens.Blue, (float) wall.line.A.X, (float) wall.line.A.Y,
                    (float) wall.line.B.X, (float) wall.line.B.Y);
            }
        }

        private static void DrawPlayerView(PaintEventArgs args, Game game, Dictionary<string, Bitmap> textures,
            int distance, int heights)
        {
            args.Graphics.FillRectangle(Brushes.CornflowerBlue, 700, 0, (float) Math.PI * 201, 300);
            args.Graphics.FillRectangle(Brushes.Gray, 700, 300, (float) Math.PI * 201, 300);

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
                            bmp, new RectangleF(700 + offset, 300 - (float) h1 / 2, 3, (float) h1),
                            new RectangleF((int) Utils.GetDist(wall.line.A, ray.B) % (width - 1), 0, 2, height),
                            GraphicsUnit.Pixel);
                    }
                }
                offset += 2;
            }

            args.Graphics.FillRectangle(Brushes.White, 699, 600, (float) Math.PI * 201, 800);
        }

        private static void DrawRays(Game game, PaintEventArgs args)
        {
            foreach (var tuple in game.CastedRays)
            {
                if (tuple.Count == 0)
                    continue;
                var ray = tuple.Last().Item1;
                if (ray.B.X > 0 && ray.B.Y > 0)
                    args.Graphics.DrawLine(Pens.Blue, (float) ray.A.X, (float) ray.A.Y, (float) ray.B.X,
                        (float) ray.B.Y);
            }
        }
    }
}