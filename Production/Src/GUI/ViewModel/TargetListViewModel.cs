using SAD.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Commands.TargetListViewModelCommands;

namespace GUI.ViewModel
{
    class TargetListViewModel
    {
        /*
         *  ViewModel for the UserControl containing the ListBox of TargetViews/ViewModels and a button (TargetManager)
         *  
         *  NO ONE TOUCH THIS OR SO HELP ME
         */ 

        // Fields
        private TargetManager targetManager;
        private TargetViewModel selectedTargetView;
        private ObservableCollection<TargetViewModel> targetViewList;

        public TargetListViewModel()
        {
            TargetManager = TargetManager.GetInstance();

            // dont forget to populate
            TargetViewList = new ObservableCollection<TargetViewModel>();

            // Command stuff
            ClearAllCommand = new TargetListViewModelCommand(ClearAll);
        }

        // Properties
        public TargetManager TargetManager
        {
            get { return targetManager; }
            set { targetManager = value; }
        }

        public ObservableCollection<TargetViewModel> TargetViewList
        {
            get { return targetViewList; }
            set { targetViewList = value; }
        }

        public TargetViewModel SelectedTargetView
        {
            get { return selectedTargetView; }
            set { selectedTargetView = value; }
        }

        /*
         *  General idea, for every target in TargetManager, 
         *  get the target
         *  send an instance to TargetView (via constructor)
         *  This will populate the list 
         * 
         *  ListView will contain TargetView/ViewModel (see his example)
         * 
         *  Question: how to notify THIS ViewModel that a target has been killed
         */ 

        // Methods
        public void ClearAll()
        {
            // May or may not work
            TargetManager.TargetList = null;
            TargetViewList.Clear();
        }

        // Commands
        public ICommand ClearAllCommand { get; set; }
    }
}
