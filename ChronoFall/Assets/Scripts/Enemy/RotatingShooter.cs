using UnityEngine;

public class RotatingShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float rotationSpeed = 90f;

    public Transform shootUp;
    public Transform shootRight;
    public Transform shootDown;
    public Transform shootLeft;

    private float timer;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= 1f / fireRate)
        {
            FireBullets();
            timer = 0f;
        }
    }

    void FireBullets()
    {
        ShootFrom(shootUp, shootUp.up);
        ShootFrom(shootRight, shootRight.up);
        ShootFrom(shootDown, shootDown.up);
        ShootFrom(shootLeft, shootLeft.up);
    }

    void ShootFrom(Transform point, Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
        SniperBullet sb = bullet.GetComponent<SniperBullet>();
        if (sb != null)
        {
            sb.SetDirection(direction);
        }
    }
}
