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
    class RapidFireStrategy : IStrategy
    {
        private TargetManager targetManager;
        private GameWatch gameWatch;
        public void RapidFire()
        {
            SAD.Core.TargetManager targetManager = SAD.Core.TargetManager.GetInstance();
            List<SAD.Core.Target> TargetList = new List<SAD.Core.Target>();
            SAD.Core.Server.ServerDataCoverter.TargetConverter targetConverter = new SAD.Core.Server.ServerDataCoverter.TargetConverter();
            gameWatch = GameWatch.GetInstance();
            SAD.Core.Devices.SADMissileLauncherFactory missileLauncherFactory = SAD.Core.Devices.SADMissileLauncherFactory.GetInstance();
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher);


            // TargetList should contain a list of targets.  
            targetConverter.UpdateTargetList();
            TargetList = targetManager.GetAllTargets.ToList();

            // target manager.update list updates the list and sends it to target manager. 
            gameWatch.StartGameWatch();

            // We do not know if the target is dead or alive. 
            // Must use the timer to figure this out. 

            TimeSpan currentTime = gameWatch.GetCurrentTime();
            int minutesInGame = currentTime.Minutes;
            bool moveRight = true;
            // While we are at less than 60 seconds... the game is running
            while (minutesInGame < 1)
            {
                if (missileLauncher.CurrentMissileCount == 0)
                {
                    MessageBox.Show("Reload", "Reload", MessageBoxButtons.OK);
                    missileLauncher.Reload();
                }

                if (moveRight == true)
                {
                    missileLauncher.MoveBy(2, 0);
                    missileLauncher.Fire();
                }

                if (moveRight == false)
                {
                    missileLauncher.MoveBy(-2, 0);
                    missileLauncher.Fire();
                }


                if (missileLauncher.CurrentPhi < -18)
                {
                    moveRight = true;
                }
                if (missileLauncher.CurrentPhi > 18)
                {
                    moveRight = false;
                }


                currentTime = gameWatch.GetCurrentTime();
                minutesInGame = currentTime.Minutes;
            }
            MessageBox.Show("GAME OVER", "GAME OVER", MessageBoxButtons.OK);
            gameWatch.StopGameWatch();
            gameWatch.ResetGameWatch();

        }
    }
}


