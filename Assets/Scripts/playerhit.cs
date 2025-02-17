using UnityEngine;
using UnityEngine.SceneManagement; // Needed to reload the scene

public class PlayerCollision : MonoBehaviour
{
    // This method is called when the player's collider enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is tagged "Puddle"
        if (other.CompareTag("Puddle"))
        {
            Debug.Log("Game Over: Hit a puddle!");
            print("enter");

            // Reload the current scene to simulate game over
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}