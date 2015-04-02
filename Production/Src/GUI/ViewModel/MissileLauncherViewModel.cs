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
using GUI.ViewModel;
using GUI.Commands.MissileLauncheViewModelCommands;
using GUI.Commands.TargetViewModelCommands;

namespace GUI.ViewModel
{
    public class MissileLauncherViewModel : ViewModelBase
    {
        // Fields
        private SADMissileLauncher missileLauncher;
        private Target target;  // Pretty sure this is necessary for the bindings on the .xaml page,
                                // but otherwise it is not use in here per se
        private double degreeConstant;

        public double DegreeConstant
        {
            get { return degreeConstant; }
            set { degreeConstant = value; }
        }

        // Constructor
        public MissileLauncherViewModel()
        {
            DegreeConstant = 5.0;
           
            // Trouble area, make sure its the correct missile launcher in the debugger
            SADMissileLauncherFactory missileFactory = SADMissileLauncherFactory.GetInstance();
            MissileLauncher = missileFactory.CreateSADMissileLauncher(SADMissileLauncher.MissileLauncherType);

            FireCommand = new MissileLauncherViewModelCommand(Fire);
            MoveUpCommand = new TargetViewModelCommand(MoveUp);
            MoveDownCommand = new TargetViewModelCommand(MoveDown);
            MoveLeftCommand = new TargetViewModelCommand(MoveLeft);
            MoveRightCommand = new TargetViewModelCommand(MoveRight);
        }

        public MissileLauncherViewModel(SADMissileLauncher missileLauncher, Target target)
        {
            DegreeConstant = 5.0;
            MissileLauncher = missileLauncher;
            Target = target;

            // Command stuff
            FireCommand = new MissileLauncherViewModelCommand(Fire);
            MoveUpCommand = new TargetViewModelCommand(MoveUp);
            MoveDownCommand = new TargetViewModelCommand(MoveDown);
            MoveLeftCommand = new TargetViewModelCommand(MoveLeft);
            MoveRightCommand = new TargetViewModelCommand(MoveRight);
        }

        // Properties
        public SADMissileLauncher MissileLauncher
        {
            get { return missileLauncher; }
            set { missileLauncher = value; }
        }

        public Target Target
        {
            get { return target; }
            set { target = value; }
        }

        // Methods
        public void Fire()
        {
            missileLauncher.Fire();
        }

        public void MoveUp()
        {
            missileLauncher.MoveBy(0.0, DegreeConstant);
        }

        public void MoveDown()
        {
            missileLauncher.MoveBy(0.0, -DegreeConstant);
        }

        public void MoveLeft()
        {
            missileLauncher.MoveBy(-DegreeConstant, 0.0);
        }

        public void MoveRight()
        {
            missileLauncher.MoveBy(DegreeConstant, 0.0);
        }

        // Commands
        public ICommand FireCommand { get; set; }
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
    }
}
