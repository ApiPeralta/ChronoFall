using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal con A y D
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Salto con W (solo si está en el suelo — velocidad vertical casi cero)
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
//    public float moveSpeed = 5f;
//    public float jumpForce = 10f;
//    public Transform groundCheck;
//    public float groundCheckRadius = 0.2f;
//    public LayerMask groundLayer;

//    private Rigidbody2D rb;
//    private bool isGrounded;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float moveInput = Input.GetAxisRaw("Horizontal");
//        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

//        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
//        {
//            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
//        }
//    }
//}
