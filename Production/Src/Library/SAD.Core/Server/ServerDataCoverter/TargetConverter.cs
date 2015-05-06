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

        private List<TargetServerCommunicator.Data.Target> serverTargetList;
        private List<Target> oldSadTargetList;
        private List<Target> newSadTargetList;

        // Camera detection strategy
        private ITargetPositionStrategy targetPositionStrategy;
        CalculatePositions calculatePositions;

        // Time the target was hit, used to determine if its alive or not
        private double timeOfLastHit;
        private bool useVisualDetection;

        public TargetConverter()
        {
            targetManager = TargetManager.GetInstance();
            gameServer = GameServer.GetInstance();
            gameWatch = GameWatch.GetInstance();
        }

        public void UpdateTargetList()
        {
            // Save the old state of targets
            oldSadTargetList = targetManager.TargetList.ToList();

            // Toast the target list
            targetManager.TargetList = null;

            // Get the updated list
            serverTargetList = gameServer.RetrieveServerTargetList().ToList(); 

            // Convert each ServerTarget to Target and pass to targetManager
            for (int count = 0; count < serverTargetList.Count; count++)
                newSadTargetList.Add(convertServerTarget(serverTargetList[count]));
            
            // Determine if we need the camera
            if (DetermineIfCameraIsInUse(serverTargetList.ElementAt(0)) == true)
            {
                targetPositionStrategy = new DetectPosition();
            }
            else
                targetPositionStrategy = new UseCurrentXYZ();

            // Calculate Phi and Theta
            calculatePositions = new CalculatePositions(targetPositionStrategy);

            // Update the list
            targetManager.TargetList = newSadTargetList;
        }

        public bool DetermineIfCameraIsInUse(TargetServerCommunicator.Data.Target serverTarget)
        {
            if (serverTarget.y < 0)
                return true;
            else
                return false;
        }

        public bool wasHit(int oldHitCount, int newHitCount)
        {
            if (oldHitCount != newHitCount)
                return true;
            else
                return false;

            return true;
        }

        public Target FindOldTarget(string name)
        {
            for (int count = 0; count < oldSadTargetList.Count; count++)
            {
                if (String.Equals(oldSadTargetList[count].Name, name))
                    return oldSadTargetList[count];
            }

            return null;
        }

        public Target convertServerTarget(TargetServerCommunicator.Data.Target serverTarget)
        {
            Target target = new Target();

            target.Name = serverTarget.name;
            target.X = serverTarget.x;
            target.Y = serverTarget.y;
            target.Z = serverTarget.z;

            target.HitCount = serverTarget.hit;

            if (serverTarget.status == 0)
                target.Friend = false;
            else if (serverTarget.status == 1)
                target.Friend = true;

            if (serverTarget.canChangeSides == true)
                target.CanSwapSidesWhenHit = true;
            else
                target.CanSwapSidesWhenHit = false;

            target.SpawnRate = serverTarget.spawnRate;
            target.Points = serverTarget.points;
            target.FlashRate = serverTarget.dutyCycle;

            // Is it dead?
            Target lastTargetShotAt = FindOldTarget(serverTarget.name);

            if (wasHit(lastTargetShotAt.HitCount, serverTarget.hit))
            {
                target.Status = false; // its dead
                target.TimeOfLastHit = gameWatch.GetCurrentTime();
            }
            else
                target.Status = true;  // its alive

            return target;
        }         
    }
}
