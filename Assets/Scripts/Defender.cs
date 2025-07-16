using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Defender : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    private float damageTaken = 20f; // Damage taken from enemies

    [Header("Health Bar UI")]
    public HealthBar healthBarUI;

    [Header("Zombie Mode Settings")]
    public float zombieDuration = 3f;
    public float immunityDuration = 4f;
    private bool isZombieModeActive = false;
    private bool isImmune = false;
    public GameObject projectilePrefab;
    public SpriteRenderer spriteRenderer;
    public GameObject immunityEffect;

    [Header("Tower Target")]
    private Tower targetTower;
    private Quaternion initialRotation;

    [Header("Defender Swap Settings")]
    public bool isSelected = false;

    void Start()
    {
        currentHealth = maxHealth;
        targetTower = FindObjectOfType<Tower>();
        initialRotation = transform.rotation;
    }

    void changeDamageTaken()
    {
        switch (damageTaken)
        {
            case 20:
                damageTaken = 25f;
                break;
            case 25:
                damageTaken = 50f;
                break;
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Defender took damage: " + damage);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth);

        healthBarUI.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Zombify();
        }
    }

    void Zombify()
    {
        if (!isZombieModeActive)
        {
            StartCoroutine(ActivateZombieMode());
        }
        else
        {
            Debug.Log("Defender is already in Zombie Mode!");
        }
    }

    IEnumerator ActivateZombieMode()
    {
        isZombieModeActive = true;

        transform.rotation = initialRotation * Quaternion.Euler(0, 0, 180f); // Rotate defender to face the tower

        spriteRenderer.color = new Color(0.5f, 0f, 0.5f, 1f); // Change color to purple
        Debug.Log("Defender is now in Zombie Mode!");

        float elapsedTime = 0f;
        float shootInterval = 1.5f;

        while (elapsedTime < zombieDuration)
        {
            shootAtTower();
            yield return new WaitForSeconds(shootInterval);
            elapsedTime += shootInterval;
        }

        isZombieModeActive = false;
        transform.rotation = initialRotation; // Reset rotation
        spriteRenderer.color = Color.white; // Reset color to white

        currentHealth = maxHealth; // Reset health
        healthBarUI.SetHealth(currentHealth, maxHealth);
        
        changeDamageTaken(); // Change damage taken for next zombie mode

        Debug.Log("Defender's Zombie Mode has ended. Resetting to normal state.");
        StartCoroutine(ActivateImmunity());
    }

    void shootAtTower()
    {
        if (projectilePrefab != null && targetTower != null)
        {
            Vector3 direction = (targetTower.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 4f; // Adjust speed as needed
            Debug.Log("Defender shot a projectile at the tower!");
        }
        else
        {
            Debug.LogWarning("Projectile prefab or target tower is not set!");
        }
    }

    IEnumerator ActivateImmunity()
    {
        isImmune = true;
        if (immunityEffect != null)
        {
            immunityEffect.SetActive(true);
        }

        Debug.Log("Defender is now immune for " + immunityDuration + " seconds!");
        yield return new WaitForSeconds(immunityDuration);

        isImmune = false;
        if (immunityEffect != null)
        {
            immunityEffect.SetActive(false);
        }
        Debug.Log("Defender's immunity has ended.");
    }

    public void selectDefender()
    {
        isSelected = true;
        Debug.Log("Defender selected.");
        // Yellow highlight to indicate selection
        spriteRenderer.color = Color.yellow;
    }

    public void deselectDefender()
    {
        isSelected = false;
        Debug.Log("Defender deselected.");
        // Remove the highlight
        spriteRenderer.color = Color.white;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(float health)
    {
        currentHealth = health;
        healthBarUI.SetHealth(currentHealth, maxHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isZombieModeActive)
            {
                Debug.Log("Defender is in Zombie Mode, cannot take damage from enemy!");
                return;
            }

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ExplodeWithParticles();
            }

            if (isImmune)
            {
                Debug.Log("Defender is immune, cannot take damage from enemy!");
                return;
            }

            TakeDamage(damageTaken);
            Debug.Log("Defender hit by enemy!");
        }
    }
}
