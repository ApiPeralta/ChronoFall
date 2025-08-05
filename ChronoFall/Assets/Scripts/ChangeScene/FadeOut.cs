using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image fadePanel;
    public GameObject menuPanel;
    public float fadeSpeed = 1f;

    private bool isFading = false;

    public void StartFade()
    {
        if (!isFading)
            StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        isFading = true;
        Color panelColor = fadePanel.color;
        while (panelColor.a < 1f)
        {
            panelColor.a += Time.deltaTime * fadeSpeed;
            fadePanel.color = panelColor;
            yield return null;
        }

        menuPanel.SetActive(true); // Mostrar menú al terminar el fade
    }

    public void ContinueGame()
    {
        StartCoroutine(FadeBackIn());
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    IEnumerator FadeBackIn()
    {
        Color panelColor = fadePanel.color;
        while (panelColor.a > 0f)
        {
            panelColor.a -= Time.deltaTime * fadeSpeed;
            fadePanel.color = panelColor;
            yield return null;
        }

        menuPanel.SetActive(false);
        isFading = false;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Portada"); // Asegurate que la escena del menú se llame así o cambiá el nombre
    }
}


