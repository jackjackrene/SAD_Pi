using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class TargetManager
    {


        // Singletone TargetManager Instance
        // If there is no TargetManager, create one. 
        private static TargetManager instance;
        public static TargetManager Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new TargetManager();
                }

                return instance;
            }

        }


        /// <summary>
        /// IEnumerable Target list. 
        /// </summary>
        IEnumerable<Target> TargetList
        {
            get { return TargetList; }
            set;
        }
        void Prioritize()
        {


        }
        private bool IsFriend(Target target);
        public IEnumerable<Target> GetEnemies
        {
            get
            {
                return (var from TargetList where var.GetFriends == false)
            }




        }
        public IEnumerable<Target> GetFriends
        {



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
