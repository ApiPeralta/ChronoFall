using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdaptiveLevelManager : MonoBehaviour
{
    public static AdaptiveLevelManager Instance;

    // Heatmap guardado acumulado por nivel
    private Dictionary<Vector2Int, int> heatmap = new Dictionary<Vector2Int, int>();

    // Parámetros para colocar trampas
    public GameObject trampaPrefab;
    public float cellSize = 1f;
    public int minVisitsForTrap = 5;
    public int maxTrapsPerLevel = 10;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Opcional: escuchamos cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cada vez que se carga un nivel, generamos trampas basadas en el heatmap guardado
        GenerateTrapsInLevel();
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

    void GenerateTrapsInLevel()
    {
        if (trampaPrefab == null)
        {
            Debug.LogWarning("AdaptiveLevelManager: No hay trampa prefab asignado.");
            return;
        }

        // Limpiamos trampas anteriores en la escena (opcional)
        var oldTraps = GameObject.FindGameObjectsWithTag("Death");
        foreach (var trap in oldTraps)
            Destroy(trap);

        // Ordenamos las celdas por cantidad de visitas descendente
        var sortedCells = new List<KeyValuePair<Vector2Int, int>>(heatmap);
        sortedCells.Sort((a, b) => b.Value.CompareTo(a.Value));

        int trapsPlaced = 0;

        foreach (var cell in sortedCells)
        {
            if (cell.Value >= minVisitsForTrap)
            {
                Vector3 pos = GridToWorld(cell.Key);
                Instantiate(trampaPrefab, pos, Quaternion.identity);
                trapsPlaced++;

                if (trapsPlaced >= maxTrapsPerLevel)
                    break;
            }
        }
    }

    public void ClearHeatmap()
    {
        heatmap.Clear();
    }
}

