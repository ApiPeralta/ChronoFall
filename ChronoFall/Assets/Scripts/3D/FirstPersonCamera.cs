using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float moveSpeed = 5f;

    // Head bobbing variables
    public float bobFrequency = 1.5f;
    public float bobAmplitude = 0.05f;
    private float bobTimer = 0f;
    private Vector3 originalCamPos;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalCamPos = transform.localPosition;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HeadBobbing();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = playerBody.transform.right * x + playerBody.transform.forward * z;
        playerBody.transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HeadBobbing()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            // Only bob when moving
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer * Mathf.PI * 2) * bobAmplitude;
            transform.localPosition = originalCamPos + new Vector3(0f, bobOffset, 0f);
        }
        else
        {
            // Reset when not moving
            bobTimer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalCamPos, Time.deltaTime * 5f);
        }
    }
}

