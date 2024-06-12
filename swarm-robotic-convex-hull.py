import numpy as np
import matplotlib.pyplot as plt
from scipy.spatial import ConvexHull

# Define the number of swarm robots
num_robots = 50

# Generate random positions for the swarm robots
np.random.seed(42)  # For reproducibility
robot_positions = np.random.rand(num_robots, 2) * 100

# Calculate the convex hull
hull = ConvexHull(robot_positions)

# Plot the swarm robots
plt.scatter(robot_positions[:, 0], robot_positions[:, 1], label='Swarm Robots')

# Plot the convex hull
for simplex in hull.simplices:
    plt.plot(robot_positions[simplex, 0], robot_positions[simplex, 1], 'r-')

# Highlight the convex hull vertices
plt.plot(robot_positions[hull.vertices, 0], robot_positions[hull.vertices, 1], 'ro', label='Convex Hull Vertices')

# Adding labels and legend
plt.xlabel('X Position')
plt.ylabel('Y Position')
plt.title('Swarm Robots with Convex Hull')
plt.legend()
plt.grid(True)
plt.show()
import numpy as np
import matplotlib.pyplot as plt
from scipy.spatial import ConvexHull

# Define the number of swarm robots
num_robots = 50

# Generate random positions for the swarm robots
np.random.seed(42)  # For reproducibility
robot_positions = np.random.rand(num_robots, 2) * 100

# Calculate the convex hull
hull = ConvexHull(robot_positions)

# Plot the swarm robots
plt.scatter(robot_positions[:, 0], robot_positions[:, 1], label='Swarm Robots')

# Plot the convex hull
for simplex in hull.simplices:
    plt.plot(robot_positions[simplex, 0], robot_positions[simplex, 1], 'r-')

# Highlight the convex hull vertices
plt.plot(robot_positions[hull.vertices, 0], robot_positions[hull.vertices, 1], 'ro', label='Convex Hull Vertices')

# Adding labels and legend
plt.xlabel('X Position')
plt.ylabel('Y Position')
plt.title('Swarm Robots with Convex Hull')
plt.legend()
plt.grid(True)
plt.show()
