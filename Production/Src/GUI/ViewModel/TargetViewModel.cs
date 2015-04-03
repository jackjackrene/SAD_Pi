using GUI.Commands.TargetViewModelCommands;
using SAD.Core;
using SAD.Core.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModel
{
    public class TargetViewModel : ViewModelBase
    {
        // Fields
        private SADMissileLauncher missileLauncher;
        private Target target;

        // Constructor
        public TargetViewModel()
        {
            // Get the MissileLauncher instance
            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            MissileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);

            // Something should go in here?!
            KillCommand = new TargetViewModelCommand(Kill);
        }

        public TargetViewModel(Target target)
        {
            // Get the MissileLauncherInstance
            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            MissileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);

            Target = target;

            // Command stuff
            KillCommand = new TargetViewModelCommand(Kill);
        }

        // Properties
        public SADMissileLauncher MissileLauncher
        {
            get { return missileLauncher; }
            set { missileLauncher = value; }
        }

        public Target Target
        {
            get { return target; }
            set { target = value; }
        }



        // Methods
        public void Kill()
        {
            // Not sure if this will work
            if (!target.Friend)
                missileLauncher.Kill(target.Phi, target.Theta);
        }

        // Commands
        public ICommand KillCommand { get; set; }
    }
}
