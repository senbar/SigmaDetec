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
                ConvertFromBytesToPixels();
            }
            catch (Exception ex)
            {
                throw new RedColourAnalyizerException(ex.Message);                
            }
        }

        public void AnalizeImage()
        {
            
            foreach (var pixel in PixelImage)
            {
                if(pixel.Red == 255)
                {
                    RedPixels.Add(pixel);
                }
            }
        }


        private void ConvertFromBytesToPixels()
        {
            for (int i = 0; i < this.ImageToProcess.Length; i+=4)
            {
                var alfa = ImageToProcess[i];
                var red = ImageToProcess[i + 1];
                var green = ImageToProcess[i + 2];
                var blue = ImageToProcess[i + 3];
                var px = 1 % ImageWidth;
                int py = (1 / ImageWidth);
                var point = new Point(px,py);
                var PointToAdd = new Pixel(alfa,red,green,blue, point);
                PixelImage.Add(PointToAdd);
            }
        }

        public void CreateRectangleCoordinades()
        {
            var averageOfX = RedPixels.Average(x => x.Point.X);
            var averageOfY = RedPixels.Average(x => x.Point.Y);
            Console.WriteLine("Average x = {0} ", averageOfX);
            Console.WriteLine("Average y = {0} ", averageOfY);
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
