using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.Commands.TargetViewModelCommands
{
    class TargetViewModelCommand : ICommand
    {
        // Fields
        private Action TargetViewModelAction;
        public event EventHandler CanExecuteChanged; // I think this is for CanExecute()

        // Constructor
        public TargetViewModelCommand(Action action)
        {
            TargetViewModelAction = action;
        }

        public bool CanExecute(object parameter)
        {
            // Some kind of check should go here
            return true;
        }

        public void Execute(object parameter)
        {
            TargetViewModelAction();
        }
    }
}
