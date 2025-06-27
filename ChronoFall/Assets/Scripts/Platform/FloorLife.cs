using System.Collections;
using UnityEngine;

public class FloorLife : MonoBehaviour
{
    public int life = 6;
    public Color hitColor = Color.red;
    public float flashDuration = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            life--;
            if (!isFlashing)
                StartCoroutine(FlashOnHit());
        }
    }

    IEnumerator FlashOnHit()
    {
        isFlashing = true;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}

