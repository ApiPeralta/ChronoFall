using UnityEngine;

public class CloneSelfDestruct : MonoBehaviour
{
    public float lifetime = 5f;

    private PlayerAbilitiesManager stamina;

    void Start()
    {
        stamina = FindObjectOfType<PlayerAbilitiesManager>();
        Invoke(nameof(DestroySelf), lifetime);
    }

    void DestroySelf()
    {
        if (stamina != null)
            stamina.RecoverClone(); // Devolvemos un clon al pool
        Destroy(gameObject);
    }
}

