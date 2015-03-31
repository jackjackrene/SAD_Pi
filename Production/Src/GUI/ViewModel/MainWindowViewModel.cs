using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GUI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string m_title;

        public MainWindowViewModel()
        {
            Title = "SAD.3.14 Controls";

        }
        /// <summary>
        /// Setting the title for the window
        /// </summary>
        public string Title
        {
            get { return m_title; }
            set
            {
                m_title = value;
                OnPropertyChanged();
            }
        }
    }
}
