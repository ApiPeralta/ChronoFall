using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public PathTracker tracker;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return; // Evitar múltiples activaciones
        if (other.CompareTag("Player"))
        {
            triggered = true;

            AdaptiveLevelManager.Instance.AddPositions(tracker.recordedPositions);
            tracker.Clear();

            // Reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}



