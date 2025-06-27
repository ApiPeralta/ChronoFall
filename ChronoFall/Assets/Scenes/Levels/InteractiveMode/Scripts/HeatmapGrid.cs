using System.Collections.Generic;
using UnityEngine;

public class HeatmapGrid : MonoBehaviour
{
    public float cellSize = 1f;
    private Dictionary<Vector2Int, int> heatmap = new Dictionary<Vector2Int, int>();

    // Asocia una posición a la celda lógica de la grilla
    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    public void AddPosition(Vector2 worldPosition)
    {
        Vector2Int cell = WorldToGrid(worldPosition);

        if (heatmap.ContainsKey(cell))
            heatmap[cell]++;
        else
            heatmap[cell] = 1;
    }

    public List<Vector2Int> GetHotCells(int minVisits = 3)
    {
        List<Vector2Int> hotCells = new List<Vector2Int>();
        foreach (var pair in heatmap)
        {
            if (pair.Value >= minVisits)
                hotCells.Add(pair.Key);
        }
        return hotCells;
    }

    public Vector3 GridToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * cellSize + cellSize / 2f, cell.y * cellSize + cellSize / 2f, 0);
    }

    public void Clear()
    {
        heatmap.Clear();
    }
}

