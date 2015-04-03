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
    public class TargetListViewModel : ViewModelBase
    {
        // Fields
        private TargetManager targetManager;
        private TargetViewModel selectedTargetView;
        private ObservableCollection<TargetViewModel> targetViewList; 

        public TargetListViewModel()
        {
            TargetManager = TargetManager.GetInstance();
            TargetViewList = new ObservableCollection<TargetViewModel>();

            // Command stuff
            ClearAllCommand = new TargetListViewModelCommand(ClearAll);
            LoadTargetsCommand = new TargetListViewModelCommand(LoadTargets);
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
            set { selectedTargetView = value; OnPropertyChanged(); }
        }

        // Methods
        public void ClearAll()
        {
            // May or may not work
            TargetManager.TargetList = null;
            TargetViewList.Clear();
        }

        public void LoadTargets()
        {
            PopulateTargetList();
        }

        // May be better of as private
        // Modify so triggered on an event
        public void PopulateTargetList()
        {
            List<Target> list = TargetManager.GetAllTargets.ToList();

            // For every target in the list
            for (int count = 0; count < list.Count; count++)
            {
                var target = list[count];
                var newTargetViewModel = new TargetViewModel(target);

                targetViewList.Add(newTargetViewModel);
                SelectedTargetView = newTargetViewModel;
            }
        }

        // Commands
        public ICommand ClearAllCommand { get; set; }
        public ICommand LoadTargetsCommand { get; set; }
    }
}
