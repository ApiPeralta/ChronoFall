using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToActiveSniper : MonoBehaviour
{
    public GameObject sniperLaser; // Asigná el objeto con LineRenderer y el script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activar el objeto si estaba desactivado
            sniperLaser.SetActive(true);

            // Activar el script (por ejemplo SniperLaser.cs)
            var script = sniperLaser.GetComponent<SniperEnemy>();
            if (script != null)
                script.enabled = true;

            // Activar el LineRenderer
            var line = sniperLaser.GetComponent<LineRenderer>();
            if (line != null)
                line.enabled = true;

            // Opcional: desactivar el trigger para que solo pase una vez
            gameObject.SetActive(false);
        }
    }
}
