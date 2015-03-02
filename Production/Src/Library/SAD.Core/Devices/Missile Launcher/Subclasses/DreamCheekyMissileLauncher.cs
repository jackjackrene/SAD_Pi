using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildDefender;

namespace SAD.Core.Devices.Missile_Launcher.Subclasses
{
    sealed class DreamCheekyMissileLauncher : SADMissileLauncher
    {
        private static DreamCheekyMissileLauncher dreamCheekyMissileLauncherInstance;
        private MissileLauncher internalMissileLauncher;    // should this be a "singleton?"

        private DreamCheekyMissileLauncher() : base()
        {
            MaxMissileCount = 4;
            CurrentMissileCount = MaxMissileCount;

            internalMissileLauncher = new MissileLauncher();
        }

        public static DreamCheekyMissileLauncher GetInstance()
        {
            if (dreamCheekyMissileLauncherInstance == null)
                return new DreamCheekyMissileLauncher();
            
            return dreamCheekyMissileLauncherInstance;
        }

        public override void Kill(double phi, double theta)
        {
            System.Console.WriteLine("Kill method of SADMissileLauncher invoked!");

            if (CurrentMissileCount < 1)
                System.Console.WriteLine("No Missiles left! MUST RELOAD!!");
            else
            {
                Move(phi, theta);
                Fire();
            }
        }

        public override void Fire()
        {
            if (CurrentMissileCount < 1)
                System.Console.WriteLine("Reload! No missiles left!");
            else
            {
                System.Console.WriteLine("Fire method of SADMissileLauncher invoked!");
                internalMissileLauncher.command_Fire();

                // Update Missile count (could be its own method)
                currentMissleCount--;
            }
        }

        public override void Move(double phi, double theta)
        {
            System.Console.WriteLine("Move method of SADMissileLauncher invoked!");

            // Reset the Launcher
            internalMissileLauncher.command_reset();

            // Move by theta
            if (theta < 0)
                internalMissileLauncher.command_Left((int)Math.Round(theta));
            else
                internalMissileLauncher.command_Right((int)Math.Round(theta));

            // Move by phi
            if (phi < 0)
                internalMissileLauncher.command_Down((int)Math.Round(phi));
            else
                internalMissileLauncher.command_Up((int)Math.Round(phi));
        }

        public override void MoveBy(double phi, double theta)
        {
            System.Console.WriteLine("MoveBy method of SADMissileLauncher invoked!");

            // Move by theta
            if (theta < 0)
                internalMissileLauncher.command_Left((int)Math.Round(theta));
            else
                internalMissileLauncher.command_Right((int)Math.Round(theta));

            // Move by phi
            if (phi < 0)
                internalMissileLauncher.command_Down((int)Math.Round(phi));
            else
                internalMissileLauncher.command_Up((int)Math.Round(phi));
        }
    }
}
