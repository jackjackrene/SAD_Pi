using SAD.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Commands.TargetListViewModelCommands;
using SAD.Core.Devices;

namespace GUI.ViewModel
{
    public class TargetListViewModel : ViewModelBase
    {
        // Fields
        private TargetManager targetManager;
        private TargetViewModel selectedTargetView;
        private ObservableCollection<TargetViewModel> targetViewList;
        private SADMissileLauncher missileLauncher; // needed for KillAll, KillFriends, KillFoes

        public TargetListViewModel()
        {
            TargetManager = TargetManager.GetInstance();
            TargetViewList = new ObservableCollection<TargetViewModel>();

            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            MissileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);

            // Command stuff
            ClearAllCommand = new TargetListViewModelCommand(ClearAll);
            LoadTargetsCommand = new TargetListViewModelCommand(LoadTargets);
            KillAllCommand = new TargetListViewModelCommand(KillAll);
            KillFoesCommand = new TargetListViewModelCommand(KillFoes);
            KillFriendsCommand = new TargetListViewModelCommand(KillFriends);
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

        public SADMissileLauncher MissileLauncher
        {
            get { return missileLauncher; }
            set { missileLauncher = value; }
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

        public void KillAll()
        {
            List<Target> list = TargetManager.GetAllTargets.ToList();

            for (int count = 0; count < list.Count; count++)
            {
                missileLauncher.Kill(list[count].Phi, list[count].Theta);
            }
        }

        public void KillFoes()
        {
            List<Target> list = TargetManager.GetEnemies.ToList();

            for (int count = 0; count < list.Count; count++)
            {
                missileLauncher.Kill(list[count].Phi, list[count].Theta);
            }
        }

        public void KillFriends()
        {
            List<Target> list = TargetManager.GetFriends.ToList();

            for (int count = 0; count < list.Count; count++)
            {
                missileLauncher.Kill(list[count].Phi, list[count].Theta);
            }
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
        public ICommand KillAllCommand { get; set; }
        public ICommand KillFriendsCommand { get; set; }
        public ICommand KillFoesCommand { get; set; }
    }
}