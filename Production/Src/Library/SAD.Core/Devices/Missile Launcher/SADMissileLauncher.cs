using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Devices
{
    abstract class SADMissileLauncher : ISADMissileLauncher
    {
        protected int maxMissileCount;
        protected int currentMissleCount;

        protected SADMissileLauncher()
        {
        }

        public abstract void ISADMissileLauncher.Kill(double phi, double theta);

        public abstract void ISADMissileLauncher.Fire();

        public abstract void ISADMissileLauncher.Move(double phi, double theta);

        public abstract void ISADMissileLauncher.MoveBy(double phi, double theta);

        public abstract void ISADMissileLauncher.Reload();

        // Properties
        public int MaxMissileCount
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Error: the maximum number of missles cannot be negative", maxMissileCount.ToString());
                }
                else
                    maxMissileCount = value;
            }

            get
            {
                return maxMissileCount;
            }
        }

        public int CurrentMissileCount
        {
            set
            {
                // Do nothing
            }

            get
            {
                return currentMissleCount;
            }
        }

        public enum SADMissileType
        {
            DreamCheekyMissileLauncher,
            MockMissileLauncher
        }
    }
}
