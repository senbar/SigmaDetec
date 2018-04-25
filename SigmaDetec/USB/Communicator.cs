﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec.USB
{
    /// <summary>
    /// class for communication with arduino via usb port
    /// </summary>
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

        static public void WriteLine(string command)
        {
            _serialPort.WriteLine(command);
        }

    }
}
