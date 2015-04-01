using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_OnStartup(object sender, StartupEventArgs e)
        {
            var missileSelectorView = new MissileSelectorView();
            var mainWindow = new MainWindow();

            missileSelectorView.ShowDialog();

            mainWindow.ShowDialog();
        }
    }
}
