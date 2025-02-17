using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management

public class PauseOnTrigger : MonoBehaviour
{
    [Header("Assign Player, Trigger Box, and UI Canvas")]
    public GameObject player; // Assign your player in the Inspector
    public Collider triggerBox; // Assign your trigger box in the Inspector
    public Canvas uiCanvas; // Assign your UI Canvas in the Inspector

    private bool isPaused = false; // Track if the game is paused

    private void Start()
    {
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false); // Hide UI initially
        }
    }

    private void Update()
    {
        // Check for R key press to restart the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // Toggle pause and resume when the player presses the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            isPaused = true;
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(true); // Show the UI
            }
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // Resume the game
            isPaused = false;
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(false); // Hide the UI
            }
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Unpause the game before restarting
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false); // Hide the UI
        }
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
