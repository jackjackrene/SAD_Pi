﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class TargetManager
    {
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



        IEnumerable<Target> TargetList;
        void Prioritize();
        private bool IsFriend(Target target);
        public IEnumerable<Target> GetEnemies
        {




        }
        public IEnumerable<Target> GetFriends
        {



        }


        
    }
}
