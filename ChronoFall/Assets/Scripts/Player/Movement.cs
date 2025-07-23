using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float acceleration = 80f;
    public float deceleration = 60f;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input horizontal
        moveInput = 0f;
        bool blockInput = wallJumping && !isGrounded;

        if (!blockInput)
        {
            if (Input.GetKey(KeyCode.A)) moveInput = -1f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        }

        // Chequeos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);
        isTouchingWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, wallLayer);
        isTouchingWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, wallLayer);

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.W))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // Salto
        if (jumpBufferCounter > 0 && (coyoteCounter > 0 || isWallSliding))
        {
            if (isWallSliding && !isGrounded)
            {
                Vector2 dir = isTouchingWallLeft ? Vector2.right : Vector2.left;
                rb.velocity = new Vector2(dir.x * wallJumpHorizontalForce, wallJumpForce);
                wallJumping = true;
                wallJumpLockCounter = wallJumpLockTime;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            jumpBufferCounter = 0;
            isJumping = true;
        }

        // Cortar salto si se suelta la tecla
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
            // Si tocás el suelo, se termina el wall jump
            if (isGrounded)
            {
                wallJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput * moveSpeed * speedMultiplier;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = accelRate * speedDiff * Time.fixedDeltaTime;
        rb.AddForce(Vector2.right * movement, ForceMode2D.Force);

        // Desbloquear input después del wall jump
        if (wallJumping)
        {
            wallJumpLockCounter -= Time.fixedDeltaTime;
            if (wallJumpLockCounter <= 0)
                wallJumping = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void SetIsSlowing(bool slowing)
    {
        isSlowing = slowing;
    }

    public bool GetIsSlowing()
    {
        return isSlowing;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckRadius);
        Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckRadius);
    }
}
