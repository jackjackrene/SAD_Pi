using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAD.Core.Strategy.Interfaces;
using SAD.Core.Game;
using System.Diagnostics;
using SAD.Core.Server.ServerDataCoverter;
using SAD.Core.Time;
using BuildDefender;
using SAD.Core.Devices;
using SAD.Core.Server;

namespace SAD.Core.Strategy
{
    public abstract class GameStrategy : IPriorityStrategy, IGame
    {
        // Game Fields
        private bool foesOnly;
        private bool isBlinking;
        private bool canChangeSides;
        private bool requiresVision;

        // Strategy Fields
        private double gameStartTime;

        protected TargetConverter targetConverter;
        protected GameWatch gameWatch;
        private SADMissileLauncher missileLauncher;
        private GameServer gameServer;
        private TargetManager targetManager;

        public GameStrategy()
        {
            targetConverter = new TargetConverter();
            gameWatch = GameWatch.GetInstance();
            gameServer = GameServer.GetInstance();
            targetManager = TargetManager.GetInstance();

            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            missileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);
        }

        protected bool FoesOnly
        {
            get { return foesOnly; }
            set { foesOnly = value; }
        }

        protected bool IsBlinking
        {
            get { return isBlinking; }
            set { isBlinking = value; }
        }

        protected bool CanChangeSides
        {
            get { return canChangeSides; }
            set { canChangeSides = value; }
        }

        protected bool RequiresVision
        {
            get { return requiresVision; }
            set { requiresVision = value; }
        }

        // Strategy Methods
        protected bool IsTargetOn(double flashRate)
        {
            // deals with flash rate
            // get the number of times in a minute the target can swap
            double gameTimeLimit = 60.0;
            int numberOfTimeIntervals = (int) (gameTimeLimit / flashRate);

            double[] timeIntervals = new double [numberOfTimeIntervals + 1];

            // populate the array with time intervals
            timeIntervals[0] = 0.0;
            timeIntervals[numberOfTimeIntervals - 1] = 60.0;

            for (int count = 1; count < (numberOfTimeIntervals - 1); count++)
                timeIntervals[count] = timeIntervals[count - 1] + flashRate;

            // Figure out if the target is on or off
            for (int count = 0; count < numberOfTimeIntervals; count++)
            {
                // assume event indexes are when the target is 'On'
                if ((count == 0) || (count % 2 == 0))
                {
                    // Avoid something nasty
                    if (count != (numberOfTimeIntervals - 1))
                    {
                        TimeSpan lowerInterval = TimeSpan.FromSeconds(timeIntervals[count]);
                        TimeSpan upperInterval = TimeSpan.FromSeconds(timeIntervals[count + 1]);

                        // If within the interval it, is on
                        if ((TimeSpan.Compare(gameWatch.GetCurrentTime(), lowerInterval) == 1) &&
                            (TimeSpan.Compare(gameWatch.GetCurrentTime(), upperInterval) == -1))
                            return true;
                    }
                }
            }

            return false;
        }

        protected bool HasSwapedSides(bool wasHitLastTime)
        {
            // If it was hit last time, yes
            if (wasHitLastTime == true)
                return true;
            else
                return false;
        }

        // Interface Methods


        public void StartGame()
        {
            /* Connect to server in the ViewModel */
            
            // Start the game watch
            gameWatch.StartGameWatch();
            TimeSpan timeLimit = TimeSpan.FromMinutes(1.00);

            while ((TimeSpan.Compare(gameWatch.GetCurrentTime(), timeLimit) == -1))
            {
                Target victim;

                // Update the target list
                targetConverter.UpdateTargetList();

                // Find the next target
                victim = PrioritizeTargets();

                // Shoot at it
                missileLauncher.Kill(victim.Phi, victim.Theta);
            }
        }

        public Target PrioritizeTargets()
        {
            Target priorityTarget = new Target();
            List<Target> targetList;
            List<Target> sortedList;

            // What do we care about?
            if (foesOnly == true)
                targetList = targetManager.GetEnemies.ToList();
            else
                targetList = targetManager.GetAllTargets.ToList();

            // Use selection sort to order from greatest points to least points
            for (int i = 0; i < targetList.Count; i++)
            {
                int largestPointValue = i;

                for (int j = i; j < targetList.Count; j++)
                {
                    if (targetList[largestPointValue].Points < targetList[j].Points)
                        largestPointValue = j;

                    Target temp = targetList[i];
                    targetList[i] = targetList[largestPointValue];
                    targetList[largestPointValue] = temp;
                }
            }




                return priorityTarget;
        }
    }
}
