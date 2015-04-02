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
    class TargetViewModel : ViewModelBase
    {
        // Fields
        private SADMissileLauncher missileLauncher;
        private Target target;
        private TargetManager targetManager;

        // Constructor
        public TargetViewModel()
        {
            TargetManager = TargetManager.GetInstance();

            // Something should go in here?!
            KillCommand = new TargetViewModelCommand(Kill);
        }

        public TargetViewModel(SADMissileLauncher missileLauncher, Target target)
        {
            TargetManager = TargetManager.GetInstance();
            MissileLauncher = missileLauncher;
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

        public TargetManager TargetManager
        {
            get { return targetManager; }
            set { targetManager = value; }
        }

        // Methods
        public void Kill()
        {
            missileLauncher.Kill(target.Phi, target.Theta);
        }

        // Commands
        public ICommand KillCommand { get; set; }
    }
}
