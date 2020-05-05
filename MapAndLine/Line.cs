using System.Windows;

namespace Game.MapAndLine
{
    public class Line
    {
        public Vector A { get; set; }
        public Vector B { get; set; }
        public double Length { get; }

        public Line(Vector a, Vector b)
        {
            A = a;
            B = b;
            Length = Utils.GetDist(A, B);
        }
    }
}