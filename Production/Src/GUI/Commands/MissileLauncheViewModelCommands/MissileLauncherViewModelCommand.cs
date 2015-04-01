using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace GUI.Commands.MissileLauncheViewModelCommands
{
    class MissileLauncherViewModelCommand : ICommand
    {
         // Fields
        private Action DreamCheekyViewModelAction;
        public event EventHandler CanExecuteChanged; // I think this is for CanExecute()

        // Constructor
        public MissileLauncherViewModelCommand(Action action)
        {
            DreamCheekyViewModelAction = action;
        }

        public bool CanExecute(object parameter)
        {
            // Some kind of check should go here
            return true;
        }

        public void Execute(object parameter)
        {
            DreamCheekyViewModelAction();
        }
    }
}
