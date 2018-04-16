using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigamDetec
{
    public class BitmapColorSegmentation
    {
        const int MINIMUM_RED_TRESHHOLD = 110;
        const int MAXIMUM_NOT_RED_TRESHHOLD = 90;

        public static byte[] ExtractRedBitmap(byte[] bitmap)
        {

            /// this algorith is placeholder; it is intended to be replaced with more general version/clustering
            for (int i = 0; i < bitmap.Length; i++)
            {
                //red pixels are of index i%2 ==2
                if (i % 4 == 2)
                {
                    int redIntensity = bitmap[i];
                    int blueIntensity = bitmap[i - 2];
                    int greenIntensity = bitmap[i - 1];

                    var notRedIntensity = blueIntensity + greenIntensity;
                    if (notRedIntensity > MAXIMUM_NOT_RED_TRESHHOLD)
                    {
                        bitmap[i - 1] = 0;
                        bitmap[i - 2] = 0;
                        bitmap[i] = 0;
                    }
                    else
                    {
                        if (redIntensity < MINIMUM_RED_TRESHHOLD)
                        {
                            bitmap[i - 1] = 0;
                            bitmap[i - 2] = 0;
                            bitmap[i] = 0;
                            continue;
                        }
                        bitmap[i] = 255;
                    }
                    
                }

            }
            return bitmap;
        }

    }
}
