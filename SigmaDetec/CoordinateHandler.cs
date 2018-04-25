using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec
{
    class CoordinateHandler
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public CoordinateHandler(double width, double height)
        {
            Width = width;
            Height = height;

        }
    }
}
