using UnityEngine;

public class PlatformPressure : MonoBehaviour
{
    public Transform platformToRaise;         // El sprite B que se mueve cuando se pisa la plataforma
    public float pressDistance = 0.2f;        // Cuánto baja la plataforma A
    public float raiseHeight = 2f;            // A qué altura sube la plataforma B
    public float speed = 2f;                  // Velocidad de movimiento

    private Vector3 originalPosA;
    private Vector3 pressedPosA;
    private Vector3 originalPosB;
    private Vector3 raisedPosB;
    private bool isPressed = false;

    void Start()
    {
        originalPosA = transform.position;
        pressedPosA = originalPosA - new Vector3(0f, pressDistance, 0f);

        if (platformToRaise != null)
        {
            originalPosB = platformToRaise.position;
            raisedPosB = originalPosB + new Vector3(0f, raiseHeight, 0f);
        }
    }

    void Update()
    {
        // Movimiento suave hacia las posiciones
        transform.position = Vector3.MoveTowards(transform.position, isPressed ? pressedPosA : originalPosA, speed * Time.deltaTime);

        if (platformToRaise != null)
        {
            platformToRaise.position = Vector3.MoveTowards(platformToRaise.position, isPressed ? raisedPosB : originalPosB, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
        }
    }
}

