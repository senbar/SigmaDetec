using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
namespace SigamDetec
{
    public class RedColourAnalyzer
    {
        private byte[] ImageToProcess { get; set; }
        private List<Pixel> RedPixels { get; set; }
        private List<Pixel> PixelImage { get; set; }
        private int ImageWidth { get; set; }
        private int NumberToDivide = 10000;
        private double AverageX { get; set; }
        private double AverageY { get; set; }
        public RedColourAnalyzer(byte[] image,int imagewidth)
        {
            try
            {
                this.ImageToProcess = image;
                this.ImageWidth = imagewidth;
                PixelImage = new List<Pixel>();
                RedPixels = new List<Pixel>();
                ConvertFromBytesToPixels();
                FindRedPixels();
                CreateRectangleCentre();

            }
            catch (Exception ex)
            {
                throw new RedColourAnalyizerException(ex.Message);                
            }
        }

        private void FindRedPixels()
        {
            foreach (var pixel in PixelImage)
            {
                if(pixel.Red != 0)
                {
                    RedPixels.Add(pixel);
                }
            }
        }


        private void ConvertFromBytesToPixels()
        {
            int j = 0;
            for (int i = 0; i < this.ImageToProcess.Length; i+=4)
            {
                var alfa = ImageToProcess[i];
                var red = ImageToProcess[i + 1];
                var green = ImageToProcess[i + 2];
                var blue = ImageToProcess[i + 3];
                var px = j % ImageWidth;
                int py = (j / ImageWidth);
                var point = new Point(px,py);
                var PointToAdd = new Pixel(alfa,red,green,blue, point);
                PixelImage.Add(PointToAdd);
                j++;
            }
        }

        private Tuple<double,double> CreateRectangleCentre()
        {
            AverageX = RedPixels.Select(x => x.Point.X).Average();
            AverageY = RedPixels.Select(x => x.Point.Y).Average();
            
            return new Tuple<double, double>(AverageX, AverageY); ;
        }

        public Rectangle CreateRectangleLeftCorner()
        {
            var size = ImageToProcess.Length / NumberToDivide;
            var RecX1 = AverageX - (size / 2);
            var RecX2 = AverageX + (size / 2);
            var RecY1 = AverageY - (size / 2);
            var RecY2 = AverageY + (size / 2);
            var RectWidth = RecX2 - RecX1;
            var RectHeight = RecY2 - RecY1;
            return new Rectangle((int)RecX1, (int)RecY1, (int)RectWidth, (int)RectHeight);

        }
    }

    public class RedColourAnalyizerException : Exception
    {
        public RedColourAnalyizerException()
        {

        }
        public RedColourAnalyizerException(string message) : base(message)
        {

        }
    }
}
