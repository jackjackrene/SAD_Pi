using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAD.Core.Server.ServerDataCoverter;
using TargetServerCommunicator;
using TargetServerCommunicator.Data;
using TargetServerCommunicator.Servers;
using SAD.Core.Game;
using SAD.Core.Time;
using SAD.Core.Devices;

namespace SAD.Core.EricStrategy
{
    class KillAllStrategy : IStrategy
    {
        private TargetManager targetManager;
        private GameWatch gameWatch;
        public void GetMostValuable()
        {
            SAD.Core.TargetManager targetManager = SAD.Core.TargetManager.GetInstance();
            List<SAD.Core.Target> TargetList = new List<SAD.Core.Target>();
            SAD.Core.Server.ServerDataCoverter.TargetConverter targetConverter = new SAD.Core.Server.ServerDataCoverter.TargetConverter();
            targetManager = TargetManager.GetInstance();
            gameWatch = GameWatch.GetInstance();
            SAD.Core.Devices.SADMissileLauncherFactory missileLauncherFactory = SAD.Core.Devices.SADMissileLauncherFactory.GetInstance();
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher);


            // TargetList should contain a list of targets. 
            TargetList = targetManager.GetAllTargets.ToList();

            foreach (Target target in TargetList)
            {
                missileLauncher.Kill(target.Phi, target.Theta);

            }
        }
           
    }
}
