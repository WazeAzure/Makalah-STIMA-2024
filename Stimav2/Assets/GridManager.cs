using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int[,] grid;
    public int width = 10;
    public int height = 10;

    private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

    void Awake()
    {
        grid = new int[width, height];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        // Set obstacles in the grid
        grid[5, 0] = 1;
        grid[5, 1] = 1;
        //grid[0, 0] = 2;
        grid[9, 9] = 3;
        grid[1, 2] = 1;
        // grid[2, 2] = 1;
        // grid[3, 2] = 1;
        grid[4, 2] = 1;
        grid[1, 5] = 1;
        grid[3, 5] = 1;
        grid[4, 6] = 1;
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return occupiedPositions.Contains(position) || grid[position.x, position.y] == 1;
    }

    public void OccupyPosition(Vector2Int position)
    {
        occupiedPositions.Add(position);
    }

    public void ReleasePosition(Vector2Int position)
    {
        occupiedPositions.Remove(position);
    }
}
