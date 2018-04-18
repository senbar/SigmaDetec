using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace SigamDetec
{
    public class RedColorAnalyzer
    {
        private byte[] ImageToProcess { get; set; }
        private List<Pixel> RedPixels { get; set; }
        private List<Pixel> PixelImage { get; set; }
        private int ImageWidth { get; set; }
        private double  NumberToDivide =0.5;
        private double MinSizeOfRectangle = 20;
        private double AverageX { get; set; }
        private double AverageY { get; set; }
        public RedColorAnalyzer(byte[] image,int imagewidth)
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
                throw new RedColorAnalyizerException(ex.Message);                
            }
        }

        public void FindRedPixels()
        {
            foreach (var pixel in PixelImage)
            {
                if(pixel.Red != 0)
                {
                    RedPixels.Add(pixel);
                }
            }
        }


        public void ConvertFromBytesToPixels()
        {
            int j = 0;
            for (int i = 0; i < this.ImageToProcess.Length; i+=4)
            {
                if (ImageToProcess[i + 1] != 0)
                {
                    var red = ImageToProcess[i + 1];
                    var px = j % ImageWidth;
                    int py = (j / ImageWidth);
                    var point = new Point(px, py);
                    var PointToAdd = new Pixel(0, red, 0, 0, point);
                    PixelImage.Add(PointToAdd);
                }
                j++;
            }
        }

        public Tuple<double,double> CreateRectangleCentre()
        {
            if(RedPixels.Count == 0)
            {
                return new Tuple<double, double>(0, 0);
            }
            else
            {
                AverageX = RedPixels.Select(x => x.Point.X).Average();
                AverageY = RedPixels.Select(x => x.Point.Y).Average();

                return new Tuple<double, double>(AverageX, AverageY);
            }
            
        }

        public Rectangle GetRectangle()
        {
            var size = Math.Sqrt( RedPixels.Count )/ NumberToDivide;
            var RecX1 = AverageX - (size / 2);
            var RecX2 = AverageX + (size / 2);
            var RecY1 = AverageY - (size / 2);
            var RecY2 = AverageY + (size / 2);
            var RectWidth = RecX2 - RecX1;
            var RectHeight = RecY2 - RecY1;
            var GeneratedRectangle = new Rectangle((int)RecX1, (int)RecY1, (int)RectWidth, (int)RectHeight);
            if(GeneratedRectangle.Size.Height < MinSizeOfRectangle && GeneratedRectangle.Size.Width < MinSizeOfRectangle)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            return GeneratedRectangle;
        }
    }

    public class RedColorAnalyizerException : Exception
    {
        public RedColorAnalyizerException()
        {

        }
        public RedColorAnalyizerException(string message) : base(message)
        {

        }
    }
}
