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
            int targetListSize = TargetList.Count;
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
