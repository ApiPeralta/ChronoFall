using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;           // Prefab a instanciar
    public Vector3 spawnPosition;       // Posición donde se spawnea
    public float spawnInterval = 2f;    // Tiempo entre spawns (segundos)
    public float yLimit = -5f;          // Altura mínima antes de destruir el objeto

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }

    void SpawnPrefab()
    {
        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
        obj.AddComponent<AutoDestroyBelowY>().yLimit = yLimit;
    }
}


