using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAD.Core.Devices;
using SAD.Core.Devices.Missile_Launcher.Subclasses;

namespace SAD.Core.Devices
{
    public class SADMissileLauncherFactory
    {
        private static SADMissileLauncherFactory sadMissileLauncherFactoryInstance;

        private SADMissileLauncherFactory()
        {
        }

        public static SADMissileLauncherFactory GetInstance()
        {
            if (sadMissileLauncherFactoryInstance == null)
                return new SADMissileLauncherFactory();

            return sadMissileLauncherFactoryInstance;
        }

        public SADMissileLauncher CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType missileType)
        {
            SADMissileLauncher missileLauncher = null;

            switch (missileType)
            {
                case SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher:
                    missileLauncher = DreamCheekyMissileLauncher.GetInstance();
                    break;
                case SAD.Core.Devices.SADMissileLauncher.SADMissileType.MockMissileLauncher:
                    missileLauncher = MockMissileLauncher.GetInstance();
                    break;
                default:
                    missileLauncher = MockMissileLauncher.GetInstance();
                    break;
            }

            return missileLauncher;
        }
    }
}
