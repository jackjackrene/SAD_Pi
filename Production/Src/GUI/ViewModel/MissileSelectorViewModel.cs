using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GUI.ViewModel.MissileSelectorViewModel
{
    class MissileSelectorViewModel : ViewModelBase
    {
        private string m_launcherTitle;
        private string m_launcherType;

        public MissileSelectorViewModel()
        {
            LauncherTitle = "Select Missile Launcher";
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

        public void LauncherSelectMock()
        {
            LauncherType = "Mock";
        }

        public void LauncherSelectDreamCheeky()
        {
            LauncherType = "Dream Cheeky";
        }
    }
}
