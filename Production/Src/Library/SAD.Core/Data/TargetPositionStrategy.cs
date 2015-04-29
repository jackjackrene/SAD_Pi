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

            // use camera to detect positions
            Bitmap sourceImageGreen;
            Bitmap sourceImageRed;
            Capture capture;
            capture = new Capture(0);
            //sourceImage = capture.QueryFrame();
            var image = capture.QueryFrame();
            sourceImageGreen = image.ToBitmap();
            sourceImageRed = image.ToBitmap();


            // set image to green, find circles
            EuclideanColorFiltering filterGreen = new EuclideanColorFiltering();
            filterGreen.CenterColor = new AForge.Imaging.RGB(215, 30, 30);
            filterGreen.Radius = 100;
            filterGreen.ApplyInPlace(sourceImageGreen);

            HoughCircleTransformation circleTransformGreen = new HoughCircleTransformation(35);
            circleTransformGreen.ProcessImage(sourceImageGreen);
            Bitmap houghCircleImageGreen = circleTransformGreen.ToBitmap();
            HoughCircle[] circlesGreen = circleTransformGreen.GetCirclesByRelativeIntensity(0.5);

            foreach (HoughCircle circle in circlesGreen)
            {
                // process circles

            }

            // set image to red, find circles
            EuclideanColorFiltering filterRed = new EuclideanColorFiltering();
            filterRed.CenterColor = new AForge.Imaging.RGB(215, 30, 30);
            filterRed.Radius = 100;
            filterRed.ApplyInPlace(sourceImageRed);

            HoughCircleTransformation circleTransformRed = new HoughCircleTransformation(35);
            circleTransformRed.ProcessImage(sourceImageRed);
            Bitmap houghCircleImageRed = circleTransformRed.ToBitmap();
            HoughCircle[] circlesRed = circleTransformRed.GetCirclesByRelativeIntensity(0.5);

            foreach (HoughCircle circle in circlesRed)
            {
                // process circles

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
