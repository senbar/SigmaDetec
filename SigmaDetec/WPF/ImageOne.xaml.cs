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
        
        public ImageOne()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            
            
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

        private void OpenClawButton_Click(object sender, RoutedEventArgs e)
        {
            USB.Communicator.WriteLine(USB.MovementEncoder.EncodeCloseGrabber());
        }

        private void CloseClawButton_Click(object sender, RoutedEventArgs e)
        {
            USB.Communicator.WriteLine(USB.MovementEncoder.EncodeOpenGrabber());

        }
    }
}
