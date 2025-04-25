using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFallActivated : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeFall playerScript = other.GetComponent<TimeFall>();
            if (playerScript != null)
            {
                playerScript.enabled = true;
            }
            this.gameObject.SetActive(false);
        }
    }
}
