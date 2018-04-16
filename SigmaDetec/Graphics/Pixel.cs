using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec.Graphics
{
    class Pixel
    {
        public byte this[int key]
        {
            get
            {
                return ARGB[key];
            }
        }
        byte[] ARGB;
        public Pixel(byte A, byte R, byte G, byte B)
        {
            ARGB = new byte[4];
            ARGB[0] = A;
            ARGB[1] = R;
            ARGB[2] = G;
            ARGB[3] = B;
        }
        
}
}

