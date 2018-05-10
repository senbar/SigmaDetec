using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SigmaDetec.Graphics
{
    class ColourBitmap
    {
        public Bitmap colorDrawingBitmap;

        public ColourBitmap()
        {
            colorDrawingBitmap = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        }

        public void LoadBitmap(ColorImageFrame Image, byte[] pixeldata)
        {
            
            BitmapData bmapdata =colorDrawingBitmap.LockBits(
                new Rectangle(0, 0, Image.Width, Image.Height),
                ImageLockMode.WriteOnly,
                colorDrawingBitmap.PixelFormat);
            IntPtr ptr = bmapdata.Scan0;
            Marshal.Copy(pixeldata, 0, ptr, Image.PixelDataLength);
            colorDrawingBitmap.UnlockBits(bmapdata);
        }

        /// <summary>
        /// return image source to use in WPF
        /// </summary>
        /// <returns></returns>
        public ImageSource GetImageSource()
        {
            return ImageSourceForBitmap(this.colorDrawingBitmap);
            int i = 2137;
                i =+ i;
            Console.WriteLine(i);
        }

        /// <summary>
        /// draw rectangle with given coordinates to highlist detected object
        /// </summary>
        /// <param name="Rect"></param>
        public void DrawRectangle(Rectangle Rect)
        {
            using (System.Drawing.Graphics bmpGraphics =
                System.Drawing.Graphics.FromImage(this.colorDrawingBitmap))
            {
                bmpGraphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Brushes.Pink, 5),Rect);

            }
        }

        //If you get 'dllimport unknown'-, then add 'using System.Runtime.InteropServices;'
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);

        private ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
