using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner Settings")]
    public GameObject enemyPrefab;
    public float initialSpawnInterval = 2f;
    public float spawnReductionStep = 0.2f;
    public float minSpawnInterval = 0.8f;

    [Header("Enemy Spawn Points")]
    public List<Vector3> spawnPoints = new List<Vector3>();

    private float spawnInterval; // The time interval between enemy spawns
    private float timer = 0f;
    private float spawnCount = 0f;
    private bool extraSpawnPointsAdded = false;

    private int spawnAmount = 1;

    void Start()
    {
        spawnInterval = initialSpawnInterval; // Initialize the spawn interval
        
        // Initialize the spawn points
        spawnPoints.Add(new Vector3(8.4f, 4.725f, 0f));
        spawnPoints.Add(new Vector3(8.4f, -4.725f, 0f));
        spawnPoints.Add(new Vector3(-8.4f, 4.725f, 0f));
        spawnPoints.Add(new Vector3(-8.4f, -4.725f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
        if (timer >= spawnInterval) // Check if it's time to spawn a new enemy
        {
            SpawnEnemy(); // Call the method to spawn an enemy
            adjustDifficulty();
            timer = 0f; // Reset the timer

            spawnCount++;
        }
    }

    void adjustDifficulty()
    {
        if (spawnCount >= 8)
        {
            spawnCount = 0;

            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnReductionStep);
            Debug.Log("Spawn interval reduced to: " + spawnInterval);
        }

        /*if (!extraSpawnPointsAdded && spawnInterval <= 1.5f)
        {
            // Add extra spawn points to increase difficulty
            spawnPoints.AddRange(new Vector3[] {
                new Vector3(8.4f, 0f, 0f),
                new Vector3(-8.4f, 0f, 0f),
                new Vector3(0f, 4.725f, 0f),
                new Vector3(0f, -4.725f, 0f)
            });

            extraSpawnPointsAdded = true;
            Debug.Log("Additional spawn points added.");
        }*/

        if (spawnInterval <= 1.2f)
        {
            spawnAmount = 2;
            Debug.Log("Spawn amount increased to: " + spawnAmount);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        } 
    }
}