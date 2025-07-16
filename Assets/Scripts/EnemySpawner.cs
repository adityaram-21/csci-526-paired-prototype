using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    private float timer = 0f;
    // Start is called before the first frame update
    public Vector3[] spawnPoints; // Array of spawn points for the enemies
    private float count = 0f;

    private float score = 0f;


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
            count += 1;
            score += 1;
        }
        if (count == 12 && spawnInterval > 0.75f)
        {
            count = 0;
            spawnInterval -= 0.10f;
        }

        if (score > 20)
        {
            Vector3[] newPoints = new Vector3[] {
                new Vector3(8.4f, 4.725f, 0f),
                new Vector3(8.4f, -4.725f, 0f),
                new Vector3(-8.4f, 4.725f, 0f),
                new Vector3(-8.4f, -4.725f, 0f),
                new Vector3(8.4f, 0f, 0f),
                new Vector3(-8.4f, 0f, 0f),
                new Vector3(0f, 4.725f, 0f),
                new Vector3(0, -4.725f, 0f)
            };

            // Combine both arrays
            spawnPoints = spawnPoints.Concat(newPoints).ToArray();
        }

    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }
}



// If the player continues playing for 10 sec's the enemy spawn time should be redused so that the difficulty of the game increases
// the time is 2secs it should be 1.75secs, then 1.5, 1.25, 1, 0.75, 0.5. it should not be less than 0.5 seconds, 