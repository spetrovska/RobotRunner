using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.
    public Vector3 offset = new Vector3(0, 5, -10); // Base offset behind the player.
    
    [Tooltip("How much of the player's lateral movement to follow. 0 = no lateral change; 1 = full lateral change.")]
    public float lateralFollowFactor = 0.5f; // 0.5 means if the player is 50 units left, the camera will move 25 units left.
    
    [Tooltip("Speed at which the camera smoothly moves toward the target position.")]
    public float smoothSpeed = 30f; // Adjust for smoother or snappier camera movement.

    void LateUpdate()
    {
        // Calculate the desired x position:
        // Instead of following the player's x exactly, we only take a fraction of it.
        // For example, if the player is in the left lane (x = -50) then desiredX becomes -25.
        float desiredX = player.position.x * lateralFollowFactor;

        // Build the desired camera position:
        // - x: the computed desiredX
        // - y: player's y plus the y offset
        // - z: player's z plus the z offset (keeping the camera behind the player)
        Vector3 desiredPosition = new Vector3(desiredX, player.position.y + offset.y, player.position.z + offset.z);

        // Smoothly move the camera from its current position to the desired position.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}