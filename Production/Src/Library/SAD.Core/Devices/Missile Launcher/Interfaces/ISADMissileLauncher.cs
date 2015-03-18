using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Devices
{
    public interface ISADMissileLauncher
    {
        void Reload();
        void Fire();
    }
}
