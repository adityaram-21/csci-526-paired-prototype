using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [Header("Tower Settings")]
    public Tower tower;

    [Header("Score based Health Settings")]
    private int score = 0;
    private int scoreSinceLastHeal = 0;

    [Header("Time based Health Settings")]
    private float healInterval = 60f; // Time in seconds between heals
    private float healTimer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        // Handle time-based healing
        healTimer += Time.deltaTime;
        if (healTimer >= healInterval)
        {
            HealTower();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreSinceLastHeal += amount;

        // Check if enough score has been accumulated for a heal
        if (scoreSinceLastHeal >= 25) // Example threshold
        {
            HealTower();
        }
    }

    void HealTower()
    {
        if (tower != null)
        {
            tower.healTower(20f); // Heal the tower by 20 points
        }
        scoreSinceLastHeal = 0; // Reset score since last heal
        healTimer = 0f; // Reset the heal timer
    }
}
