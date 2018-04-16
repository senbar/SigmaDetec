using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigamDetec
{
    public class ImageAverage
    {
        private int NumberOfImagesToProcess { get; set; }
        private int SizeOfImage { get; set; }
        private int[] Memory;

        public ImageAverage(int numberOfImagesToProcess)
        {
            this.NumberOfImagesToProcess = numberOfImagesToProcess;            
        }
        
        public void AddImage(byte[] image)
        {
            int i = 0;
            int[] bytesAsInts = image.Select(x => (int)x).ToArray();
            if (Memory == null)
            {
                Memory = bytesAsInts;
            }
            else
            {
                foreach (var pixel in Memory)
                {
                    Memory[i] += bytesAsInts[i];
                    i++;
                }
            }
            
            SizeOfImage = bytesAsInts.Length;
        }

        public byte[] GenerateAverageImage()
        {
            int[] AverageImage = new int[SizeOfImage];
            for(int i = 0; i<AverageImage.Length; i++)
            {
                AverageImage[i] = Memory[i] / NumberOfImagesToProcess;
            }
            return ConvertIntToByte(AverageImage);
        }

        private byte[] ConvertIntToByte(int[] ArrayToConvert)
        {

            byte[] result = new byte[ArrayToConvert.Length];
            
            for (int i = 0; i < ArrayToConvert.Length; i++)
            {
                result[i] = (byte) ArrayToConvert[i];
            }
            return result;
        }
    }
}
