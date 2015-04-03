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

namespace GUI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string m_title;
        private BitmapSource m_cameraImage;
        private Capture m_capture;

        // ViewModel Instances
        private TargetListViewModel targetListViewModel;

        public MainWindowViewModel()
        {
            Title = "SAD.3.14 Controls";

            TakePictureCommand = new MyCommand(TakePicture);

        }

        // Properties
        public TargetListViewModel TargetListViewModel
        {
            get { return targetListViewModel; }
            set { targetListViewModel = value; }
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

        #region Camera Stuff
        private void TakePicture()
        {
            if (m_capture == null)
                m_capture = new Capture(0);

            // take a picture

            var image = m_capture.QueryFrame();
            //image.Save(@"c:\data\test.png");

            var wpfImage = ConvertImageToBitmap(image);
            CameraImage = wpfImage;
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr ptr);
        private static BitmapSource ConvertImageToBitmap(IImage image)
        {
            if (image != null)
            {
                using (var source = image.Bitmap)
                {
                    var hbitmap = source.GetHbitmap();
                    try
                    {
                        var bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero,
                                                     Int32Rect.Empty,
                                                     BitmapSizeOptions.FromEmptyOptions());
                        DeleteObject(hbitmap);
                        bitmap.Freeze();
                        return bitmap;
                    }
                    catch
                    {
                        image = null;
                    }
                }
            }
            return null;
        }

        public BitmapSource CameraImage
        {
         get { return m_cameraImage; }
         set
         {
            if(Equals(value, m_cameraImage))
            {
               return;
            }
            m_cameraImage = value;
            OnPropertyChanged();
         }
      }
      #endregion

        public ICommand TakePictureCommand { get; set; }


    }

    /* Moved to its own seperate file
     * 
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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
    */ 
}
