using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    sealed class GameOneStrategy : GameStrategy
    {
        public GameOneStrategy()
        {
            FoesOnly = true;
            IsBlinking = false;
            CanChangeSides = false;
            RequiresVision = false;
        }
    }
}
