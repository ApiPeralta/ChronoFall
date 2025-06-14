using UnityEngine;

public class RotateByStep : MonoBehaviour
{
    public float rotationAmount = 120f; // grados por paso
    public float interval = 2f;         // tiempo entre inicios de rotaci�n
    public float rotationSpeed = 90f;   // grados por segundo (ajust� para hacerlo m�s lento)

    private float timer;
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        timer = interval;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (!isRotating && timer <= 0f)
        {
            // Iniciar nueva rotaci�n
            targetRotation *= Quaternion.Euler(0f, 0f, rotationAmount);
            isRotating = true;
            timer = interval; // reiniciar el temporizador
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Si ya lleg� a la rotaci�n destino
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }
}

