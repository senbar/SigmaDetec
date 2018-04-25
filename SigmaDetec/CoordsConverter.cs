using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec
{
    public static class CoordsConverter
    {

        public static double ToInverseCoords(double coord) 
            {
                return coord = coord * 670;

            }
    }
}
