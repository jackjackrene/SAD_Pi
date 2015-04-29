using System;
using System.Collections.Generic;
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

namespace GUI.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        private string m_title;
        private string m_ip;
        private string m_port;
        private string m_teamName;
        private BitmapSource bitmapImage;
        private readonly Capture capture;
        private bool isRunning;
        private CancellationTokenSource cts;
        private BlockingCollection<Image<Bgr, byte>> imageBlockingCollection;
        private BlockingCollection<Image<Bgr, byte>> processBuffer;

        // ViewModel Instances
        private TargetListViewModel targetListViewModel;

        public MainWindowViewModel()
        {
            Title = "SAD.3.14 Controls";

            this.TargetListViewModel = new TargetListViewModel();

            this.capture = new Capture();
            cts = new CancellationTokenSource(); // necessary to indicate cancellation of tasks.
            imageBlockingCollection = new BlockingCollection<Image<Bgr, byte>>(); // Acts as a FIFO. Part of the .NET framework as of .NET 4.0. No bounded capacity.
            processBuffer = new BlockingCollection<Image<Bgr, byte>>();
            this.Start = ReactiveCommand.Create(this.WhenAnyValue(x => x.IsRunning).Select(x => x == false));
            this.Start.Subscribe(x => this.StartAcquisition());

            this.Stop = ReactiveCommand.Create(this.WhenAnyValue(x => x.IsRunning).Select(x => x == true));
            this.Stop.Subscribe(x => this.StopAcquisition());

            this.IsRunning = false;
        }

        // Properties
        public TargetListViewModel TargetListViewModel
        {
            get { return this.targetListViewModel; }
            set { targetListViewModel = value; this.OnPropertyChanged(); }
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
        public string IP
        {
            get { return m_ip; }
            set
            {
                m_ip = value;
                OnPropertyChanged();
            }
        }
        public string Port
        {
            get { return m_port; }
            set
            {
                m_port = value;
                OnPropertyChanged();
            }
        }
        public string TeamName
        {
            get { return m_teamName; }
            set
            {
                m_teamName = value;
                OnPropertyChanged();
            }
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
