using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Devices
{
    public abstract class SADMissileLauncher : ISADMissileLauncher
    {
        protected int maxMissileCount;
        protected int currentMissleCount;

        protected SADMissileLauncher()
        {
        }
        
        public abstract void Kill(double phi, double theta);

        public abstract void Fire();

        public abstract void Move(double phi, double theta);

        public abstract void MoveBy(double phi, double theta);

        public void Reload()
        {
            System.Console.WriteLine("Reload method of SADMissileLauncher invoked!");

            if (CurrentMissileCount > 0)
                System.Console.WriteLine("No Reload is necessary");
            else
            {
                CurrentMissileCount = MaxMissileCount;
                System.Console.WriteLine("Reload succesful");
            }
        }
        
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
