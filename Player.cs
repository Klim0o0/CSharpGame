using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game
{
    public class Player
    {
        public float X { get; private set; }
        public float Y{get; private set;}
        public readonly double rayStep = 0.005;
        private float spead = 3;
        public readonly double vsize =Math.PI / 4;
        public List<Ray> Rays { get; private set; }
        
        public double Direction{get; private set;}
        public  Player(float x, float y, double direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            Rays= new List<Ray>();
            for (var i = Direction-vsize; i <= Direction+vsize; i += rayStep)
            {
                Rays.Add(new Ray(new Vector(x, y ), i));
            }
        }

        public void Move()
        {
            for (var i = 0; i <Rays.Count; i++)
            {
                Rays[i].Pos = new Vector(X, Y);
            }
        }
        public void TurnLeft()
        {
            Direction = (Direction - 0.05);
            ChengeRays();
        }
        public void TurnRigth()
        {
            Direction = (Direction + 0.05);
            ChengeRays();
        }

        private void ChengeRays()
        {
            for (double i = 0, j = Direction-Math.PI/4; i <Rays.Count; i++,j+=rayStep)
            {
                Rays[(int) i].Direction =new Vector(Math.Cos(j),-Math.Sin(j));
            }
        }
        
        public void MoveForvard()
        {
            var direction = new Vector(Math.Cos(Direction),-Math.Sin(Direction));
            X += (float)direction.X * spead;
            Y += (float)direction.Y * spead;
            Move();
        }
        public void MoveBeak()
        {
            var direction = new Vector(Math.Cos(Direction),-Math.Sin(Direction));
            direction.Negate();
            X += (float)direction.X * spead;
            Y += (float)direction.Y * spead;
            Move();
        }
        public void MoveLeft()
        {
            var direction = new Vector(Math.Cos(Direction+Math.PI/2),-Math.Sin(Direction+Math.PI/2));
            direction.Negate();
            X += (float) direction.X * spead;
            Y += (float)direction.Y * spead;
            Move();
        }
        public void MoveRigth()
        {
            var direction = new Vector(Math.Cos(Direction+Math.PI/2),-Math.Sin(Direction+Math.PI/2));
            
            X += (float)direction.X * spead;
            Y += (float) direction.Y * spead;
            Move();
        }
    }
}
