using System.Collections.Generic;
using UnityEngine;

public class PathTracker : MonoBehaviour
{
    public float recordInterval = 0.2f;
    public List<Vector2> recordedPositions = new List<Vector2>();

    void Start()
    {
        InvokeRepeating(nameof(RecordPosition), 0f, recordInterval);
    }

    void RecordPosition()
    {
        recordedPositions.Add(transform.position);
    }

    public void Clear()
    {
        recordedPositions.Clear();
    }
}

