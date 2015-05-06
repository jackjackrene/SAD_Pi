using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SAD.Core
{
    public class Target
    {
        public string Name
        { get; set; }
        public double X
        { get; set; }
        public double Y
        { get; set; }
        public double Z
        { get; set; }
        public bool Friend
        { get; set; }
        public bool Status
        { get; set; }
        public double Points
        { get; set; }
        public double FlashRate
        { get; set; }
        public int HitCount
        { get; set; }
        public TimeSpan TimeOfLastHit
        { get; set; }
        public bool WasHtLastTime
        { get; set; }

        public double SpawnRate
        { get; set; }
        public bool CanSwapSidesWhenHit
        { get; set; }
        public double Phi
        { get; set; }
        public double Theta
        { get; set; }
        public string RectangularCoordinates
        {
            get
            {
                return "(" + X + ", " + Y + ", " + Z + ")";
            }
        }
        public string SphericalCoordinates
        {
            get
            {
                return "(" + Phi + ", " + Theta + ")";
            }
        }
        public Target()
        {
            Name = "NOT VALID";
        }
        
        public Target(string Nm, double x, double y, double z, bool Frd, int Pts, int FR, int SR, bool CSSWH, int hitCount, TimeSpan timeOfLastHit)
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
            TimeOfLastHit = timeOfLastHit;
        }
    }
}
