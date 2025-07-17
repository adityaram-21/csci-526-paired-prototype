using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Assign the Game Over UI in the inspector
    public TMP_Text titleText;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        titleText.text = "Game Paused";
    }

    public void ResumeGame()
    {
        isPaused = false;
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        titleText.text = "Game Over";
    }

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
