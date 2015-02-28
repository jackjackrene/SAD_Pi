using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core
{
    public class TargetManager
    {


        // Singletone TargetManager Instance
        // If there is no TargetManager, create one. 
        private static TargetManager instance;
        private IEnumerable<Target> targetList;
        public static TargetManager GetInstance()
        {           
                if (instance == null)
                {
                    instance = new TargetManager();
                }

                return instance;
            
         
        }
        /// <summary>
        /// Target Manager constructor which creates a targetInstance... A list of Targets
        /// </summary>
      private TargetManager()
        {

        }

        public IEnumerable<Target> TargetList
      {
          set { targetList = value; }
          get { return targetList; }
      }
        public void SetTargets()
      {
      }

        void Prioritize()
        {


        }

        private bool IsFriend(Target target)
        {
            if (target.Friend == true)
            {
                return true;
            }
            else return false;

        }
        public IEnumerable<Target> GetEnemies
        {
            get
            {
                return (targetList.Where(c => c.Friend != true));
            }
        }

        public IEnumerable<Target> GetFriends
        {
            get
            {
                return (targetList.Where(c => c.Friend == true));
            }
        }

        /// <summary>
        /// Function to prioritize Targets within game that will be
        /// implemented later. 
        /// </summary>
        void Prioritize
        {
            

        }

        
    }
}
