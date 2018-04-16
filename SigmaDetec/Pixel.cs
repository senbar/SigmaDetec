using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigamDetec
{
    public class Pixel
    {
        public int Alfa { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Point Point { get; set; }

        public Pixel(int alfa, int red, int green, int blue, Point point)
        {
            this.Alfa = alfa;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Point = point;


        }
    }
}
