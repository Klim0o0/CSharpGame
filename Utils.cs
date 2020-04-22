using System;
using System.Windows;

namespace Game
{
    public static class Utils
    {
        public static double GetDist(Vector a, Vector b) =>
            Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
    }
}