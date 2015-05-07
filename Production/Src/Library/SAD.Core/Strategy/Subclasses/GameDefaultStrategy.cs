using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Subclasses
{
    class GameDefaultStrategy : GameStrategy
    {
        public override Target PrioritizeTargets()
        {
            Target priorityTarget = null;

            List<Target> sortedList = SortTargetList();

            // Find the highest priority in the list, starting at the highest point value
            for (int count = 0; count < sortedList.Count; count++)
            {
                if (sortedList[count].Status == true)
                {
                    if (sortedList[count].Friend == false)
                    {
                        priorityTarget = sortedList[count];
                        break;
                    }
                }
            }

            return priorityTarget;
        }
    }
}
