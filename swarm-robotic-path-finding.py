import numpy as np

class Grid:
    def __init__(self, width, height, obstacles=None):
        self.width = width
        self.height = height
        self.grid = np.zeros((width, height), dtype=int)
        if obstacles:
            for (x, y) in obstacles:
                self.grid[x][y] = 1  # Mark obstacle positions

    def is_within_bounds(self, position):
        (x, y) = position
        return 0 <= x < self.width and 0 <= y < self.height

    def is_obstacle(self, position):
        (x, y) = position
        return self.grid[x][y] == 1

    def neighbors(self, position):
        (x, y) = position
        potential_neighbors = [(x+1, y), (x-1, y), (x, y+1), (x, y-1)]
        valid_neighbors = [pos for pos in potential_neighbors if self.is_within_bounds(pos) and not self.is_obstacle(pos)]
        return valid_neighbors


from collections import deque
import heapq

def bfs(grid, start, goal):
    queue = deque([start])
    came_from = {start: None}

    while queue:
        current = queue.popleft()

        if current == goal:
            break

        for neighbor in grid.neighbors(current):
            if neighbor not in came_from:
                queue.append(neighbor)
                came_from[neighbor] = current

    return reconstruct_path(came_from, start, goal)

def a_star(grid, start, goal):
    def heuristic(a, b):
        (x1, y1) = a
        (x2, y2) = b
        return abs(x1 - x2) + abs(y1 - y2)

    open_list = []
    heapq.heappush(open_list, (0, start))
    came_from = {start: None}
    g_score = {start: 0}

    while open_list:
        _, current = heapq.heappop(open_list)

        if current == goal:
            break

        for neighbor in grid.neighbors(current):
            tentative_g_score = g_score[current] + 1
            if neighbor not in g_score or tentative_g_score < g_score[neighbor]:
                g_score[neighbor] = tentative_g_score
                f_score = tentative_g_score + heuristic(neighbor, goal)
                heapq.heappush(open_list, (f_score, neighbor))
                came_from[neighbor] = current

    return reconstruct_path(came_from, start, goal)

def reconstruct_path(came_from, start, goal):
    current = goal
    path = []
    while current != start:
        path.append(current)
        current = came_from.get(current)
        if current is None:
            return None
    path.append(start)
    path.reverse()
    return path


import matplotlib.pyplot as plt

def plot_path(grid, path, start, goal, title):
    grid_data = grid.grid.copy()
    for (x, y) in path:
        grid_data[x][y] = 0.5

    grid_data[start] = 0.7
    grid_data[goal] = 0.9

    plt.imshow(grid_data.T, cmap='gray_r', origin='lower')
    plt.title(title)
    plt.show()


# Define the grid and obstacles
width, height = 10, 10
obstacles = [(1, 1), (1, 2), (1, 3), (3, 1), (3, 2), (3, 3)]
grid = Grid(width, height, obstacles)

# Define start and goal positions
start = (0, 0)
goal = (7, 7)

# Find paths using BFS and A*
bfs_path = bfs(grid, start, goal)
a_star_path = a_star(grid, start, goal)

# Visualize the paths
plot_path(grid, bfs_path, start, goal, title="BFS Path")
plot_path(grid, a_star_path, start, goal, title="A* Path")

