using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;
    public Transform player;
    public Vector3 spawnPosition;

    public float coinChance = 0.3f;
    public float distanceBetweenObstacles = 15f;
    public float horizonDistance = 200f;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.instance == null ||
       GameStateManager.instance.currentState != GameState.Playing)
        {
            return;
        }
        //Calculate distance between player and next spawn position
        float distance = Vector3.Distance(player.position, spawnPosition);

        //Check if player is within horizon distance
        if (distance < horizonDistance)
        {
            int x = Random.Range(-3, 4);
            spawnPosition = new Vector3(x, 1.5f, spawnPosition.z + distanceBetweenObstacles);

            if (Random.value < coinChance)
            {
                spawnPosition.y = 0.6f;
                GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
