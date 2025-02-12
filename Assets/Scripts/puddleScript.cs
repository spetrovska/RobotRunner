using UnityEngine;

public class PuddleSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject puddlePrefab;  // Assign your puddle prefab here.
    public Transform player;         // Assign your player's transform here.

    [Header("Spawn Settings")]
    public float spawnDistance = 10f;    // Distance in front of the player.
    public float spawnInterval = 0.1f;     // Time (in seconds) between spawns.

    private float timer;

    private void Update()
    {
        // Increment the timer by the time elapsed since the last frame.
        timer += Time.deltaTime;

        // If the timer exceeds the interval, spawn a puddle.
        if (timer >= spawnInterval)
        {
            SpawnPuddle();
            timer = 0f;
        }
    }

    void SpawnPuddle()
    {
        // Calculate a spawn position in front of the player.
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        // Optionally adjust the Y position if you want the puddle at ground level.
        spawnPos.y = 0f; 

        // Instantiate the puddle prefab at the calculated position.
        Instantiate(puddlePrefab, spawnPos, Quaternion.identity);
    }
}