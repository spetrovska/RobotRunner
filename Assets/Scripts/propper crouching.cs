using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation angle when holding the down arrow key.")]
    public float targetRotationAngle = 45f;
    [Tooltip("Rotation speed.")]
    public float rotationSpeed = 5f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(targetRotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
