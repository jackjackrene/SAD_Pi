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
                mockMissileLauncherInstance = new MockMissileLauncher();
                return mockMissileLauncherInstance;
            }
            return mockMissileLauncherInstance;
        }

        public override void Kill(double phi, double theta)
        {
            System.Console.WriteLine("Kill method of mockMissileLauncher invoked!");
        }

        public override void Fire()
        {
            System.Console.WriteLine("Fire method of mockMissileLauncher invoked!");
        }

        public override void Move(double phi, double theta)
        {
            System.Console.WriteLine("Move method of mockMissileLauncher invoked!");
        }

        public override void MoveBy(double phi, double theta)
        {
            System.Console.WriteLine("MoveBy method of mockMissileLauncher invoked!");
        }

        //public override void MoveUp()
        //{
        //    System.Console.WriteLine("MoveUp method of mockMissileLauncher invoked!");

        //}

        //public override void MoveDown()
        //{
        //    System.Console.WriteLine("MoveDown method of mockMissileLauncher invoked!");

        //}

        //public override void MoveLeft()
        //{
        //    System.Console.WriteLine("MoveLeft method of mockMissileLauncher invoked!");

        //}

        //public override void MoveRight()
        //{
        //    System.Console.WriteLine("MoveUp method of mockMissileLauncher invoked!");

        //}

    }
}
