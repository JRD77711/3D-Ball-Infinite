using UnityEngine;

public class GameResetManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerMovement player;

    [Header("Ground")]
    public Transform ground1;
    public Transform ground2;

    [Header("Obstacle Generator")]
    public ObstacleGenerator obstacleGenerator;

    private Vector3 ground1StartPosition;
    private Vector3 ground2StartPosition;
    private Vector3 generatorStartSpawnPosition;

    private void Awake()
    {
        if (ground1 != null)
        {
            ground1StartPosition = ground1.position;
        }

        if (ground2 != null)
        {
            ground2StartPosition = ground2.position;
        }

        if (obstacleGenerator != null)
        {
            generatorStartSpawnPosition = obstacleGenerator.spawnPosition;
        }
    }

    public void ResetGameObjects()
    {
        if (player != null)
        {
            player.ResetPlayer();
        }

        if (ground1 != null)
        {
            ground1.position = ground1StartPosition;
        }

        if (ground2 != null)
        {
            ground2.position = ground2StartPosition;
        }

        if (obstacleGenerator != null)
        {
            obstacleGenerator.spawnPosition = generatorStartSpawnPosition;
        }

        ClearSpawnedObjects();
    }

    private void ClearSpawnedObjects()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);

        foreach (Coin coin in coins)
        {
            Destroy(coin.gameObject);
        }
    }
}