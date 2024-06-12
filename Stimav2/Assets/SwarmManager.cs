using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public GameObject robotPrefab;
    public List<Vector2Int> startPositions;
    public Vector2Int targetPosition;
    private int counter;

    void Start()
    {
        foreach (Vector2Int startPosition in startPositions)
        {
            GameObject robot = Instantiate(robotPrefab, new Vector3(startPosition.x, startPosition.y, 0), Quaternion.identity);
            RobotController controller = robot.GetComponent<RobotController>();
            controller.startPosition = startPosition;
            controller.targetPosition = targetPosition;
            controller.idRobot = counter;
            counter++;
        }
    }
}
