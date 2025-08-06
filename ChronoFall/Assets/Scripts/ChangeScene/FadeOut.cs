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
    private bool menuOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOn = !menuOn;
            menuPanel.SetActive(menuOn);
        }
    }
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
    public void Level1()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
    public void Level2()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 2);
    }
    public void Level3()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 3);
    }
    public void Level4()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 4);
    }
    public void Level5()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 5);
    }
    public void Level6()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 6);
    }
    public void Level7()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 7);
    }
    public void Level8()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 8);
    }
    public void Level9()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 9);
    }
    public void Level10()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 10);
    }
    public void Level11()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 11);
    }
    public void Level12()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 12);
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


