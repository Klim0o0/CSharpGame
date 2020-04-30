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
            Cursor.Hide();
            Cursor.Position = new Point((int) (Math.PI * 100 + 700), 300);
            
            var textures = new Dictionary<string,Bitmap>();
            textures.Add("wall", new Bitmap(Image.FromFile("wall.png")));
            
            ClientSize = new Size(1900, 1200);
            var player = new Player(7, 7, 0);
            var map = new Map(player);
            var centerX = ClientSize.Width / 2;
            var centerY = ClientSize.Height / 2;
            var h = 100;
            var d = 200;
            Paint += (sender, args) =>
            {
                args.Graphics.FillRectangle(Brushes.White, 0, 0, ClientSize.Width, ClientSize.Height);
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                map.CastRays();
                foreach (var tuple in map.CastetReys)
                {
                    var ray = tuple.Item1;
                    if (ray.B.X > 0 && ray.B.Y > 0)
                        args.Graphics.DrawLine(Pens.Blue, (float) ray.A.X, (float) ray.A.Y, (float) ray.B.X, (float) ray.B.Y);
                }

                args.Graphics.FillRectangle(Brushes.CornflowerBlue, 700, 0, (float) Math.PI * 200, 300);
                args.Graphics.FillRectangle(Brushes.Gray, 700, 300, (float) Math.PI * 200, 300);

                var ofset = 0;
                var angle = -Math.PI / 4;
                foreach (var (ray, item2) in map.CastetReys)
                {
                    if (ray.B.X > 0 && ray.B.Y > 0)
                    {
                        var bmp = textures[item2.Name];
                        var d1 = ray.Length;
                        var h1 = d / d1 * h;
                        //h1 = h1 % 8000;
                        var width =  bmp.Width;
                        var height =  bmp.Height;
                        var destinationRect = new RectangleF(
                            700 + ofset,
                            300 - (float) h1 / 2,
                            4,
                            (float) h1);

                        var sourceRect = new RectangleF((int) Utils.GetDist(item2.line.A, ray.B) % (width -1), 0, 2, height);

                        args.Graphics.DrawImage(
                            bmp,
                            destinationRect,
                            sourceRect,
                            GraphicsUnit.Pixel);
                        //var rec = new RectangleF(0+ofset,0, 0.4f*bmp.Width,0.4f*bmp.Height);

                        //args.Graphics.DrawImage(bmp, 700 + ofset, 300 - (float) h1 / 2,rec , GraphicsUnit.Pixel);
                        //args.Graphics.FillRectangle(Brushes.WhiteSmoke, 700 + ofset, 300 - (float) h1 / 2, 2.5f, (float) h1);
                    }

                    ofset += 2;
                    angle += Player.RayStep;
                }

                args.Graphics.FillRectangle(Brushes.White, 700, 600, (float) Math.PI * 200 + 1, 800);
                foreach (var wall in map.walls)
                {
                    args.Graphics.DrawLine(Pens.Blue, (float) wall.line.A.X, (float) wall.line.A.Y, (float) wall.line.B.X, (float) wall.line.B.Y);
                }
                
                args.Graphics.FillEllipse(Brushes.Aqua, new Rectangle((int)player.X-5,(int)player.Y-5,10,10));
            };
            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();
            
            
            var a = (int) (Math.PI * 100 + 700);
            MouseMove += (sender, args) =>
            {
                var g = a - args.X;
                if (Math.Abs(g) > 20)
                {
                    if (g < 0)
                        player.TurnRight();
                    else
                        player.TurnLeft();
                    Cursor.Position = new Point((int) (Math.PI * 100 + 700), 300);
                }
            };

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Right)
                {
                    player.TurnRight();
                }

                if (args.KeyCode == Keys.Left)
                {
                    player.TurnLeft();
                }

                if (args.KeyCode == Keys.W)
                {
                    player.MoveForward(map.walls);
                }

                if (args.KeyCode == Keys.S)
                {
                    player.MoveBeak(map.walls);
                    Invalidate();
                }

                if (args.KeyCode == Keys.A)
                {
                    player.MoveLeft(map.walls);
                }

                if (args.KeyCode == Keys.D)
                {
                    player.MoveRight(map.walls);
                }
            };
        }
    }
}