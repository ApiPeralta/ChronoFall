using UnityEngine;

public class PlayerGlider : MonoBehaviour
{
    public float normalGravity = 1f;
    public float glideGravity = 0.1f;
    private Rigidbody2D rb;
    private PlayerAbilitiesManager stamina;

    public GameObject glider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stamina = FindObjectOfType<PlayerAbilitiesManager>();
        glider.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y < 0 && stamina.CanGlide())
        {
            glider.SetActive(true);
            rb.gravityScale = glideGravity;
            stamina.DrainGlide(1f);
        }
        else
        {
            glider.SetActive(false);
            rb.gravityScale = normalGravity;
        }
    }
}

