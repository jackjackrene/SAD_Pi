using SAD.Core.Server.ServerDataCoverter;
using SAD.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    sealed class GameTwoStrategy : GameStrategy
    {
        public GameTwoStrategy() : base()
        {
            FoesOnly = true;
            IsBlinking = true;
            CanChangeSides = false;
            RequiresVision = false;
        }
    }
}
