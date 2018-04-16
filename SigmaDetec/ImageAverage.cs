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
        private int[] Biuffer;

        public ImageAverage(int numberOfImagesToProcess)
        {
            this.NumberOfImagesToProcess = numberOfImagesToProcess;            
        }
        
        public void AddImage(byte[] image)
        {
            int i = 0;
            foreach (var pixel in Biuffer)
            {
                Biuffer[i] += image[i];
                i++;
            }
            SizeOfImage = image.Length;
        }

        public byte[] GenerateAverageImage()
        {
            int[] AverageImage = new int[SizeOfImage];
            for(int i = 0; i<AverageImage.Length; i++)
            {
                AverageImage[i] = AverageImage[i] / NumberOfImagesToProcess;
            }
            return ConvertIntToByte(AverageImage);
        }
        
        private byte[] ConvertIntToByte(int[] ArrayToConvert)
        {
            byte[] result = new byte[ArrayToConvert.Length * sizeof(int)];
            Buffer.BlockCopy(ArrayToConvert, 0, result, 0, result.Length);
            return result;
        }
    }
}
