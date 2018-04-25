using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SigmaDetec.USB
{
    /// <summary>
    /// class for amanaging avaiable com ports
    /// </summary>
    class PortManager
    {
       public String[] ports;
        
        public void GetAvailablePorts()
        {
            ports = SerialPort.GetPortNames();
        }

    }
}
