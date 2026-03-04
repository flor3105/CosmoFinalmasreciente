using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // Imagen negra que cubre toda la pantalla
    public float fadeDuration = 1.5f; // duración del fade, ajustable

    // Fade-in: negro → transparente
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    // Fade-out: transparente → negro
    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    // Coroutine genérica de fade
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
