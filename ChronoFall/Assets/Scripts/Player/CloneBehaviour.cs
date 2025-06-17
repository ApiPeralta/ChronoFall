using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFrozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isFrozen && Input.GetKeyDown(KeyCode.E))
        {
            FreezeClone();
        }
    }

    void FreezeClone()
    {
        isFrozen = true;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.layer = LayerMask.NameToLayer("Ground"); // Asegurate que esta layer colisione con el jugador
    }
}

