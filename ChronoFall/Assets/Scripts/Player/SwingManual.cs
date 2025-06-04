using UnityEngine;
public class SwingManual : MonoBehaviour
{
    public Transform player;
    public float swingRadius = 4f;
    public float swingSpeed = 90f;

    private bool isSwinging = false;
    public float swingAngle = 0f;
    public float angleDirection = 1f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vector2.Distance(player.position, transform.position) < swingRadius)
            {
                isSwinging = true;
                swingAngle = 0f;
                angleDirection = 1f;
                line.enabled = true;
            }
        }

        if (isSwinging)
        {
            swingAngle += swingSpeed * Time.deltaTime * angleDirection;

            if (swingAngle > 60f) angleDirection = -1f;
            if (swingAngle < -60f) angleDirection = 1f;

            float rad = swingAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Sin(rad), -Mathf.Cos(rad), 0f) * swingRadius;

            player.position = transform.position + offset;

            // Actualizar cuerda visual
            line.SetPosition(0, transform.position);
            line.SetPosition(1, player.position);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isSwinging = false;
                line.enabled = false;

                Vector2 launchDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = launchDir * 10f;
                }
            }
        }
    }
}
