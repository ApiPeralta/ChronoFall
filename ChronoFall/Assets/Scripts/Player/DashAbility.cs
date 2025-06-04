using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashForce = 20f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool canDash = true;
    private Movement movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (movement.GetIsSlowing() && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        canDash = false;

        float direction = Input.GetAxisRaw("Horizontal");
        if (direction == 0f)
            direction = transform.localScale.x;

        rb.velocity = new Vector2(direction * dashForce, rb.velocity.y);

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

