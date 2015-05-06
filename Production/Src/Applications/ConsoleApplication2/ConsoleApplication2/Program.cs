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
    class Program
    {
        static void Main(string[] args)
        {
            int currentTargetNum = 0;
            int radius;
            int x;
            int y;
            int numCircles = 0;
            int height;
            int width;

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
            houghCircleImage.Save("houghImage.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            height = houghCircleImage.Height;
            width = houghCircleImage.Width;

            foreach (HoughCircle circle in circles)
            {
                // process circles
                radius = circle.Radius;
                x = circle.X;
                y = circle.Y;
                currentTargetNum++;
                // calculate positions and update targets
                Console.Out.WriteLine("{0} and {1} and {2}", x, y, radius);
                numCircles++;
            }
            Console.Out.WriteLine("{0} circles found", numCircles);
            Console.Out.WriteLine("Hit enter to quit: ");
            Console.ReadLine();
        }
    }
}
