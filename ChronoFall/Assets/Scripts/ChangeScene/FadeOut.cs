using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image fadePanel;
    public float fadeSpeed = 1f;

    public void StartFade()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        Color panelColor = fadePanel.color;
        while (panelColor.a < 1)
        {
            panelColor.a += Time.deltaTime * fadeSpeed;
            fadePanel.color = panelColor;
            yield return null;
        }
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}

