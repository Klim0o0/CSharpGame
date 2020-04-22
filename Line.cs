using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game
{
    public class Line
    {
        public Vector A { get; private set; }
        public Vector B { get; private set; }
        
        public double Length { get; }
        public Line(Vector a, Vector b)
        {
            A = a;
            B = b;
            Length = Utils.GetDist(A, B);
        }
        
    }
}
