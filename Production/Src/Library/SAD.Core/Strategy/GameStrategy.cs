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

namespace SAD.Core.Strategy
{
    public abstract class GameStrategy : IPriorityStrategy, IGame
    {
        // Fields that control what is prioritized
        private bool prioritizeEnemies;
        private bool prioritizeFriends;
        private bool prioritizeDistance;
        private bool prioritizePoints;

        protected TargetConverter targetConverter;
        protected GameWatch gameWatch;
        private SADMissileLauncher missileLauncher;

        public GameStrategy()
        {
            targetConverter = new TargetConverter();
            gameWatch = GameWatch.GetInstance();
            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            missileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);
        }

        protected bool PrioritizeEnemies
        {
            get { return prioritizeEnemies; }
            set { prioritizeEnemies = value; }
        }

        protected bool PrioritizeFriends
        {
            get { return prioritizeFriends; }
            set { prioritizeFriends = value; }
        }

        protected bool PrioritizeDistance
        {
            get { return prioritizeDistance; }
            set { prioritizeDistance = value; }
        }

        protected bool PrioritizePoints
        {
            get { return prioritizePoints; }
            set { prioritizePoints = value; }
        }

        // Interface Methods
        public abstract void PrioritizeTargets();

        public abstract void StartGame();


    }
}
