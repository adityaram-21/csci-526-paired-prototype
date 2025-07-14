using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClickHandler : MonoBehaviour
{
    public float splashRadius = 0.5f;
    public LayerMask enemyLayer;
    public TMP_Text scoreText;
    private int score = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 for 2D

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(mousePosition, splashRadius, enemyLayer);

            int hitCount = 0;

            foreach (Collider2D hit in hitEnemies)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ExplodeWithParticles(); // Call the explosion method on the enemy
                }
                hitCount++;
            }

            updateScore(hitCount);
        }
    }

    void updateScore(int amount)
    {
        score += amount; // Update the score by the specified amount
        scoreText.text = "Score: " + score; // Update the score display
    }
}