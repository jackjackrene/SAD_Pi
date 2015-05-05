using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy.Data
{
   public enum GameStrategyType
   {
       PrioritizeFoes,
       PrioritizeFriends,
       PrioritizeClosestTargets,
       PrioritizeHighestPoints
   }
}
