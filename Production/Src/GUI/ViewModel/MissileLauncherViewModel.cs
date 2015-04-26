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
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class MissileLauncherViewModel : ViewModelBase
    {
        // Fields
        private SADMissileLauncher missileLauncher;
        private Target target;  // Pretty sure this is necessary for the bindings on the .xaml page,
                                // but otherwise it is not use in here per se 
        private int m_currentMissileCount;
        private string m_currentPhiTheta;

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
            CurrentMissileCount = missileLauncher.CurrentMissileCount;
            ReloadCommand = new TargetViewModelCommand(Reload);
            CalibrateCommand = new TargetViewModelCommand(Calibrate);

            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi,2) + ", " + Math.Round(missileLauncher.CurrentTheta,2) + ")";
            m_currentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi,2) + ", " + Math.Round(missileLauncher.CurrentTheta,2) + ")";
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
            ReloadCommand = new TargetViewModelCommand(Reload);



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

        public int CurrentMissileCount
        {
            get { return m_currentMissileCount; }
            set
            {
                m_currentMissileCount = value;
                OnPropertyChanged("CurrentMissileCount");
            }
        }

        public string CurrentPhiTheta
        {
            get { return m_currentPhiTheta; }
            set
            {
                m_currentPhiTheta = value;
                OnPropertyChanged("CurrentPhiTheta");
            }
        }

        // Methods
        public async void Fire()
        {
            Task fireTask = Task.Run(() =>
                {
                    missileLauncher.Fire();
                });
            await fireTask;
            Task updateUITask = Task.Run(() =>
                
            CurrentMissileCount = missileLauncher.CurrentMissileCount;
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
               
        public void MoveUp()
        {
            missileLauncher.MoveBy(0.0, DegreeConstant);
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
        }

        public void MoveDown()
        {
            missileLauncher.MoveBy(0.0, -DegreeConstant);
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
        }

        public void MoveLeft()
        {
            missileLauncher.MoveBy(-DegreeConstant, 0.0);
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
        }

        public void MoveRight()
        {
            missileLauncher.MoveBy(DegreeConstant, 0.0);
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
        }
        public void Reload()
        {
            missileLauncher.Reload();
            CurrentMissileCount = missileLauncher.CurrentMissileCount;
        }
        public void Calibrate()
        {
            missileLauncher.Move(0, 0);
            CurrentPhiTheta = "(" + Math.Round(missileLauncher.CurrentPhi, 2) + ", " + Math.Round(missileLauncher.CurrentTheta, 2) + ")";
        }

        // Commands
        public ICommand FireCommand { get; set; }
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand UpdateMissileCountCommand { get; set; }
        public ICommand UpdatePhiThetaCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand CalibrateCommand { get; set; }

    }

}
