using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework01
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

        public void PrintTarget()
        {
            Console.Write("Name=");
            Console.WriteLine(this.Name);
            Console.Write("X=");
            Console.WriteLine(this.X);
            Console.Write("Y=");
            Console.WriteLine(this.Y);
            Console.Write("Z=");
            Console.WriteLine(this.Z);
            Console.Write("Friend=");
            Console.WriteLine(this.Friend);
            Console.Write("Points=");
            Console.WriteLine(this.Points);
            Console.Write("FlashRate=");
            Console.WriteLine(this.FlashRate);
            Console.Write("SpawnRate=");
            Console.WriteLine(this.SpawnRate);
            Console.Write("CanSwapSidesWhenHit=");
            Console.WriteLine(this.CanSwapSidesWhenHit);
        }
    }
}
