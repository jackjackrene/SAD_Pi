using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    sealed class GameThreeStrategy : GameStrategy
    {
        public GameThreeStrategy()
        {
            FoesOnly = false; // watch out for friends!
            IsBlinking = true;
            CanChangeSides = false;
            RequiresVision = false;
        }
    }
}
