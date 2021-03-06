﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    class GameFiveStrategy : GameStrategy
    {
        public GameFiveStrategy()
        {
            FoesOnly = false; // watch out for friends!
            IsBlinking = false;
            CanChangeSides = false;
            RequiresVision = true;
        }

        public override Target PrioritizeTargets()
        {
            Target priorityTarget = null;

            List<Target> sortedList = SortTargetList();

            /* This may have to be very different, depending on wether or not we trust the server info at all */

            // Find the highest priority in the list, starting at the highest point value
            for (int count = 0; count < sortedList.Count; count++)
            {
                if (sortedList[count].Status == true)
                {
                    priorityTarget = sortedList[count];
                    break;
                }
            }

            // If all targets are dead, attempt to shoot at the next one that will turn on
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
