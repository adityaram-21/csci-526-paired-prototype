using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Assign the Game Over UI in the inspector
    public void TriggerGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
