using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public TMP_Text timerText; // Use TextMeshPro instead of legacy Text
    private float startTime;
    private bool isRunning = true;

    private void Start()
    {
        startTime = Time.time; // Capture the start time
    }

    private void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime; // Calculate elapsed time

            // Format the time into minutes, seconds, and milliseconds
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
            
            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            }
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }
}
