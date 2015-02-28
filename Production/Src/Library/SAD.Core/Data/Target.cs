﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core
{
    public class Target
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public bool Friend { get; set; }
        public int Points { get; set; }
        public int FlashRate { get; set; }
        public int SpawnRate { get; set; }
        public bool CanSwapSidesWhenHit { get; set; }
        public double Phi { get; set; }
        public double Theta { get; set; }

        public Target()
        {
            Name = "NOT VALID";
        }
        
        public Target(string Nm, double x, double y, double z, bool Frd, int Pts, int FR, int SR, bool CSSWH)
        {
            Name = Nm;
            X = x;
            Y = y;
            Z = z;
            Friend = Frd;
            Points = Pts;
            FlashRate = FR;
            SpawnRate = SR;
            CanSwapSidesWhenHit = CSSWH;
        }
    }
}