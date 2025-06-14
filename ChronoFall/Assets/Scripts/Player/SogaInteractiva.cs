using UnityEngine;

public class SogaTrigger : MonoBehaviour
{
    public string tagJugador = "Player"; // Asegurate que tu jugador tenga este tag
    private bool colgado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (colgado) return;

        if (other.CompareTag(tagJugador))
        {
            Rigidbody2D rbJugador = other.GetComponent<Rigidbody2D>();

            if (rbJugador != null)
            {
                DistanceJoint2D joint = other.gameObject.AddComponent<DistanceJoint2D>();
                joint.connectedBody = GetComponent<Rigidbody2D>();
                joint.autoConfigureDistance = false;
                joint.distance = 0.8f;
                joint.enableCollision = true;

                colgado = true;
            }
        }
    }

    void Update()
    {
        if (!colgado) return;

        GameObject jugador = GameObject.FindGameObjectWithTag(tagJugador);
        if (jugador == null) return;

        // Movimiento mientras está colgado
        float h = Input.GetAxis("Horizontal");
        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        rbJugador.AddForce(new Vector2(h * 5f, 0f), ForceMode2D.Force);

        // Soltarse
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DistanceJoint2D joint = jugador.GetComponent<DistanceJoint2D>();
            if (joint != null)
            {
                Destroy(joint);
                colgado = false;
            }
        }
    }
}


