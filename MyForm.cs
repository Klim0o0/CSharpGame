using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;

namespace Game
{
    class MyForm : Form
    {
        List<Ray> rays = new List<Ray>();

        public MyForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1900, 1200);
            var player = new Player(10, 10, 0);
            var map = new Map(player);
            var centerX = ClientSize.Width / 2;
            var centerY = ClientSize.Height / 2;
            var h = 200;
            var d = 100;

            Paint += (sender, args) =>
            {
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                map.CastRays();
                foreach (var ray in map.CastetReys.Values)
                {
                    if (ray.B.X > 0 && ray.B.Y > 0)
                        args.Graphics.DrawLine(Pens.Blue, (float) ray.A.X, (float) ray.A.Y, (float) ray.B.X, (float) ray.B.Y);
                }

                args.Graphics.FillRectangle(Brushes.CornflowerBlue, 700, 0, (float) Math.PI * 200, 300);
                args.Graphics.FillRectangle(Brushes.Gray, 700, 300, (float) Math.PI * 200, 300);

                var ofset = 0.0f;
                foreach (var ray in map.CastetReys.Values)
                {
                    if (ray.B.X > 0 && ray.B.Y > 0)
                    {
                        var h1 = d / ray.Length * h;
                        h1 = h1 > 600 ? 600 : h1;
                        args.Graphics.FillRectangle(Brushes.WhiteSmoke, 700 + ofset, 300 - (float) h1 / 2, 2.5f, (float) h1);
                    }

                    ofset += 2f;
                }

                foreach (var wall in map.walls)
                {
                    args.Graphics.DrawLine(Pens.Blue, (float) wall.A.X, (float) wall.A.Y, (float) wall.B.X, (float) wall.B.Y);
                }
            };
            Cursor.Hide();
            Cursor.Position= new Point((int)( Math.PI * 100+700),300);
            var a = (int)( Math.PI * 100+700);
            MouseMove += (sender, args) =>
            {
                var g = a - args.X;
                if (Math.Abs(g) >40)
                {
                    if (g < 0)
                        player.TurnRigth();
                    else
                        player.TurnLeft();
                    Cursor.Position= new Point((int)( Math.PI * 100+700),300);
                    Invalidate();
                }

            };

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Right)
                {
                    player.TurnRigth();
                }

                if (args.KeyCode == Keys.Left)
                {
                    player.TurnLeft();
                }

                if (args.KeyCode == Keys.W)
                {
                    player.MoveForvard();
                }

                if (args.KeyCode == Keys.S)
                {
                    player.MoveBeak();
                    Invalidate();
                }

                if (args.KeyCode == Keys.A)
                {
                    player.MoveLeft();
                }

                if (args.KeyCode == Keys.D)
                {
                    player.MoveRigth();
                }

                Invalidate();
            };
        }
    }
}