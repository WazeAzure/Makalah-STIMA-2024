using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public int[,] grid;
    public int width;
    public int height;
    public GameObject cellPrefab;
    public Sprite freeCellSprite;
    public Sprite obstacleSprite;
    public Sprite startPositionSprite;
    public Sprite targetPositionSprite;

    public float cellSize = 1f; // Fixed cell size

    private void Start()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager not found in the scene.");
            return;
        }

        grid = gridManager.grid;
        width = gridManager.width;
        height = gridManager.height;

        RenderGrid();
    }

    void RenderGrid()
    {
        for (int x = -1; x <= width; x++)
        {
            for (int y = -1; y <= height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);

                SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();

                if(!(x == -1 || x == width || y == -1 || y == height))
                {

                    if (grid[x, y] == 0)
                    {
                        renderer.sprite = freeCellSprite;
                    }
                    else if (grid[x, y] == 1)
                    {
                        renderer.sprite = obstacleSprite;
                    }
                    else if (grid[x, y] == 2)
                    {
                        renderer.sprite = startPositionSprite;
                    }
                    else if (grid[x, y] == 3)
                    {
                        renderer.sprite = targetPositionSprite;
                    }
                }
                else
                {
                    renderer.sprite = obstacleSprite;
                }
                // Ensure the sprite fits the cell size
                int size = 5;
                cell.transform.localScale = new Vector3(cellSize/size, cellSize/size, 1/size);
            }
        }
    }
}
