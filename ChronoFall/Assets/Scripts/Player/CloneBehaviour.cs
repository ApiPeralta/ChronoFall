using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isFrozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Al instanciar el clon, que sea trigger (sin colisiones)
        col.isTrigger = true;
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

        // Hacerlo sólido
        col.isTrigger = false;

        // Cambiar layer para que colisione con el jugador y entorno
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}

