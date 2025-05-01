using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float speedMultiplier = 1f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    private Rigidbody2D rb;
    private float moveInput = 0f;
    private float velocityX = 0f;

    public float dashForce = 20f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isSlowing = false;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask Ground;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);

        if (isGrounded)
        {
            canDash = true;
        }
        // Capturar input con respuesta directa
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isSlowing && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }
    public void SetIsSlowing(bool slowing)
    {
        isSlowing = slowing;
    }
    void FixedUpdate()
    {
        float targetSpeed = moveInput * moveSpeed * speedMultiplier;
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = accelRate * speedDiff;

        rb.AddForce(Vector2.right * movement);
    }
    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
    IEnumerator PerformDash()
    {
        canDash = false;

        float direction = Input.GetAxisRaw("Horizontal");
        if (direction == 0f)
            direction = transform.localScale.x; // Por defecto, hacia donde mira

        rb.velocity = new Vector2(direction * dashForce, rb.velocity.y);

        // Esperás solo 0.1s para evitar spameo en el aire (opcional)
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
