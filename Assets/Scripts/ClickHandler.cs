using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClickHandler : MonoBehaviour
{
    public float splashRadius = 0.5f;
    public LayerMask enemyLayer;
    public TMP_Text scoreText;
    public TMP_Text swapCooldownText;
    private int score = 0;

    private Defender firstDefender;
    private Defender secondDefender;

    private bool canSwap = false;
    private float swapCooldown = 30f;
    private float swapTimer = 0f;

    void Start()
    {
        scoreText.text = "Score: " + score;
        swapCooldownText.text = "Swap Cooldown: " + Mathf.CeilToInt(swapCooldown) + "s";
    }
    void Update()
    {
        handleCooldown();

        if (Input.GetMouseButtonDown(0)) // Checking for mouse click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 for 2D

            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (hit != null && hit.CompareTag("Defender"))
            {
                if (!canSwap)
                {
                    Debug.Log("Swap is on cooldown!");
                    return; // Prevent swapping
                }

                Defender defender = hit.GetComponent<Defender>();

                if (firstDefender == null)
                {
                    firstDefender = defender;
                    defender.selectDefender();
                }
                else if (secondDefender == null && defender != firstDefender)
                {
                    secondDefender = defender;
                    defender.selectDefender();

                    swapDefenderHealth();
                }
                return; // Defender click handled, don't check enemies
            }

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(mousePosition, splashRadius, enemyLayer);

            int hitCount = 0;

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ExplodeWithParticles(); // Call the explosion method on the enemy
                }
                hitCount++;
            }

            updateScore(hitCount);
        }
    }

    void handleCooldown()
    {
        if (!canSwap)
        {
            swapTimer += Time.deltaTime;
            if (swapTimer >= swapCooldown)
            {
                canSwap = true;
                swapTimer = 0f;
                swapCooldownText.text = "Swap Ready!";
            }
            else
            {
                swapCooldownText.text = "Swap Cooldown: " + Mathf.CeilToInt(swapCooldown - swapTimer) + "s";
            }
        }
    }

    void updateScore(int amount)
    {
        score += amount; // Update the score by the specified amount
        scoreText.text = "Score: " + score;

        if (HealthManager.Instance != null)
        {
            HealthManager.Instance.AddScore(amount); // Add score to the HealthManager for auto healing
        }
    }

    void swapDefenderHealth()
    {
        if (firstDefender != null && secondDefender != null)
        {
            float tempHealth = firstDefender.getCurrentHealth();
            firstDefender.setCurrentHealth(secondDefender.getCurrentHealth());
            secondDefender.setCurrentHealth(tempHealth);

            Debug.Log("Swapped health between defenders.");

            firstDefender.deselectDefender();
            secondDefender.deselectDefender();

            firstDefender = null; // Reset the first defender
            secondDefender = null; // Reset the second defender

            canSwap = false;
            swapTimer = 0f;

            swapCooldownText.text = "Swap Cooldown: " + Mathf.CeilToInt(swapCooldown) + "s";
        }
    }
}