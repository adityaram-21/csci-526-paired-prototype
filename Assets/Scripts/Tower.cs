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

    public void healTower(float health)
    {
        if (health >= 100f)
        {
            Debug.LogWarning("Already at max health, cannot heal further.");
            return; // Prevent healing above max health
        }

        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBarUI.SetHealth(currentHealth, maxHealth);
        Debug.Log("Tower healed by " + health + " points. Current Health: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Tower took damage: " + damage);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth);

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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ExplodeWithParticles();
            }
            TakeDamage(10f);
            Debug.Log("Tower hit by enemy!");  
        }
    }
}
