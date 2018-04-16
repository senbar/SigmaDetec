using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace SigmaDetec.Graphics
{
    public class Contour
    {
        private Pixel[][] pixels;
        private byte[] buffer;
        public int Height { get; }
        public int Width { get; }

    public Contour(int hheight, int hwidth)
        {
            Height = hheight;
            Width = hwidth;
            pixels = new Pixel[Width][];
            //fill with transparent pixels
            for(int i =0; i<Width; i++)
            {
                pixels[i] = new Pixel[Height];
                for (int j = 0; j < Height; j++)
                {
                    pixels[i][j] = new Pixel(255, 255, 255, 0);
                }
            }

            buffer = new byte[Width * Height * 4];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        buffer[i*Width*4 + j*(4) + k] = pixels[i][j][k];
                    }
                }
            }
            Console.Write(":(");
        }

        public byte[] ReadPixels()
        {
            return buffer;
        }
    }
}