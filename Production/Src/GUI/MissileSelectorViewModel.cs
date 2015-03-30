﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GUI
{
    class MissileSelectorViewModel : MissileSelectorViewModelBase
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
}
