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

namespace ConsoleApplication2
{
    public interface ITargetPositionStrategy
    {
        void CalculatePhiTheta();
    }

    public class DetectPosition : ITargetPositionStrategy
    {
        public void CalculatePhiTheta()
        {
            int currentTargetNum = 0;
            int radius;
            int x;
            int y;

            // use camera to detect positions
            Bitmap sourceImage;
            Capture capture;
            capture = new Capture(0);
            //sourceImage = capture.QueryFrame();
            var image = capture.QueryFrame();
            sourceImage = image.ToBitmap();

            // find circles
            HoughCircleTransformation circleTransform = new HoughCircleTransformation(35);
            circleTransform.ProcessImage(sourceImage);
            Bitmap houghCircleImage = circleTransform.ToBitmap();
            HoughCircle[] circles = circleTransform.GetCirclesByRelativeIntensity(0.5);

            foreach (HoughCircle circle in circles)
            {
                // process circles
                radius = circle.Radius;
                x = circle.X;
                y = circle.Y;
                currentTargetNum++;
                // calculate positions and update targets
            }
        }
    }

    public class UseCurrentXYZ : ITargetPositionStrategy
    {
        public void CalculatePhiTheta()
        {

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
