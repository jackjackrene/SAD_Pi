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
using System.Windows;
using System.Windows.Forms;


namespace SAD.Core.EricStrategy
{
    public class KillAllStrategy : IStrategy
    {
        private TargetManager targetManager;
        private GameWatch gameWatch;
        public void GetTargetAndKillIt()
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
            targetConverter.UpdateTargetList();
            // target manager.update list updates the list and sends it to target manager. 
            gameWatch.StartGameWatch();
            
            // We do not know if the target is dead or alive. 
            // Must use the timer to figure this out. 





            foreach (Target target in TargetList)
            {
                if (missileLauncher.CurrentMissileCount == 0)
                {
                    MessageBox.Show("Reload", "Reload", MessageBoxButtons.OK);
                    missileLauncher.Reload();
                    // add logic stating that we must reload and wait until the reload is complete. 
                }
                missileLauncher.Kill(target.Phi, target.Theta);
                gameWatch.GetCurrentTime();
            }
        }
           
    }
}
