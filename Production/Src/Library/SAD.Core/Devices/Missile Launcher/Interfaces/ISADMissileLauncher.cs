using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Devices
{
    public interface ISADMissileLauncher
    {
        void Kill(double phi, double theta);

        void Fire();

        void Move(double phi, double theta);

        void MoveBy(double phi, double theta);

        void Reload();
    }
}
