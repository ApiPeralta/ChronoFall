using System.Collections;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject deathParticlesPrefab;
    public float destroyDelay = 2f;
    private SpriteRenderer r;
    private Rigidbody2D body;
    public GameObject camera;

    private bool isDead = false; // flag para evitar m�ltiples muertes

    private void Start()
    {
        r = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        deathParticlesPrefab.SetActive(false);
        r.enabled = true;
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDead = false;

        // Buscar c�mara autom�ticamente si no est� asignada
        if (camera == null)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && other.CompareTag("Death"))
        {
            isDead = true; // bloquea ejecuciones posteriores

            body.constraints = RigidbodyConstraints2D.FreezeAll;
            deathParticlesPrefab.SetActive(true);
            r.enabled = false;

            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(destroyDelay);

        if (playerPrefab != null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, new Vector3(-9f, 2f, 1f), Quaternion.identity);

            // Hacer que la c�mara siga al nuevo jugador
            CameraFollow camFollow = camera.GetComponent<CameraFollow>();
            if (camFollow != null)
                camFollow.target = newPlayer.transform;
        }

        Destroy(gameObject);
    }
}
