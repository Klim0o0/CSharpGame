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
        
        public static void DrawGame(PaintEventArgs args, Game game, Dictionary<string, Bitmap> textures, int distance, Map map)
        {
            args.Graphics.FillRectangle(Brushes.White, 0, 0, args.Graphics.DpiX, args.Graphics.DpiY); 
            //args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            game.CastRays();
            DrawPlayerView(args, game, textures, distance);
            DrawRays(game, args);
            DrawRayScreenWall(map, args);
            DrawPlayer(map, args, game.PlayerPos);
            DrawAim(args);
        }

        public static void DrawPlayerView(PaintEventArgs args, Game game, Dictionary<string, Bitmap> textures,
                                          int distance)
        {
            var time1 = DateTime.Now;
            args.Graphics.FillRectangle(Brushes.CornflowerBlue, 0, 0, 1000 / game.CastedRays.Length * game.CastedRays.Length + 10, 500);
            args.Graphics.FillRectangle(Brushes.Gray, 0, 500, 1000 / game.CastedRays.Length * game.CastedRays.Length + 10, 500);

            var offset = 0;
            foreach (var tuples in game.CastedRays)
            {
                foreach (var (ray, wall) in tuples)
                {
                    if (!(ray.B.X > 0) || !(ray.B.Y > 0))
                        continue;

                    var bmp = textures[wall.Name];
                    var h = (int) (distance / ray.Length);
                    args.Graphics.DrawImage(
                             bmp, new RectangleF(0 + offset, 500 -  h, 6,  h*2),
                             new RectangleF((int) Utils.GetDist(wall.line.A, ray.B) % (bmp.Width - 1), 0, 1, bmp.Height),
                             GraphicsUnit.Pixel);
                }
                offset += 1000 / game.CastedRays.Length;
            }
            var time2 = DateTime.Now;
            args.Graphics.DrawString("ticks on frame:"+time2.Subtract(time1).Ticks.ToString(), SystemFonts.MenuFont, Brushes.Black, 1200, 0);
            //args.Graphics.FillRectangle(Brushes.White, 0, 600, (float) Math.PI * 201, 800);
        }

        private static void DrawAim(PaintEventArgs args)
        {
            const float centreX = 475;
            const int centre = 500;

            args.Graphics.FillRectangle(Brushes.GreenYellow, centreX - 1, centre - 1, 2, 2);
        }

        private static void DrawRayScreenWall(Map map, PaintEventArgs args)
        {
            foreach (var wall in map.Walls.Concat(map.Doors.Select(x=>x.DorWall)))
            {
                args.Graphics.DrawLine(Pens.Blue, (float) wall.line.A.X / mast, (float) wall.line.A.Y / mast,
                                       (float) wall.line.B.X / mast, (float) wall.line.B.Y / mast);
            }
        }

        private static void DrawPlayer(Map map, PaintEventArgs args, Vector playerPos)
        {
            args.Graphics.FillEllipse(Brushes.Aqua,
                                      new Rectangle((int) (playerPos.X / mast) - 2, (int) (playerPos.Y / mast) - 2, 4, 4));
        }

        private static void DrawRays(Game game, PaintEventArgs args)
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