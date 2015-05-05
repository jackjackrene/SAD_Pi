using SAD.Core.Server.ServerDataCoverter;
using SAD.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    sealed class PrioritizePointsStrategy : GameStrategy
    {
        public PrioritizePointsStrategy() : base()
        {
            PrioritizePoints = true;
            PrioritizeFriends = false;
            PrioritizeEnemies = true;
            PrioritizeDistance = false;
        }

        public override void PrioritizeTargets()
        {
            throw new NotImplementedException();
        }

        public override void StartGame()
        {
            // Start the clock
            gameWatch.StartGameWatch();

            // Kill logic goes here
            PrioritizeTargets();

        }
    }
}
