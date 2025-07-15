using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    private Vector3 targetPosition; // Target position for the enemy to move towards

    [Header("Explosion Effect")]
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Tower") != null)
        {
            targetPosition = GameObject.FindWithTag("Tower").transform.position; // Find the tower's position
        }
        else
        {
            targetPosition = Vector3.zero; // Default position if no tower is found
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void ExplodeWithParticles()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f); // Destroy the explosion effect after 2 seconds
        }
        Destroy(gameObject); // Destroy the enemy object
    }
}
