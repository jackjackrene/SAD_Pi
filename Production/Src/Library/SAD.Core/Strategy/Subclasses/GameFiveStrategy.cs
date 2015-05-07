using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    class GameFiveStrategy : GameStrategy
    {
        public GameFiveStrategy()
        {
            FoesOnly = false; // watch out for friends!
            IsBlinking = false;
            CanChangeSides = false;
            RequiresVision = true;
        }
    }
}
