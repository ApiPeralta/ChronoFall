using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives = 3;

    public Transform lifeBarParent;
    public GameObject lifeRectPrefab;
    private Rigidbody2D rb;
    private GameObject[] lifeRects;

    public float timeBeforeFade = 3f; // segundos antes de empezar a desaparecer
    public float fadeSpeed = 1f; // qué tan rápido se desvanecen
    private float timeSinceLastHit = 0f;
    private bool isFading = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Inicializar los rectángulos de vida
        lifeRects = new GameObject[maxLives];
        for (int i = 0; i < maxLives; i++)
        {
            GameObject rect = Instantiate(lifeRectPrefab, lifeBarParent);
            rect.transform.localPosition = new Vector3(i * 0.4f, 0, 0); // Espaciado horizontal
            lifeRects[i] = rect;
        }
    }
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;

        if (timeSinceLastHit >= timeBeforeFade)
        {
            FadeOutLifeRects();
        }
    }
    void FadeOutLifeRects()
    {
        foreach (var rect in lifeRects)
        {
            if (rect == null) continue;
            var sr = rect.GetComponent<SpriteRenderer>();
            if (sr == null) continue;

            Color c = sr.color;
            c.a = Mathf.MoveTowards(c.a, 0f, fadeSpeed * Time.deltaTime); // baja alfa hacia 0
            sr.color = c;
        }
    }

    public void TakeDamage()
    {
        timeSinceLastHit = 0f;

        foreach (var rect in lifeRects)
        {
            if (rect == null) continue;
            var sr = rect.GetComponent<SpriteRenderer>();
            if (sr == null) continue;

            Color c = sr.color;
            c.a = 1f; // vuelve a aparecer instantáneamente
            sr.color = c;
        }

        if (currentLives <= 0) return;

        currentLives--;

        // Ocultar el rectángulo de vida correspondiente
        if (currentLives >= 0 && currentLives < lifeRects.Length)
        {
            lifeRects[currentLives].SetActive(false);
        }

        if (currentLives <= 0)
        {
            // Reiniciar nivel o hacer animación de muerte
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
