using System.Collections;
using UnityEngine; // Assuming you have a HealthBar script in a namespace

public class Defender : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar UI")]
    public DefenderHealthBar healthBarUI;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Defender took damage: " + damage);
        Debug.Log("Current Health: " + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Clamped Health: " + currentHealth);

        healthBarUI.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10f);
            Debug.Log("Defender hit by enemy!");
            
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ExplodeWithParticles();
            }
        }
    }
}
