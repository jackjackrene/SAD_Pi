using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Devices.Missile_Launcher.Subclasses
{
    class MockMissileLauncher : SADMissileLauncher
    {
        private static MockMissileLauncher mockMissileLauncherInstance;

        private MockMissileLauncher() : base()
        {
            MaxMissileCount = 1;
            CurrentMissileCount = MaxMissileCount;
        }

        public static MockMissileLauncher GetInstance()
        {
            if (mockMissileLauncherInstance == null)
            {
                MissileLauncherType = SADMissileType.MockMissileLauncher;

                mockMissileLauncherInstance = new MockMissileLauncher();
                return mockMissileLauncherInstance;
            }
            return mockMissileLauncherInstance;
        }

        public override void Kill(double phi, double theta)
        {
            System.Console.WriteLine("Kill method of mockMissileLauncher invoked!");
            Move(phi, theta);
            Fire();
        }

        public override void Fire()
        {
            System.Console.WriteLine("Fire method of mockMissileLauncher invoked!");
        }

        public override void Move(double phi, double theta)
        {
            System.Console.WriteLine("Move method of mockMissileLauncher invoked!");
            CurrentPhi = phi;
            CurrentTheta = theta;
        }

        public override void MoveBy(double phi, double theta)
        {
            System.Console.WriteLine("MoveBy method of mockMissileLauncher invoked!");
            CurrentPhi = CurrentPhi + phi;
            CurrentTheta = CurrentTheta + theta;
        }
    }
}
