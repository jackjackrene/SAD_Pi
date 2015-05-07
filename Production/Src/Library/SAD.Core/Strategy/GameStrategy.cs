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
using System.Windows.Forms;

namespace SAD.Core.Strategy
{
    public abstract class GameStrategy : IPriorityStrategy, IGame
    {
        protected TargetConverter targetConverter;
        protected GameWatch gameWatch;
        private SADMissileLauncher missileLauncher;
        private GameServer gameServer;
        private TargetManager targetManager;

        // Game Fields
        private bool foesOnly;
        private bool isBlinking;
        private bool canChangeSides;
        private bool requiresVision;

        // Strategy Fields
        private double gameStartTime;
        private Target lastTargetShotAt;


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

        protected TimeSpan TimeTillTargetRespawns(TimeSpan timeOfLastHit, TimeSpan spawnRate)
        {
            return (gameWatch.GetCurrentTime() - (timeOfLastHit + spawnRate));
        }

        protected List<Target> SortTargetList()
        {
            List<Target> sortedList;

            // What do we care about?
            if (foesOnly == true || canChangeSides == false)
                sortedList = targetManager.GetEnemies.ToList();
            else
                sortedList = targetManager.GetAllTargets.ToList(); // Traitors!

            // Use selection sort to order from greatest points to least points
            for (int i = 0; i < sortedList.Count; i++)
            {
                int largestPointValue = i;

                for (int j = i; j < sortedList.Count; j++)
                {
                    if (sortedList[largestPointValue].Points < sortedList[j].Points)
                        largestPointValue = j;

                    Target temp = sortedList[i];
                    sortedList[i] = sortedList[largestPointValue];
                    sortedList[largestPointValue] = temp;
                }
            }

            return sortedList;
        }

        protected bool HasSwapedSides(int hitCount)
        {
            // Assume all start off as 'normal'
            // If the hit count is odd, hes flipped
            // If even, hes following his dogma
            if ((hitCount == 0) || (hitCount % 2 == 0))
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

            // For as long as the game is running
            while ((TimeSpan.Compare(gameWatch.GetCurrentTime(), timeLimit) == -1))
            {
                Target victim;

                // Update the target list
                targetConverter.UpdateTargetList();

                // Find the next target
                victim = PrioritizeTargets();

                // Shoot at it
                missileLauncher.Kill(victim.Phi, victim.Theta);

                // Reload (IF)
                if (missileLauncher.CurrentMissileCount == 0)
                {
                    MessageBox.Show("Reload", "Reload", MessageBoxButtons.OK);
                    missileLauncher.Reload();
                }
            }
        }

        public abstract Target PrioritizeTargets();
    }
}
