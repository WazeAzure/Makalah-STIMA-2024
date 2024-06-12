import numpy as np
import heapq
import time
import os
from collections import deque

# Define the grid size
grid_size = (10, 10)

# Initialize grid with zeros (0: free space, 1: obstacle)
grid = np.zeros(grid_size)
obstacles = [(1, 2), (2, 2), (3, 2), (4, 2), (8, 8), (4, 4), (6, 6), (0, 8), (8, 0)]
for obs in obstacles:
    grid[obs] = 1

# Start and target positions
start_positions = [(0, 0), (5, 5), (6, 6), (7, 0), (9, 8), (8, 9)]
target_position = (9, 9)

# Directions for movement (up, down, left, right)
directions = [(0, 1), (1, 0), (0, -1), (-1, 0)]

def heuristic(a, b):
    return abs(a[0] - b[0]) + abs(a[1] - b[1])

def astar(start, goal, grid):
    open_list = []
    heapq.heappush(open_list, (0 + heuristic(start, goal), 0, start))
    came_from = {}
    g_score = {start: 0}
    f_score = {start: heuristic(start, goal)}
    
    while open_list:
        _, current_g, current = heapq.heappop(open_list)
        
        if current == goal:
            path = []
            while current in came_from:
                path.append(current)
                current = came_from[current]
            path.reverse()
            return path
        
        for direction in directions:
            neighbor = (current[0] + direction[0], current[1] + direction[1])
            tentative_g_score = current_g + 1
            if 0 <= neighbor[0] < grid.shape[0] and 0 <= neighbor[1] < grid.shape[1]:
                if grid[neighbor] == 1:
                    continue
                if neighbor not in g_score or tentative_g_score < g_score[neighbor]:
                    came_from[neighbor] = current
                    g_score[neighbor] = tentative_g_score
                    f_score[neighbor] = tentative_g_score + heuristic(neighbor, goal)
                    heapq.heappush(open_list, (f_score[neighbor], tentative_g_score, neighbor))
    
    return []

# Example usage
path = astar(start_positions[0], target_position, grid)
print("Path using A*:", path)

def bfs(start, goal, grid):
    queue = deque([start])
    came_from = {start: None}
    
    while queue:
        current = queue.popleft()
        
        if current == goal:
            path = []
            while current is not None:
                path.append(current)
                current = came_from[current]
            path.reverse()
            return path
        
        for direction in directions:
            neighbor = (current[0] + direction[0], current[1] + direction[1])
            if 0 <= neighbor[0] < grid.shape[0] and 0 <= neighbor[1] < grid.shape[1]:
                if grid[neighbor] == 1 or neighbor in came_from:
                    continue
                queue.append(neighbor)
                came_from[neighbor] = current
    
    return []

# Example usage
path = bfs(start_positions[1], target_position, grid)
print("Path using BFS:", path)

def simulate_swarm(start_positions, target_position, grid, algorithm):
    paths = []
    for start in start_positions:
        if algorithm == 'A*':
            path = astar(start, target_position, grid)
        elif algorithm == 'BFS':
            path = bfs(start, target_position, grid)
        paths.append(path)
    
    max_path_length = max(len(path) for path in paths)
    for step in range(max_path_length):
        print(f"Step {step}:")
        for i, path in enumerate(paths):
            if step < len(path):
                position = path[step]
                print(f"Robot {i} at {position}")
                grid[position] = 2  # Mark the robot's position
        print(grid)
        grid[grid == 2] = 0  # Clear the robots' positions for the next step
        time.sleep(1)
        os.system('cls')
    

# Run the simulation
simulate_swarm(start_positions, target_position, grid, 'A*')
simulate_swarm(start_positions, target_position, grid, 'BFS')
