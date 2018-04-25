using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec.USB
{
    static class Communicator
    {
        static SerialPort _serialPort;
        static Communicator()
        {
            _serialPort = new SerialPort();
            ///IF YOU GET ERROR CHANGE TO OPEN COM ON YOUR MACHINE
        }
        
        static public void SetPort(string portName)
        {
            _serialPort.PortName = portName;
        }

        //public void test()
        //{
        //    Console.WriteLine("Available Ports:");
        //    foreach (string s in SerialPort.GetPortNames())
        //    {
        //        Console.WriteLine("   {0}", s);
        //    }
        //    new Task(() =>
        //    {
        //        _serialPort.WriteLine("sdfsdf");
        //    }).Start();
        //}
    }
}
