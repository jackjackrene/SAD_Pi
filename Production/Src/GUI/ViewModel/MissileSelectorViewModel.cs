using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SAD.Core;
using BuildDefender;
using SAD.Core.Devices;
using SAD.Core.Devices.Missile_Launcher.Subclasses;

namespace GUI.MissileSelectorViewModel
{
    public class MissileSelectorViewModel : MissileSelectorViewModelBase
    {
        private string m_launcherTitle;
        private string m_launcherType;

        public MissileSelectorViewModel()
        {
            LauncherTitle = "Select Missile Launcher";

            LauncherSelectMock = new MyCommand(GetLauncherMock);
            LauncherSelectDreamCheeky = new MyCommand(GetLauncherDreamCheeky);
        }

        public string LauncherTitle
        {
            get { return m_launcherTitle; }
            set
            {
                m_launcherTitle = value;
                OnPropertyChanged();
            }
        }

        public string LauncherType
        {
            get { return m_launcherType; }
            set
            {
                m_launcherType = value;
                OnPropertyChanged();
            }
        }

        public void GetLauncherMock()
        {
            SAD.Core.Devices.SADMissileLauncherFactory missileLauncherFactory = SAD.Core.Devices.SADMissileLauncherFactory.GetInstance();
            // Create a SADDreamCheekyMissileLauncher
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.MockMissileLauncher);
            Application.Current.MainWindow.Close();
        }

        public void GetLauncherDreamCheeky()
        {
            SAD.Core.Devices.SADMissileLauncherFactory missileLauncherFactory = SAD.Core.Devices.SADMissileLauncherFactory.GetInstance();
            // Create a SADDreamCheekyMissileLauncher
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher);
            Application.Current.MainWindow.Close();
        }

        public ICommand LauncherSelectMock { get; set; }
        public ICommand LauncherSelectDreamCheeky { get; set; }
    }

    public class LauncherSelectMock : ICommand
    {
        private MissileSelectorViewModel m_model;
        public LauncherSelectMock(MissileSelectorViewModel model)
        {
            m_model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

      public event EventHandler CanExecuteChanged;
      public void Execute(object parameter)
      {
         m_model.GetLauncherMock();
      }
    }

    public class LauncherSelectDreamCheeky : ICommand
    {
        private MissileSelectorViewModel m_model;
        public LauncherSelectDreamCheeky(MissileSelectorViewModel model)
        {
            m_model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            m_model.GetLauncherDreamCheeky();
        }
    }

    public abstract class MissileSelectorViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
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
