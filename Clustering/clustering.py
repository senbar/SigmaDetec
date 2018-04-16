from sklearn.cluster import KMeans
from matplotlib import pyplot
from skimage import color

import numpy as np

def learn (X):
    figure,plots=pyplot.subplots(2,4)

    X_lab=color.rgb2lab(X)
    X_lab= X_lab[:,:,:]
    ncolumns= X.shape[0]
    nrows= X.shape[1]
    X_lab *= (1 / X_lab.max())*4

    X_coords= np.zeros((ncolumns,nrows,2))
    for i,columns in enumerate(X_lab):
        for j, row in enumerate(columns):
            X_coords[i,j]=[i/ncolumns,j/nrows]
    X_lab_cooords= np.concatenate((X_coords,X_lab),2)
    R= X_lab_cooords.reshape(nrows*ncolumns,5)

    n_clusters=8
    means=KMeans(n_clusters=n_clusters, random_state=0).fit(R)
    for i in range(n_clusters):
        desired_centroid=means.predict(R)
        desired_centroid[np.where(means.predict(R) != i)]=0

        pixel_labels = desired_centroid.reshape( ncolumns, nrows)
        plots[i%2][int(i/2)].imshow(pixel_labels)
    pyplot.ion()
    figure.show()
    pyplot.ioff()

    pyplot.figure(2)
    pixel_labels = means.predict(R).reshape(ncolumns, nrows)
    pyplot.imshow(pixel_labels)
    pyplot.show()

    pyplot.pause(2)