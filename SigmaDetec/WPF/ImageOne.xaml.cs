using Microsoft.Kinect;
using SigamDetec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SigmaDetec
{
    /// <summary>
    /// Logika interakcji dla klasy ImageOne.xaml
    /// </summary>
    public partial class ImageOne : Window
    {
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;



        private int Iterator = 0;

        /// <summary>

        /// Intermediate storage for the color data received from the camera
        /// </summary>
        private Graphics.ColourBitmap colorDrawingBitmap;

        private byte[] colorPixels;
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public ImageOne()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {

                // Turn on the color stream to receive color frames
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.colorDrawingBitmap = new Graphics.ColourBitmap();
                // Allocate space to put the pixels we'll receive
                this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];


                // Set the image we display to point to the bitmap where we'll put the image data
                this.Image.Source = this.colorDrawingBitmap.GetImageSource();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.ColorFrameReady += this.SensorColorFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.NoKinectReady;
            }
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {


                if (colorFrame != null)
                {
                    Iterator++;
                    if (Iterator == 1)
                    {
                        // Copy the pixel data from the image to a temporary array
                        colorFrame.CopyPixelDataTo(this.colorPixels);

                        byte[] redPixels = BitmapColorSegmentation.ExtractRedBitmap(this.colorPixels);
                        RedColorAnalyzer RedColorAnalizer = new RedColorAnalyzer(redPixels, colorFrame.Width);

                        var RectangleToDraw = RedColorAnalizer.GetRectangle();
                        //load processed crude buffer and draw rectangle on object
                        colorDrawingBitmap.LoadBitmap(colorFrame, redPixels);

                        //example recttangle todo BESI gimme rectangle of object
                        colorDrawingBitmap.DrawRectangle(RectangleToDraw);
                        //hack to get wpf ovbject from WPF
                        this.Image.Source = colorDrawingBitmap.GetImageSource();
                        Iterator = 0;

                    }

                }
            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var centerpoint = new CoordinateHandler(Image.ActualWidth / 2, Image.ActualHeight / 2);

            System.Windows.Point position = e.GetPosition(Image);
            var coordhandler = new CoordinateHandler(position.X, position.Y);

            var widthtransformed = (position.X - centerpoint.Width) / centerpoint.Width;
            var heighttransformed = (position.Y - centerpoint.Height) / centerpoint.Height;

            var convertedcoords = new CoordinateHandler(CoordsConverter.ToInverseCoords(widthtransformed), CoordsConverter.ToInverseCoords(heighttransformed));

            Console.WriteLine($"x:{convertedcoords.Width} y:{convertedcoords.Height}");
            USB.Communicator.WriteLine(USB.MovementEncoder.EncodeArmMovement((float)convertedcoords.Width, (float)convertedcoords.Height, 300));

        }
    }
}
