using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Concurrent;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace SAD.Core
{
    public interface ITargetPositionStrategy
    {
        void CalculatePhiTheta();
    }

    public class DetectPosition : ITargetPositionStrategy
    {
        public void CalculatePhiTheta()
        {
            SAD.Core.TargetManager targetManager = SAD.Core.TargetManager.GetInstance();
            List<SAD.Core.Target> TargetList = new List<SAD.Core.Target>();
            TargetList = targetManager.GetAllTargets.ToList();
            int currentTargetNum = 0;
            int radius;
            double x;
            double y;
            double height;
            double width;
            double phi;
            double theta;
            double a;
            double b;
            double c;

            // use camera to detect positions
            Bitmap sourceImage;
            Capture capture;
            capture = new Capture(1);
            //sourceImage = capture.QueryFrame();
            var image = capture.QueryFrame();
            sourceImage = image.ToBitmap();

            var filter = new FiltersSequence(new IFilter[]
            {
                Grayscale.CommonAlgorithms.BT709,                                                     
                new Threshold(0x40)
            });
            var binaryImage = filter.Apply(sourceImage);
            // create filter
            HomogenityEdgeDetector filter2 = new HomogenityEdgeDetector();
            // apply the filter
            filter2.ApplyInPlace(binaryImage);
            // find circles
            HoughCircleTransformation circleTransform = new HoughCircleTransformation(150);
            circleTransform.ProcessImage(binaryImage);
            Bitmap houghCircleImage = circleTransform.ToBitmap();
            HoughCircle[] circles = circleTransform.GetMostIntensiveCircles(4);
            // houghCircleImage.Save("houghImage.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            height = houghCircleImage.Height;
            width = houghCircleImage.Width;

            foreach (HoughCircle circle in circles)
            {
                // process circles
                radius = circle.Radius;
                x = circle.X;
                y = circle.Y;
                a = width / 2 - x;
                b = y;
                c = Math.Sqrt(a * a + b * b);
                phi = Math.Acos(a / c);
                theta = Math.Asin(b / c);
                TargetList[currentTargetNum].Phi = phi;
                TargetList[currentTargetNum].Theta = theta;
                currentTargetNum++;
            }
        }
    }

    public class UseCurrentXYZ : ITargetPositionStrategy
    {
        public void CalculatePhiTheta()
        {
            // use spherical conversion to calculate positions
            SAD.Core.TargetManager targetManager = SAD.Core.TargetManager.GetInstance();
            List<SAD.Core.Target> TargetList = new List<SAD.Core.Target>();
            TargetList = targetManager.GetAllTargets.ToList();
            int listSize = TargetList.Count;
            for (int i = 0; i < listSize; i++)
            {
                SAD.Core.Spherical.ConvertToSphere(TargetList[i]);
            }
        }
    }
    public class CalculatePositions
    {
        private readonly ITargetPositionStrategy _strategy;

        public CalculatePositions(ITargetPositionStrategy strategy)
        {
            _strategy = strategy;
        }

        public void CalculatePhiTheta()
        {
            _strategy.CalculatePhiTheta();
        }
    }
}
