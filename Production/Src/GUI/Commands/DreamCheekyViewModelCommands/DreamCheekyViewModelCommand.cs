using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.Commands.DreamCheekyViewModelCommands
{
    class DreamCheekyViewModelCommand : ICommand
    {
        // Fields
        private Action DreamCheekyViewModelAction;
        public event EventHandler CanExecuteChanged; // I think this is for CanExecute()

        // Constructor
        public DreamCheekyViewModelCommand(Action action)
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
