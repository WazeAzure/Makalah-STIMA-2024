using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public class Node
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public Node parent;

        public int FCost { get { return gCost + hCost; } }

        public Node(Vector2Int pos)
        {
            position = pos;
        }
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, int[,] grid)
    {
        GridManager gridManager = FindObjectOfType<GridManager>();

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        Node startNode = new Node(start);
        Node targetNode = new Node(target);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost ||
                    openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.position == targetNode.position)
            {
                return RetracePath(startNode, currentNode);
            }

            foreach (Vector2Int direction in GetDirections())
            {
                Vector2Int neighborPos = currentNode.position + direction;
                if (!IsValidPosition(neighborPos, grid) || grid[neighborPos.x, neighborPos.y] == 1 || gridManager.IsPositionOccupied(neighborPos))
                {
                    continue;
                }

                Node neighborNode = new Node(neighborPos);
                if (closedList.Contains(neighborNode))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighborNode);
                if (newMovementCostToNeighbor < neighborNode.gCost || !openList.Contains(neighborNode))
                {
                    neighborNode.gCost = newMovementCostToNeighbor;
                    neighborNode.hCost = GetDistance(neighborNode, targetNode);
                    neighborNode.parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null; // No valid path found
    }

    List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            // Debug.Log("STUCK IN THE FIRST WHILE");
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.position.x - b.position.x);
        int dstY = Mathf.Abs(a.position.y - b.position.y);
        return dstX + dstY;
    }

    bool IsValidPosition(Vector2Int pos, int[,] grid)
    {
        return pos.x >= 0 && pos.x < grid.GetLength(0) && pos.y >= 0 && pos.y < grid.GetLength(1);
    }

    List<Vector2Int> GetDirections()
    {
        return new List<Vector2Int>
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };
    }
}
