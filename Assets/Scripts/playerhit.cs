using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management.

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged "Puddle"
        if (other.CompareTag("Puddle"))
        {
            Debug.Log("Player hit a puddle!");

            // Restart the current scene (i.e., end the game).
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}