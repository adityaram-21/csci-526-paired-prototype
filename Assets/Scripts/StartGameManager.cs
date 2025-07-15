using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public GameObject startCanvas; // Assign in Inspector
    public GameObject gameWorld;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game initially
    }

    public void StartGame()
    {
        startCanvas.SetActive(false);
        gameWorld.SetActive(true);
        Time.timeScale = 1f; // Resume game
    }
}