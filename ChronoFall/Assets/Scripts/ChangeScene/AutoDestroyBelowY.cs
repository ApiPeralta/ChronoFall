using UnityEngine;

public class AutoDestroyBelowY : MonoBehaviour
{
    public float yLimit = -5f;

    void Update()
    {
        if (transform.position.y < yLimit)
        {
            Destroy(gameObject);
        }
    }
}

