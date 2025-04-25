using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
