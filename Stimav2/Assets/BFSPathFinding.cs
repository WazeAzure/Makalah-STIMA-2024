using System.Collections.Generic;
using UnityEngine;

public class BFSPathfinding : MonoBehaviour
{
    public class Node
    {
        public Vector2Int position;
        public Node parent;

        public Node(Vector2Int pos)
        {
            position = pos;
        }
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, int[,] grid)
    {
        GridManager gridManager = FindObjectOfType<GridManager>();

        Queue<Node> queue = new Queue<Node>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Node startNode = new Node(start);
        queue.Enqueue(startNode);
        visited.Add(start);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();

            if (currentNode.position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            foreach (Vector2Int direction in GetDirections())
            {
                Vector2Int neighborPos = currentNode.position + direction;
                if (!IsValidPosition(neighborPos, grid) || grid[neighborPos.x, neighborPos.y] == 1 || gridManager.IsPositionOccupied(neighborPos) || gridManager.IsPositionOccupied(neighborPos))
                {
                    continue;
                }

                if (!visited.Contains(neighborPos))
                {
                    visited.Add(neighborPos);
                    Node neighborNode = new Node(neighborPos) { parent = currentNode };
                    queue.Enqueue(neighborNode);
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
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Add(startNode.position); // Add the start node at the end
        path.Reverse();
        return path;
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
