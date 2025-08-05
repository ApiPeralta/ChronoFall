using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.1f;
    public float jumpCutMultiplier = 0.5f;

    [Header("Paredes")]
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 12f;
    public float wallJumpHorizontalForce = 8f;
    public float wallJumpLockTime = 0.2f;

    [Header("Chequeos")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask Ground;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isFacingRight = true;

    private bool isGrounded;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool isWallSliding;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool isJumping;
    private bool wallJumping;
    private float wallJumpLockCounter;

    private bool isSlowing = false;
    private float speedMultiplier = 1f;

    private int lastWallJumpDir = 0; // -1 izquierda, 1 derecha, 0 = ninguno

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento con flechas o A/D
        moveInput = Input.GetAxisRaw("Horizontal");

        // Chequeos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);
        isTouchingWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, wallLayer);
        isTouchingWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, wallLayer);

        // Coyote time
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            lastWallJumpDir = 0; // resetea wall jump
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.W)) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        // Salto y wall jump
        if (jumpBufferCounter > 0)
        {
            if (isGrounded || coyoteCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                jumpBufferCounter = 0;
            }
            else if (!isGrounded && (isTouchingWallLeft || isTouchingWallRight))
            {
                int currentWallDir = isTouchingWallLeft ? -1 : 1;

                if (currentWallDir != lastWallJumpDir)
                {
                    Vector2 dir = currentWallDir == -1 ? Vector2.right : Vector2.left;
                    rb.velocity = new Vector2(dir.x * wallJumpHorizontalForce, wallJumpForce);

                    wallJumping = true;
                    wallJumpLockCounter = wallJumpLockTime;
                    jumpBufferCounter = 0;
                    isJumping = true;

                    lastWallJumpDir = currentWallDir;
                }
            }
        }

        // Cortar salto
        if (Input.GetKeyUp(KeyCode.W) && isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            isJumping = false;
        }

        // Wall slide
        isWallSliding = !isGrounded && ((isTouchingWallLeft && moveInput < 0) || (isTouchingWallRight && moveInput > 0)) && rb.velocity.y < 0;
        if (isWallSliding)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));

        // Flip
        if (moveInput != 0 && !wallJumping)
        {
            if (moveInput > 0 && !isFacingRight) Flip();
            if (moveInput < 0 && isFacingRight) Flip();
        }
    }

    void FixedUpdate()
    {
        if (wallJumpLockCounter > 0f)
        {
            wallJumpLockCounter -= Time.fixedDeltaTime;
        }

        if (wallJumpLockCounter <= 0f)
        {
            // Movimiento normal con flechas (solo si no está bloqueado)
            rb.velocity = new Vector2(moveInput * moveSpeed * speedMultiplier, rb.velocity.y);
        }

        // Si sigue bloqueado, no se mueve con flechas. Solo mantiene la velocidad del wall jump.
    }


    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void SetSpeedMultiplier(float multiplier) => speedMultiplier = multiplier;
    public void SetIsSlowing(bool slowing) => isSlowing = slowing;
    public bool GetIsSlowing() => isSlowing;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckRadius);
        Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckRadius);
    }
}
