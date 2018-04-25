using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec.USB
{
    /// <summary>
    /// class for encoding braccio movement into string commands sent via usb com port
    /// </summary>

    static public class MovementEncoder
    {
        private const float GRIPPER_CLOSED = 10;
        private const float GRIPPER_OPEN = 38;
       
        static public string  EncodeArmMovement(float x, float y, float z)
        {
            string command = String.Format("Move:{0}:{1}:{2}",x,y,z);
            return command;
        }

        static public string EncodeGripperRotation(float rotation)
        {
            string command = String.Format("Rotate:{0}", rotation);
            return command;
        }

        static public string EncodeGripperGape(float gape)
        {
            string command = String.Format("Grabber:{0}", gape);
            return command;
        }

        static public string EncodeOpenGrabber()
        {
            return EncodeGripperGape(GRIPPER_OPEN);
        }
        static public string EncodeCloseGrabber()
        {
            return EncodeGripperGape(GRIPPER_CLOSED);
        }


    }
}
