using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public GameObject playerPrefab;          // Prefab del jugador
    public GameObject deathParticlesPrefab;  // Prefab de partículas
    public float destroyDelay = 2f;          // Tiempo antes de destruir el jugador

    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Desactiva renderizadores y colisionadores
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;

        // Instanciar partículas
        if (deathParticlesPrefab != null)
        {
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }

        // Instanciar nuevo jugador
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }

        // Destruir este objeto después de un tiempo
        Destroy(gameObject, destroyDelay);
    }
}
