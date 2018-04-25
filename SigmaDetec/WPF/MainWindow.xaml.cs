using Microsoft.Kinect;
using SigamDetec;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SigmaDetec.USB;
using System.Windows.Controls;

namespace SigmaDetec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        public MainWindow()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
           // code to fill combobox with available coms
            var portmanager = new PortManager();
            portmanager.GetAvailablePorts();
                foreach (string port in portmanager.ports)
                {
                    PortComboBox.Items.Add(port);
                    Console.WriteLine(port);
                    if (portmanager.ports[0] != null)
                    { PortComboBox.SelectedItem = portmanager.ports[0]; }
                }




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

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's ColorFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
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

        /// <summary>
        /// Handles the user clicking on the screenshot button
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ButtonScreenshotClick(object sender, RoutedEventArgs e)
        {
            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.ConnectDeviceFirst;
                return;
            }

            // create a png bitmap encoder which knows how to save a .png file
            BitmapEncoder encoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            // encoder.Frames.Add(BitmapFrame.Create(this.colorBitmap));

            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            // write the new file to disk
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", Properties.Resources.ScreenshotWriteSuccess, path);
            }
            catch (IOException)
            {
                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", Properties.Resources.ScreenshotWriteFailed, path);
            }
        }

        ImageOne imageone;
        private void MainToOneButton_Click(object sender, RoutedEventArgs e)
        {


            if (imageone == null)
            {
                imageone = new ImageOne();
                imageone.Closed += (a, b) => imageone = null;
                imageone.Show();
            }
            else
            { imageone.Show(); }
        }
        ImageTwo imagetwo;
        private void MainToTwoButton_Click(object sender, RoutedEventArgs e)
        {
            if (imagetwo == null)
            {
                imagetwo = new ImageTwo();
                imagetwo.Closed += (a, b) => imagetwo = null;
                imagetwo.Show();
            }
            else
            {
                imagetwo.Show();
            }
        }

        private void portSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Console.WriteLine(e.AddedItems[0] as string);
            Communicator.SetPort(e.AddedItems[0] as string);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            USB.Communicator.WriteLine(USB.MovementEncoder.EncodeArmMovement(100, 200, 300));
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var centerpoint = new CoordinateHandler(Image.ActualWidth/2, Image.ActualHeight/2);

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
