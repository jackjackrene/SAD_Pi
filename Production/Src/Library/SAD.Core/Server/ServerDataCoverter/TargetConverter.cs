using SAD.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetServerCommunicator;
using TargetServerCommunicator.Data;
using TargetServerCommunicator.Servers;


namespace SAD.Core.Server.ServerDataCoverter
{
    public class TargetConverter
    {
        private TargetManager targetManager;
        private GameServer gameServer;
        private GameWatch gameWatch;

        // Time the target was hit, used to determine if its alive or not
        private double timeOfLastHit;

        public TargetConverter()
        {
            targetManager = TargetManager.GetInstance();
            gameServer = GameServer.GetInstance();
            gameWatch = GameWatch.GetInstance();
        }

        public void UpdateTargetList()
        {
            // Toast the target list
            targetManager.TargetList = null;

            // Get the updated list
            // gameServer.RetrieveServerTargetList();

            // Convert each ServerTarget to Target and pass to targetManager
        }

        public bool DetermineIfHit()
        {
            return true;
        }

        public Target convertServerTarget(TargetServerCommunicator.Data.Target serverTarget)
        {
            Target target = new Target();

            target.Name = serverTarget.name;
            target.X = serverTarget.x;
            target.Y = serverTarget.y;
            target.Z = serverTarget.z;

            if (serverTarget.status == 0)
                target.Friend = false;
            else if (serverTarget.status == 1)
                target.Friend = true;

            if (serverTarget.canChangeSides == true)
                target.CanSwapSidesWhenHit = true;
            else
                target.CanSwapSidesWhenHit = false;

            // May want to round?
            target.SpawnRate = (int)serverTarget.spawnRate;
            target.Points = (int)serverTarget.points;
            target.FlashRate = (int)serverTarget.dutyCycle;

            /*
            // To determine if the target is alive or not
            if ((TimeOfLastHit - serverTarget.spawnRate) < 0)
                target.Status = false; // still dead
            else
                target.Status = true;  // its alive!    

             */

            return target;
        }         
    }
}
