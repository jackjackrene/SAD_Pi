using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    sealed class GameFourStrategy : GameStrategy
    {
        public GameFourStrategy()
        {
            FoesOnly = false; // watch out for friends!
            IsBlinking = true;
            CanChangeSides = true; // traitors!
            RequiresVision = false;
        }

        public override Target PrioritizeTargets()
        {
            Target priorityTarget = null;

            List<Target> sortedList = SortTargetList();

            // Find the highest priority in the list, starting at the highest point value
            for (int count = 0; count < sortedList.Count; count++)
            {
                if (sortedList[count].Status == true)
                {
                    if (IsTargetOn(sortedList[count].FlashRate) &&
                        !HasSwapedSides(sortedList[count].HitCount) &&
                        sortedList[count].Status == false)
                    {
                        priorityTarget = sortedList[count];
                        break;
                    }
                }
            }

            // If all targets are dead, or off, attempt to shoot at the next one that will turn on
            if (priorityTarget == null)
            {
                priorityTarget = sortedList[0];

                for (int count = 0; count < sortedList.Count; count++)
                {
                    TimeSpan previousValue = TimeTillTargetRespawns(sortedList[count].TimeOfLastHit, TimeSpan.FromSeconds(sortedList[count].SpawnRate));
                    TimeSpan currentValue = TimeTillTargetRespawns(priorityTarget.TimeOfLastHit, TimeSpan.FromSeconds(priorityTarget.SpawnRate));

                    if (TimeSpan.Compare(previousValue, currentValue) == -1)
                    {
                        priorityTarget = sortedList[count];
                    }
                }
            }

            return priorityTarget;
        }
    }
}
