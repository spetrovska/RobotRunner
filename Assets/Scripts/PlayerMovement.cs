using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f; // constant forward speed
    public float speedIncreaseRate = 0.1f; // (if you plan to increase speed over time)
    public float speedIncrement = 0.5f; // how much speed increases every second

    [Header("Jump (Manual Arc)")]
    public float jumpHeight = 3f;    // how high the jump goes
    public float jumpDuration = 0.8f; // total time for going up + coming down

    [Header("Lane Settings")]
    public float laneDistance = 1f;
    public float laneSwitchSpeed = 100f;

    private Animator animator;
    private bool isJumping = false;
    private bool isCrouching = false;
    private int targetLane = 0; // -1, 0, 1 for left/center/right

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && !isCrouching)
        {
            StartCoroutine(DoJump());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJumping)
        {
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouch(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            targetLane = Mathf.Max(targetLane - 1, -1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            targetLane = Mathf.Min(targetLane + 1, 1);
        }

        float newZ = transform.position.z + forwardSpeed * Time.deltaTime; 
        float targetX = targetLane * laneDistance * 2.75f;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneSwitchSpeed * Time.deltaTime);
        float currentY = transform.position.y;

        transform.position = new Vector3(newX, currentY, newZ);
    }

    private IEnumerator DoJump()
    {
        isJumping = true;
        float startY = transform.position.y;
        float peakY = startY + jumpHeight;
        float halfTime = jumpDuration * 0.5f;

        float t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float newY = Mathf.Lerp(startY, peakY, t / halfTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float newY = Mathf.Lerp(peakY, startY, t / halfTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        isJumping = false;
    }

    private void Crouch(bool state)
    {
        isCrouching = state;
        if (animator)
        {
            animator.SetBool("Crouch", state);
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            forwardSpeed += speedIncrement;
        }
    }
}
