using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 6f;
    public float speedMultiplier = 1f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    private Rigidbody2D rb;
    private float moveInput = 0f;
    private float velocityX = 0f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask Ground;
    private bool isGrounded;
    private bool isSlowing = false;

    // Wall jump variables
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;
    public float wallSlideSpeed = 1f;
    public float wallJumpForce = 10f;
    public float wallJumpHorizontalForce = 7f;

    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    private bool isWallSliding;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground and wall checks
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);
        isTouchingLeftWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, wallLayer);
        isTouchingRightWall = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, wallLayer);

        // Input
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        // Wall sliding check
        isWallSliding = (moveInput != 0f && !isGrounded && (isTouchingLeftWall || isTouchingRightWall));

        // Jumping
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isWallSliding)
            {
                Vector2 jumpDir = isTouchingLeftWall ? Vector2.right : Vector2.left;
                rb.velocity = new Vector2(jumpDir.x * wallJumpHorizontalForce, wallJumpForce);
            }
            else if (Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput * moveSpeed * speedMultiplier;
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = accelRate * speedDiff;

        rb.AddForce(Vector2.right * movement);

        // Wall slide effect
        if (isWallSliding && rb.velocity.y < -wallSlideSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    public void SetIsSlowing(bool slowing)
    {
        isSlowing = slowing;
    }

    public bool GetIsSlowing()
    {
        return isSlowing;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
