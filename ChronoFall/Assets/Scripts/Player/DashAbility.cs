using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashForce = 20f;
    public float dashCooldown = 1f;
    public GameObject ghostPrefab;
    public float ghostDuration = 6f;
    public float fadeOutDuration = 2f;

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

        // Crear clon
        Vector3 spawnOffset = new Vector3(-Mathf.Sign(transform.localScale.x) * 1.5f, 0f, 0f); // Ajusta el 1.5f si querés más o menos distancia
        GameObject ghost = Instantiate(ghostPrefab, transform.position + spawnOffset, transform.rotation);
        ghost.tag = "Clone";

        // Eliminar scripts del clon (opcional)
        Destroy(ghost.GetComponent<DashAbility>());
        Destroy(ghost.GetComponent<Movement>());

        string originalTag = gameObject.tag;
        gameObject.tag = "Invisible";

        yield return new WaitForSeconds(ghostDuration);

        StartCoroutine(FadeOutAndDestroy(ghost, fadeOutDuration));
        gameObject.tag = originalTag;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Destroy(obj);
            yield break;
        }

        Color originalColor = sr.color;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }
}

