using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigamDetec
{
    public class RedColourAnalyzer
    {
        private byte[] ImageToProcess { get; set; }
        private List<Pixel> RedPixels { get; set; }
        private List<Pixel> PixelImage { get; set; }
        private int ImageWidth { get; set; }
        public RedColourAnalyzer(byte[] image,int imagewidth)
        {
            try
            {
                this.ImageToProcess = image;
                this.ImageWidth = imagewidth;
                PixelImage = new List<Pixel>();
                RedPixels = new List<Pixel>();
                ConvertFromBytesToPixels();
            }
            catch (Exception ex)
            {
                throw new RedColourAnalyizerException(ex.Message);                
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

        public Tuple<double,double> CreateRectangleCentre()
        {
            var averageOfX = RedPixels.Select(x => x.Point.X).Average();
            var averageOfY = RedPixels.Select(x => x.Point.Y).Average();
            return new Tuple<double, double>(averageOfX, averageOfY);
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
