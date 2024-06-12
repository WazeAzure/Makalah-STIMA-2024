using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Vector2Int startPosition;
    public Vector2Int targetPosition;
    public float speed = 5f;
    private List<Vector2Int> path;
    public int idRobot;
    private int pathIndex = 0;
    private AStarPathfinding astar;
    private BFSPathfinding pathfinder;
    private GridManager gridManager;
    private bool shouldMove = true; // Flag to control movement
    private bool iterate = true;

    void Start()
    {
        // pathfinder = GetComponent<AStarPathfinding>();
        pathfinder = GetComponent<BFSPathfinding>();
        gridManager = FindObjectOfType<GridManager>();

        if (pathfinder == null)
        {
            Debug.LogError("AStarPathfinding component not found on the robot.");
            return;
        }

        if (gridManager == null)
        {
            Debug.LogError("GridManager not found in the scene.");
            return;
        }

        // Set initial position
        transform.position = new Vector3(startPosition.x, startPosition.y, 0);
        gridManager.OccupyPosition(startPosition);

        RecalculatePath();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(path[pathIndex].x, path[pathIndex].y, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (shouldMove && path != null && pathIndex < path.Count)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // check if next is occupied or not
                if(iterate)
                {
                    if (gridManager.IsPositionOccupied(new Vector2Int(path[pathIndex].x, path[pathIndex].y)))
                    {
                        shouldMove = false;
                    }
                    else
                    {
                        shouldMove = true;
                    }
                }
                // Release the old position
                gridManager.ReleasePosition(startPosition);

                // Update start position to the current path index
                startPosition = path[pathIndex];

                // Occupy the new position
                gridManager.OccupyPosition(startPosition);

                pathIndex++;

            }
        }
        if(iterate)
        {
            // Recalculate path for the next step
            RecalculatePath();
        }
    }

    void RecalculatePath()
    {
        if (startPosition.x == targetPosition.x && startPosition.y == targetPosition.y)
        {
            shouldMove = false;
            iterate = false;
        }
        
        if(gridManager.IsPositionOccupied(targetPosition) && targetPosition != null)
        {
            Debug.Log("salah masuk");
            if (!gridManager.IsPositionOccupied(new Vector2Int(targetPosition.x - 1, targetPosition.y)))
            {
                targetPosition.x--;
            }
            else if(!gridManager.IsPositionOccupied(new Vector2Int(targetPosition.x, targetPosition.y-1)))
            {
                targetPosition.y--;
            }
        }
        else
        {
            Debug.Log("Masuk sini benar euy!");
            path = pathfinder.FindPath(startPosition, targetPosition, gridManager.grid);
            pathIndex = 1; // for BFS
            // pathIndex = 0; // for A*
            foreach(var x in path)
            {
                Debug.Log(x);
            }
            if (path == null || path.Count == 0)
            {
                shouldMove = false; // Stop movement if no valid path is found
                Debug.LogError("Path not found or blocked.");
            }
        }
    }
}
