using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec.Graphics
{
    static class Tracking 
    {
        /// <summary>
        /// draw rectangle with given coordinates to highlist detected object
        /// </summary>
        /// <param name="Rect"></param>
        static public void DrawRectangle(Rectangle Rect)
        {
            using (System.Drawing.Graphics bmpGraphics =
                System.Drawing.Graphics.FromImage(this.colorDrawingBitmap))
            {
                bmpGraphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Brushes.Pink, 5), Rect);

            }
        }
    }
}
