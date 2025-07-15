using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar UI")]
    public HealthBar healthBarUI;

    [Header("Game Over Manager")]
    public GameOverManager gameOverManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Tower took damage: " + damage);
        Debug.Log("Current Health: " + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Clamped Health: " + currentHealth);

        healthBarUI.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Destroy(gameObject); // Destroy the tower
        Debug.Log("Tower has been destroyed!");
        gameOverManager.TriggerGameOver();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10f);
            Debug.Log("Tower hit by enemy!");

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ExplodeWithParticles();
            }
        }
    }
}
