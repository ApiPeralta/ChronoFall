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

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask Ground;
    private bool isGrounded;
    private bool isSlowing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);

        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput * moveSpeed * speedMultiplier;
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = accelRate * speedDiff;

        rb.AddForce(Vector2.right * movement);
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

