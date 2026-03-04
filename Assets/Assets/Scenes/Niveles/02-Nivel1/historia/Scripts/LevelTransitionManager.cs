using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelTransitionManager : MonoBehaviour
{
    public GameObject comicCanvas;      // Canvas que contiene la historia
    public Image comicImage;            // Imagen que se mostrará
    public string lobbySceneName = "Inicio Puertas";
    public FadeController fadeController;

    // Oculta el Canvas al iniciar
    void Start()
    {
        comicCanvas.SetActive(false);
    }

    // Mostrar historia pasando la imagen que corresponde
    public void TriggerLevelEnd(Sprite storySprite)
    {
        CustomTPController.Instance.canMove = false;

        comicImage.sprite = storySprite; // Cambia la imagen
        comicCanvas.SetActive(true);

        StartCoroutine(FadeInStory());
    }

    public void EndComicAndGoToLobby()
{
    CustomTPController.Instance.canMove = true;
    StartCoroutine(FadeThenLoad());
}

private IEnumerator FadeInStory()
{
    comicImage.color = new Color(1f, 1f, 1f, 0f); // comienza transparente
    float t = 0f;
    float duration = 1.5f; // duración del fade notorio
    while(t < duration)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, t / duration);
        comicImage.color = new Color(1f, 1f, 1f, alpha);
        yield return null;
    }
    comicImage.color = new Color(1f, 1f, 1f, 1f); // termina completamente visible
}

private IEnumerator FadeThenLoad()
{
    yield return StartCoroutine(FadeOutStory()); // primero fade de la historia
    fadeController.FadeOut();                     // opcional: luego fade negro
    yield return new WaitForSeconds(fadeController.fadeDuration);
    SceneManager.LoadScene(lobbySceneName);
}

private IEnumerator FadeOutStory()
{
    float t = 0f;
    float duration = 1.5f;
    Color c = comicImage.color;

    while (t < duration)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, t / duration);
        comicImage.color = new Color(c.r, c.g, c.b, alpha);
        yield return null;
    }
    comicImage.color = new Color(c.r, c.g, c.b, 0f);
}
}
