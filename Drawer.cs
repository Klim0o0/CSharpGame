using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Game.MapAndLine;

namespace Game
{
    public static class Drawer
    {
        private static float mast = 10;
        private static float m = 2;

        public static void DrawPlayerView(PaintEventArgs args, Game game, Dictionary<string, Bitmap> textures,
                                          int distance, int heights)
        {
            args.Graphics.FillRectangle(Brushes.CornflowerBlue, 0, 0, 1000 / game.CastedRays.Length * game.CastedRays.Length + 10, 500);
            args.Graphics.FillRectangle(Brushes.Gray, 0, 500, 1000 / game.CastedRays.Length * game.CastedRays.Length + 10, 500);

            var offset = 0;
            foreach (var tuples in game.CastedRays)
            {
                foreach (var (ray, wall) in tuples)
                {
                    if (ray.B.X > 0 && ray.B.Y > 0)
                    {
                        var bmp = textures[wall.Name];
                        var d1 = ray.Length;
                        var h1 = (distance / d1 * heights) * 2;
                        var width = bmp.Width;
                        var height = bmp.Height;
                        args.Graphics.DrawImage(
                            bmp, new RectangleF(0 + offset, 500 - (float) h1 / 2, 6, (float) h1),
                            new RectangleF((int) Utils.GetDist(wall.line.A, ray.B) % (width - 1), 0, 1, height),
                            GraphicsUnit.Pixel);
                    }
                }

                offset += 1000 / game.CastedRays.Length;
            }

            //args.Graphics.FillRectangle(Brushes.White, 0, 600, (float) Math.PI * 201, 800);
        }

        public static void DrawAim(PaintEventArgs args)
        {
            const float centreX = 950 / 2;
            const int centre = 500;

            args.Graphics.FillRectangle(Brushes.GreenYellow, centreX - 1, centre - 1, 2, 2);
        }

        public static void DrawRayScreenWall(Map map, PaintEventArgs args)
        {
            foreach (var wall in map.Walls.Concat(map.Doors.Select(x=>x.DorWall)))
            {
                args.Graphics.DrawLine(Pens.Blue, (float) wall.line.A.X / mast, (float) wall.line.A.Y / mast,
                                       (float) wall.line.B.X / mast, (float) wall.line.B.Y / mast);
            }
        }

        public static void DrawPlayer(Map map, PaintEventArgs args, Vector PlayerPos)
        {
            args.Graphics.FillEllipse(Brushes.Aqua,
                                      new Rectangle((int) (PlayerPos.X / mast) - 2, (int) (PlayerPos.Y / mast) - 2, 4, 4));
        }

        public static void DrawRays(Game game, PaintEventArgs args)
        {
            foreach (var tuple in game.CastedRays)
            {
                if (tuple.Count == 0)
                    continue;
                var ray = tuple.Last().Item1;
                if (ray.B.X > 0 && ray.B.Y > 0)
                    args.Graphics.DrawLine(Pens.Blue, (float) ray.A.X / mast, (float) ray.A.Y / mast, (float) ray.B.X / mast,
                                           (float) ray.B.Y / mast);
            }
        }
    }
}