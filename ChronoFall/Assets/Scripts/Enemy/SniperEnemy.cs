using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireInterval = 3f;
    private float fireTimer;
    private Transform player;

    private LineRenderer laserLine;

    void Start()
    {
        fireTimer = fireInterval;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        laserLine = GetComponent<LineRenderer>();

        if (laserLine != null)
        {
            laserLine.startWidth = 0.05f;
            laserLine.endWidth = 0.05f;
            laserLine.material = new Material(Shader.Find("Sprites/Default"));
            laserLine.startColor = Color.red;
            laserLine.endColor = Color.red;
        }
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireInterval;
        }

        // Opcional: apuntar
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        if (laserLine != null && player != null)
        {
            Vector3 startPos = firePoint.position;
            Vector3 dir = (player.position - firePoint.position).normalized;
            Vector3 endPos = startPos + dir * 20f;

            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);
        }

    }
    void Fire()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.GetComponent<SniperBullet>().SetDirection(direction);
    }
}

