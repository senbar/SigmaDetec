from bitmap import Bitmap
from clustering import learn
if __name__ == '__main__':
    Image = Bitmap();
    try:
        Image.loadBitmap("Data/KinectSnapshot-09-39-49(square).bmp")
    except FileNotFoundError:
        print("could not load file")
        raise

    Image.Show();
    learn(Image.image)



    input("waiting for keystroke")