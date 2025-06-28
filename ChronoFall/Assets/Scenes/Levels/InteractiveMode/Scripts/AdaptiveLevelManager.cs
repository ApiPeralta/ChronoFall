using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdaptiveLevelManager : MonoBehaviour
{
    public static AdaptiveLevelManager Instance;

    private Dictionary<Vector2Int, int> heatmap = new Dictionary<Vector2Int, int>();

    [Header("Enemigos")]
    public GameObject[] enemyPrefabs;
    public int minVisitsForTrap = 3;
    public int maxTrapsPerLevel = 10;

    [Header("Grilla y restricciones")]
    public float cellSize = 1f;
    public LayerMask groundLayer;

    private HashSet<Vector2Int> zonasProhibidas = new HashSet<Vector2Int>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CalcularZonasProhibidas();
        AtenuarHeatmap();
        GenerateEnemies();
    }

    public void AddPositions(List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            Vector2Int cell = WorldToGrid(pos);
            if (heatmap.ContainsKey(cell))
                heatmap[cell]++;
            else
                heatmap[cell] = 1;
        }
    }

    Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    Vector3 GridToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * cellSize + cellSize / 2f, cell.y * cellSize + cellSize / 2f, 0);
    }

    void CalcularZonasProhibidas()
    {
        zonasProhibidas.Clear();
        GameObject[] zonas = GameObject.FindGameObjectsWithTag("ZonaProhibida");

        foreach (var zona in zonas)
        {
            BoxCollider2D box = zona.GetComponent<BoxCollider2D>();
            if (box != null)
            {
                Vector2 min = box.bounds.min;
                Vector2 max = box.bounds.max;

                for (float x = min.x; x <= max.x; x += cellSize)
                {
                    for (float y = min.y; y <= max.y; y += cellSize)
                    {
                        Vector2Int cell = WorldToGrid(new Vector2(x, y));
                        zonasProhibidas.Add(cell);
                    }
                }
            }
        }
    }

    void AtenuarHeatmap()
    {
        var keys = new List<Vector2Int>(heatmap.Keys);
        foreach (var key in keys)
        {
            heatmap[key] = Mathf.Max(heatmap[key] / 2, 0);
        }
    }

    bool HayAlgoEn(Vector3 posicion, Vector2 tamaño)
    {
        return Physics2D.OverlapBox(posicion, tamaño, 0f) != null;
    }

    void GenerateEnemies()
    {
        Debug.Log("Generando enemigos...");

        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No hay enemigos en el array.");
            return;
        }

        var sortedCells = new List<KeyValuePair<Vector2Int, int>>(heatmap);
        sortedCells.Sort((a, b) => b.Value.CompareTo(a.Value));

        int enemiesPlaced = 0;

        foreach (var cell in sortedCells)
        {
            Debug.Log($"Celda {cell.Key} con {cell.Value} visitas");

            if (cell.Value >= minVisitsForTrap && !zonasProhibidas.Contains(cell.Key))
            {
                Vector3 enemyPos = GridToWorld(cell.Key);

                // TEMP: Desactivamos check de solapamiento
                // if (HayAlgoEn(enemyPos, new Vector2(1f, 1f))) continue;

                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                GameObject enemy = Instantiate(prefab, enemyPos, Quaternion.identity);
                DontDestroyOnLoad(enemy);
                Debug.Log($"Instanciado enemigo en {enemyPos}");

                enemiesPlaced++;
                if (enemiesPlaced >= maxTrapsPerLevel)
                    break;
            }
        }

        if (enemiesPlaced == 0)
            Debug.Log("No se generó ningún enemigo.");
    }


    public void ClearHeatmap()
    {
        heatmap.Clear();
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var cell in zonasProhibidas)
        {
            Gizmos.DrawWireCube(GridToWorld(cell), new Vector3(cellSize, cellSize, 0));
        }
    }
#endif
}



