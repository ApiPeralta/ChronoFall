using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cam;      // La c�mara
    private Vector3 previousCamPos; // Posici�n anterior de la c�mara
    public float parallaxMultiplier = 0.5f; // Qu� tanto se mueve (0 = no se mueve, 1 = igual que la c�mara)

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - previousCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, deltaMovement.y * parallaxMultiplier, 0);
        previousCamPos = cam.position;
    }
}

