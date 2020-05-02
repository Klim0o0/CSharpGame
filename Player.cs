﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game
{
    public class Player
    {
        public float X { get; set; }
        public float Y { get; set; }
        public const double RayStep = 0.005;
        public readonly int Speed = 3;
        private readonly double vsize = Math.PI / 4;
        public List<Ray> Rays { get; private set; }
        public double Direction { get; private set; }

        public Player(float x, float y, double direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            Rays = new List<Ray>();
            for (var i = Direction + vsize; i >= Direction - vsize; i -= RayStep)
            {
                Rays.Add(new Ray(new Vector(x, y), i));
            }
        }

        public void Move(Vector m)
        {
            X += (float) m.X;
            Y += (float) m.Y;
            foreach (var ray in Rays)
                ray.Pos = new Vector(X, Y);
        }

        public void TurnAt(double delta)
        {
            Direction = (Direction + delta);
            ChangeRays();
        }

        private void ChangeRays()
        {
            for (double i = 0, j = Direction + Math.PI / 4; i < Rays.Count; i++, j -= RayStep)
            {
                Rays[(int) i].Direction = new Vector(Math.Cos(j), -Math.Sin(j));
            }
        }
    }
}