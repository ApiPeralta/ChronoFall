using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerCloneThrower : MonoBehaviour
{
    public GameObject clonePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float cloneGravity = 0.3f; // debe coincidir con gravityScale del clon
    public int trajectoryPoints = 30;
    public float timeBetweenPoints = 0.1f;

    private GameObject currentClone;
    private bool isAiming = false;
    private LineRenderer lineRenderer;
    private PlayerAbilitiesManager stamina;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = trajectoryPoints;
        lineRenderer.enabled = false;

        stamina = FindObjectOfType<PlayerAbilitiesManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && stamina != null && stamina.CanSpawnClone())
        {
            isAiming = true;
            lineRenderer.enabled = true;
        }

        if (isAiming)
        {
            Vector2 direction = GetMouseDirection();
            ShowTrajectory(direction);
        }

        if (Input.GetKeyUp(KeyCode.Q) && isAiming)
        {
            Vector2 direction = GetMouseDirection();
            ThrowClone(direction);
            isAiming = false;
            lineRenderer.enabled = false;
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - throwPoint.position).normalized;
        return direction;
    }

    void ThrowClone(Vector2 direction)
    {
        if (stamina == null || !stamina.CanSpawnClone())
            return;

        if (currentClone != null)
            Destroy(currentClone);

        currentClone = Instantiate(clonePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = currentClone.GetComponent<Rigidbody2D>();
        rb.velocity = direction * throwForce;

        stamina.UseClone(); // se gasta un clon
    }

    void ShowTrajectory(Vector2 direction)
    {
        Vector2 startPos = throwPoint.position;
        Vector2 startVelocity = direction * throwForce;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeBetweenPoints;
            Vector2 pos = startPos + startVelocity * t + 0.5f * Physics2D.gravity * cloneGravity * t * t;
            lineRenderer.SetPosition(i, pos);
        }
    }
}
