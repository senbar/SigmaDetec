using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigamDetec
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            try
            {
                this.X = x;
                this.Y = y;
            }
            catch (Exception ex) 
            {
                throw new PointException(ex.Message);
            }
            
        }
    }

    public class PointException : Exception
    {
        public PointException()
        {

        }

        public PointException(string message): base(message)
        {

        }
    }
}
