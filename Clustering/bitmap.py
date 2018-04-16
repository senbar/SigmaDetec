
from PIL import Image, ImageDraw;
from matplotlib import pyplot

import matplotlib.image as mpimg
import pandas as pd

class Bitmap:
    def __init__(self):
        self.image=None
        self.dataFrame=None

    def loadBitmap(self, filename):
        self.image = mpimg.imread(filename)

    def Show(self):
        pyplot.ioff()
        pyplot.imshow(self.image)
        pyplot.show()
