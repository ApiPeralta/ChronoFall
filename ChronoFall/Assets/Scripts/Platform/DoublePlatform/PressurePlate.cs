using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool IsPressed { get; private set; } = false;
    public bool IsLocked { get; private set; } = false;

    public float pressDistance = 0.2f;
    public float speed = 2f;
    public Color pressedColor = Color.green;

    private Vector3 originalPos;
    private Vector3 pressedPos;
    private int objectsInside = 0;

    private SpriteRenderer sr;
    private Color originalColor;

    void Start()
    {
        originalPos = transform.position;
        pressedPos = originalPos - new Vector3(0f, pressDistance, 0f);

        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalColor = sr.color;
        }
    }

    void Update()
    {
        Vector3 targetPos = (IsPressed || IsLocked) ? pressedPos : originalPos;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (sr != null)
        {
            sr.color = (IsPressed || IsLocked) ? pressedColor : originalColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            objectsInside++;
            IsPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            objectsInside = Mathf.Max(0, objectsInside - 1);
            IsPressed = (objectsInside > 0);
        }
    }

    public void LockPlate()
    {
        IsLocked = true;
    }
}




