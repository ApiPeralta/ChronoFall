using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    public float stretchAmount = 0.2f; // Ancho extra cuando camina
    public float squashAmount = 0.2f;  // Altura reducida cuando camina
    public float jumpStretchAmount = 0.3f; // Altura extra cuando salta
    public float jumpSquashAmount = 0.15f; // Ancho reducido cuando salta
    public float speedThreshold = 0.1f; // Velocidad mínima para detectar movimiento
    public float transitionSpeed = 5f; // Velocidad de interpolación

    private Vector3 originalScale;
    private Rigidbody2D rb;

    private bool isGrounded = true;

    public Transform groundCheck; // Un empty object debajo del personaje
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private void Start()
    {
        originalScale.x = 1;
        originalScale.y = 1;
        originalScale.z = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb == null)
            return;

        CheckGrounded();

        Vector3 targetScale = originalScale;

        if (!isGrounded) // Si está saltando o en el aire
        {
            targetScale.x = originalScale.x - jumpSquashAmount;
            targetScale.y = originalScale.y + jumpStretchAmount;
        }
        else if (Mathf.Abs(rb.velocity.x) > speedThreshold) // Si está caminando
        {
            targetScale.x = originalScale.x + stretchAmount;
            targetScale.y = originalScale.y - squashAmount;
        }

        // Interpolamos suavemente
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
    }

    private void CheckGrounded()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else
        {
            // Si no hay groundCheck asignado, asumimos que siempre está en el suelo
            isGrounded = true;
        }
    }
}
