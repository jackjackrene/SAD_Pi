using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Concurrent;
using System.Threading;
using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Threading;
using Timer = System.Timers.Timer;
using TargetServerCommunicator;
using TargetServerCommunicator.Servers;
using SAD.Core.Server.ServerDataCoverter;
using System.Timers;
using SAD.Core.EricStrategy;

namespace GUI.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        private string m_title;
        private string m_ip;
        private int m_port;
        private string m_teamName;
        private string m_game;
        private string m_selectedGame;
        private IGameServer m_server;
        private static Timer myTimer;

        private BitmapSource bitmapImage;
        private readonly Capture capture;
        private bool isRunning;
        private CancellationTokenSource cts;
        private BlockingCollection<Image<Bgr, byte>> imageBlockingCollection;
        private BlockingCollection<Image<Bgr, byte>> processBuffer;
        private IStrategy m_strategy;

        // ViewModel Instances
        private TargetListViewModel targetListViewModel;
        
        public MainWindowViewModel()
        {
            Title = "SAD.3.14 Controls";
            this.TargetListViewModel = new TargetListViewModel();
            m_ip = "192.168.1.3";
            m_port = 0000;
            m_teamName = "Desperate Housewives";
            this.capture = new Capture();
            cts = new CancellationTokenSource(); // necessary to indicate cancellation of tasks.
            imageBlockingCollection = new BlockingCollection<Image<Bgr, byte>>(); // Acts as a FIFO. Part of the .NET framework as of .NET 4.0. No bounded capacity.
            processBuffer = new BlockingCollection<Image<Bgr, byte>>();
            this.Start = ReactiveCommand.Create(this.WhenAnyValue(x => x.IsRunning).Select(x => x == false));
            this.Start.Subscribe(x => this.StartAcquisition());

            this.Stop = ReactiveCommand.Create(this.WhenAnyValue(x => x.IsRunning).Select(x => x == true));
            this.Stop.Subscribe(x => this.StopAcquisition());

            this.IsRunning = false;
            Games = new ObservableCollection<String>();



              ConnectToGameServerCommand = new MyCommand(ConnectToGameServer);
         //   GetGameListCommand = new MyCommand(GetGameList);
          //  GetTargetsCommand = new MyCommand(GetTargets);
              StartGameCommand = new MyCommand(StartGame);
            StopGameCommand = new MyCommand(StopGame);

            m_strategy = new KillAllStrategy();
        }
        /// <summary>
        ///  Game Server shhhhhhtuff. 
        /// </summary>
        private void ConnectToGameServer()
        {
            m_server = GameServerFactory.Create(GameServerType.WebClient, TeamName, IP, Port);
            m_server.StopRunningGame();
            // Returns an IEnumerable of strings... a collection. 
            var games = m_server.RetrieveGameList();
            
            foreach(var game in games){
                Games.Add(game);
            }
        }
     
        /// <summary>
        /// Gets a list of targets for the game that was selected. 
        /// Still need to implement. Must add each temporaryTarget to a list of targets
        /// then add the list of targets to our targetmanager. 
        /// </summary>
        private void GetTargets()
        {
            SAD.Core.Server.ServerDataCoverter.TargetConverter targetConverter = new SAD.Core.Server.ServerDataCoverter.TargetConverter();

            if (m_server == null)
            {
                return;
            }
            if (SelectedGame == null)
            {
                return;
            }
            m_strategy.GetTargetAndKillIt();
            // Translate the gameservercommunicatortarget into your own...
            // Then add to your own collection of targets bound by the view. 
            //var targets = m_server.RetrieveTargetList(SelectedGame);
            //foreach (var target in targets)
            //{
            //    var temporaryTarget = targetConverter.convertServerTarget(target);


            //}

        }
        private async void StartGame()
        {

            //if (m_server == null)
            //{
            //    return;
            //}
            //if (SelectedGame == null)
            //{
            //    return;
            //}
            // declare the strategy and use it.  0
             Task runGameTask = Task.Run(() =>
                 {
            m_strategy.GetTargetAndKillIt();
             });
             await runGameTask;


           
        }
        private void StopGame()
        {
            if (m_server == null)
            {
                return;
            }
            if (SelectedGame == null)
            {
                return;
            }
            m_server.StopRunningGame();
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
                OnPropertyChanged("Title");
            }
        }
        public string IP
        {
            get { return m_ip; }
            set
            {
                m_ip = value;
                OnPropertyChanged("IP");
            }
        }
        public int Port
        {
            get { return m_port; }
            set
            {
                m_port = value;
                OnPropertyChanged("Port");
            }
        }

        public string TeamName
        {
            get { return m_teamName; }
            set
            {
                m_teamName = value;
                OnPropertyChanged("TeamName");
            }
        }
        public string Game
        {
            get { return m_game; }
            set
            {
                m_game = value;
            }
        }
        public ObservableCollection<string> Games
        {
            get;
            set;
        }
        public string SelectedGame
        {
            get { return m_selectedGame; }
            set
            {
                if (value == m_selectedGame)
                {
                    return;
                }
                m_selectedGame = value;
                OnPropertyChanged();
            }
        }


        // Properties
        public TargetListViewModel TargetListViewModel
        {
            get { return this.targetListViewModel; }
            set { targetListViewModel = value; this.OnPropertyChanged(); }
        }
     
        private void StartAcquisition()
        {
            this.IsRunning = true;
            cts = new CancellationTokenSource();
            imageBlockingCollection = new BlockingCollection<Image<Bgr, byte>>(); // Acts as a FIFO. Part of the .NET framework as of .NET 4.0. No bounded capacity.
            processBuffer = new BlockingCollection<Image<Bgr, byte>>();

            var producerTask = Task.Run(() => this.ProduceFrame(imageBlockingCollection, cts.Token));
            var consumerTask = Task.Run(() => this.ConsumeFrame(imageBlockingCollection, cts.Token));
        }

        private void StopAcquisition()
        {
            this.IsRunning = false;
            this.cts.Cancel();
        }

        /// <summary>
        /// Producer
        /// </summary>
        private void ProduceFrame(BlockingCollection<Image<Bgr, byte>> bc, CancellationToken ct)
        {
            if (this.capture != null)
            {
                while (!ct.IsCancellationRequested)
                {
                    var image = this.capture.QueryFrame();
                    bc.Add(image);
                }

                bc.CompleteAdding();
            }
        }

        /// <summary>
        /// 1 consumer thread. May have n consumer threads! (Of course the other threads will be doing *other* activities with the frames, not displaying them to the screen!
        /// </summary>
        private void ConsumeFrame(BlockingCollection<Image<Bgr, byte>> bc, CancellationToken ct)
        {
            while (!bc.IsCompleted)
            {
                var imageToDisplay = bc.Take();
                App.Current.Dispatcher.Invoke(
                    () => this.BitmapImage = BitmapConverter.ToBitmapSource(image: imageToDisplay));
                processBuffer.Add(imageToDisplay);
                // property implements IPropertyNotify. Will update fairly quickly. Use dependency properties to make this even faster.
            }

            processBuffer.CompleteAdding();
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.isRunning, value);
            }
        }

        public BitmapSource BitmapImage
        {
            get
            {
                return bitmapImage;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.bitmapImage, value);
            }
        }
  
        public ICommand ConnectToGameServerCommand { get; set; }
        public ICommand GetGameListCommand { get; set; }
        public ICommand GetTargetsCommand { get; set; }
        public ICommand StartGameCommand { get; set; }
        public ICommand StopGameCommand { get; set; }


        public ReactiveCommand<object> Start { get; private set; }
        public ReactiveCommand<object> Stop { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        //    [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
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

    public class BitmapConverter
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr ptr);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}
