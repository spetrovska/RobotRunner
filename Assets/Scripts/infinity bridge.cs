using UnityEngine;

public class BridgeCloneTrigger : MonoBehaviour
{
    public GameObject bridgePrefab; // Reference to the bridge prefab
    public GameObject player; // Reference to the player object (assignable from the Inspector)
    public float spawnZIncrement = 500f; // The fixed increment for Z-axis position
    private float nextBridgeZPosition; // Z position for the next bridge

    void Start()
    {
        // Initialize the spawn position, starting at the first increment (e.g., 500)
        nextBridgeZPosition = spawnZIncrement;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player triggers the event
        if (other.gameObject == player) // Compare the object triggering the collider with the player
        {
            // Instantiate a new bridge at the next position
            Instantiate(bridgePrefab, new Vector3(0, 0, nextBridgeZPosition), Quaternion.identity);

            // Update the position for the next bridge, incrementing Z by the specified fixed amount
            nextBridgeZPosition += spawnZIncrement; // Move the spawn point ahead by the fixed increment
        }
    }
}
