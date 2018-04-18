using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SigmaDetec.USB;
namespace SigamDetec
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Communicator communicator;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            communicator = new Communicator();
            communicator.test();
        }
    }
}
