using UnityEngine;

public class PuddleSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject puddleObject;  // Assign your puddle GameObject here.
    public Transform player;         // Assign your player's transform here.

    [Header("Spawn Settings")]
    [Tooltip("Distance ahead of the player to spawn puddles.")]
    public float spawnAheadDistance = 200f;
    [Tooltip("Spacing along the Z-axis between each puddle row.")]
    public float rowSpacing = 12f;

    [Header("Lane Settings")]
    [Tooltip("Base lane distance (as used in your PlayerMovement script).")]
    public float laneDistance = 1f;
    [Tooltip("Multiplier to space lanes apart (e.g., 3.5).")]
    public float laneMultiplier = 4.5f;
    [Tooltip("Extra offset applied to left/right lanes to push them further out.")]
    public float laneEdgeOffset = 2f;

    // Tracks the next Z position where a new puddle row will spawn.
    private float nextSpawnZ;

    private void Start()
    {
        // Initialize spawn position a row ahead of the player's current Z.
        nextSpawnZ = player.position.z + rowSpacing;
    }

    private void Update()
    {
        // Continuously spawn rows ahead of the player until spawnAheadDistance is reached.
        while (nextSpawnZ < player.position.z + spawnAheadDistance)
        {
            SpawnRow(nextSpawnZ);
            nextSpawnZ += rowSpacing;
        }
    }

    /// <summary>
    /// Spawns a single puddle in a randomly chosen lane (left, center, or right) at the given Z position.
    /// If the chosen lane is left or right, an extra offset is applied.
    /// </summary>
    /// <param name="zPos">The Z coordinate for this row.</param>
    void SpawnRow(float zPos)
    {
        // Choose one lane: -1 (left), 0 (center), or 1 (right)
        int lane = Random.Range(-1, 2); // Random.Range with ints is min inclusive, max exclusive

        // Calculate the base X position using your lane math.
        float xPos = lane * laneDistance * laneMultiplier;

        // If the lane is left (-1) or right (1), push it further out.
        if (lane == -1)
        {
            xPos -= laneEdgeOffset;
        }
        else if (lane == 1)
        {
            xPos += laneEdgeOffset;
        }

        Vector3 spawnPos = new Vector3(xPos, 0f, zPos);

        // Instantiate the puddle by duplicating the existing GameObject.
        GameObject newPuddle = Instantiate(puddleObject, spawnPos, Quaternion.identity);
        newPuddle.SetActive(true);
    }
}
