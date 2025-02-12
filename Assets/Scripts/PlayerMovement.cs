using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f; // constant forward speed
    public float speedIncreaseRate = 0.1f; // (if you plan to increase speed over time)

    [Header("Jump (Manual Arc)")]
    public float jumpHeight = 3f;    // how high the jump goes
    public float jumpDuration = 0.8f; // total time for going up + coming down

    [Header("Lane Settings")]
    public float laneDistance = 1f;
    public float laneSwitchSpeed = 100f;

    // Animator is optional if you only want run/crouch animations
    private Animator animator;
    
    private bool isJumping = false;
    private bool isCrouching = false;
    private int targetLane = 0; // -1, 0, 1 for left/center/right

    void Start()
    {
        animator = GetComponent<Animator>(); 
        // If you have no animator, remove all animator references.
    }

    void Update()
    {
        // ------------------- Jump -------------------
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && !isCrouching)
        {
            StartCoroutine(DoJump());
        }

        // ------------------- Crouch -------------------
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJumping)
        {
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouch(false);
        }

        // ------------------- Lane Switch -------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            targetLane = Mathf.Max(targetLane - 1, -1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            targetLane = Mathf.Min(targetLane + 1, 1);
        }

        // ------------------- Forward + Lane Movement -------------------
        float newZ = transform.position.z + forwardSpeed * Time.deltaTime; 
        float targetX = targetLane * laneDistance * 2.75f;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneSwitchSpeed * Time.deltaTime);
        
        // Keep Y wherever it currently is (unless the jump coroutine is adjusting it).
        float currentY = transform.position.y;

        // Apply the new position
        transform.position = new Vector3(newX, currentY, newZ);
    }

    // Coroutine: moves Y up to jumpHeight, then back down over jumpDuration
    private IEnumerator DoJump()
    {
        isJumping = true;

        // Optionally trigger a jump animation if you have one:
        // animator?.SetTrigger("Jump");

        // Original Y
        float startY = transform.position.y;
        float peakY = startY + jumpHeight;

        // We'll spend half the duration going up, half going down
        float halfTime = jumpDuration * 0.5f;

        // ---------------- Up Phase ----------------
        float t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / halfTime);

            float newY = Mathf.Lerp(startY, peakY, normalized);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }

        // ---------------- Down Phase ----------------
        t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / halfTime);

            float newY = Mathf.Lerp(peakY, startY, normalized);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }

        isJumping = false;
    }

    // Crouch toggles a bool. If you have a crouch animation, set an Animator bool here
    private void Crouch(bool state)
    {
        isCrouching = state;
        if (animator)
        {
            animator.SetBool("Crouch", state);
        }
    }
}
