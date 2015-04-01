using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SAD.Core;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using SAD.Core.Devices;
using SAD.Core.Devices.Missile_Launcher;
using SAD.Core.Devices.Missile_Launcher.Subclasses;
using SAD.Core.Devices.Missile_Launcher;


namespace GUI.ViewModel
{
    public class MissileLauncherViewModel : ViewModelBase
    {
        //Create an instance within 
        public MissileLauncherViewModel()
        {
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher);

            
            FireCommand = new MyCommand()
        }

        public ICommand FireCommand { get; set; }
    }

    public class MyCommand : ICommand
    {
        private Action m_action;

        public MyCommand(Action someAction)
        {
            m_action = someAction;
        }

        #region Implementation of ICommand
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            m_action();
        }
        public event EventHandler CanExecuteChanged;
        #endregion
    }
}




