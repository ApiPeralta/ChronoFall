using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changer : MonoBehaviour
{
    private FadeOut fadeOut;
    private void Start()
    {
        fadeOut = GetComponent<FadeOut>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeOut.StartFade();
        }

    }
}
