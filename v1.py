import numpy as np
import matplotlib.pyplot as plt

def mandelbrot(c, max_iter):
    z = 0
    n = 0
    while abs(z) <= 2 and n < max_iter:
        z = z*z + c
        n += 1
    return n

def mandelbrot_set(xmin, xmax, ymin, ymax, width, height, max_iter):
    x = np.linspace(xmin, xmax, width)
    y = np.linspace(ymin, ymax, height)
    mandelbrot_matrix = np.zeros((width, height))
    for i in range(width):
        for j in range(height):
            mandelbrot_matrix[i,j] = mandelbrot(x[i] + 1j*y[j], max_iter)
    return mandelbrot_matrix

def plot_mandelbrot(xmin, xmax, ymin, ymax, width, height, max_iter):
    mandelbrot_matrix = mandelbrot_set(xmin, xmax, ymin, ymax, width, height, max_iter)
    plt.imshow(mandelbrot_matrix.T, extent=(xmin, xmax, ymin, ymax))
    plt.colorbar()
    plt.title('Mandelbrot Set')
    plt.show()

# Set the range and resolution of the plot
xmin, xmax = -2, 2
ymin, ymax = -2, 2
width, height = 1000, 1000
max_iter = 100

# Plot the Mandelbrot set
plot_mandelbrot(xmin, xmax, ymin, ymax, width, height, max_iter)
