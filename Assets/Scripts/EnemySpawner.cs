using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    private float timer = 0f;
    // Start is called before the first frame update
    public Vector3[] spawnPoints; // Array of spawn points for the enemies

    void Start()
    {
        spawnPoints = new Vector3[] {
            new Vector3(8.4f, 4.725f, 0f),
            new Vector3(8.4f, -4.725f, 0f),
            new Vector3(-8.4f, 4.725f, 0f),
            new Vector3(-8.4f, -4.725f, 0f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
        if (timer >= spawnInterval) // Check if it's time to spawn a new enemy
        {
            SpawnEnemy(); // Call the method to spawn an enemy
            timer = 0f; // Reset the timer
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }
}
