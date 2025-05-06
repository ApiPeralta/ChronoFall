using UnityEngine;
using UnityEngine.SceneManagement;
public class SniperBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 5f;

    private Vector2 direction;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Life>().TakeDamage();
            Destroy(gameObject);
        }
    }
}

