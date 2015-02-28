using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core
{
    public static class Spherical
    {
        public static void ConvertToSphere(Target t)
        {
            double phi = 0;
            double theta = 0;

            phi = Math.Atan2(t.X, t.Y);
            theta = Math.Acos(t.Z / Math.Sqrt(Math.Pow(t.X, 2) + Math.Pow(t.Y, 2) + Math.Pow(t.Z, 2)));

            t.Phi = phi;
            t.Theta = theta;

            return;
        }
    }
}
