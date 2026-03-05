using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    [Header("Canvas y Paneles")]
    public GameObject comicCanvas;      // Canvas que contiene la historia
    public Image[] comicPanels;         // Las 4 viñetas (UL, UR, LL, LR)
    public Image separators;
    public bool IsPlaying { get; private set; } = false;

    [Header("Escena")]
    public string lobbySceneName = "Inicio Puertas";

    [Header("Aparición automática")]
    public float delayBetweenPanels = 1f; // Tiempo en segundos entre cada viñeta

private void Start()
{
    if (comicCanvas != null)
        comicCanvas.SetActive(false);

    if (comicPanels != null)
    {
        foreach (var panel in comicPanels)
        {
            if (panel != null)
                panel.gameObject.SetActive(false);
        }
    }

    // Asegurarse que no se reproduce automáticamente
    IsPlaying = false;
}

    /// <summary>
    /// Inicia el cómic con un array de 4 sprites y los muestra de a uno automáticamente
    /// </summary>
    public void TriggerLevelEnd(Sprite[] storySprites)
    {
        IsPlaying = true; // marcar que el comic está en reproducción

    comicCanvas.SetActive(true);

        // Activar Canvas
        comicCanvas.SetActive(true);

        // Asignar sprites y ocultar viñetas
        for (int i = 0; i < comicPanels.Length; i++)
        {
            comicPanels[i].sprite = storySprites[i];
            comicPanels[i].gameObject.SetActive(false); // empiezan ocultas
        }

        // Iniciar la secuencia automática
        StartCoroutine(ShowPanelsOneByOne());
    }

public void HideComic()
{
    if (comicCanvas != null)
        comicCanvas.SetActive(false);
}

    /// <summary>
    /// Muestra las viñetas de a una automáticamente
    /// </summary>
    private IEnumerator ShowPanelsOneByOne()
{
    IsPlaying = true; // Se empieza a reproducir

    foreach (Image panel in comicPanels)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        panel.gameObject.SetActive(true);

        float t = 0f;
        float fadeDuration = 1f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        cg.alpha = 1f;
        yield return new WaitForSeconds(delayBetweenPanels);
    }

    if (separators != null)
        separators.gameObject.SetActive(true);

    IsPlaying = false; // Termina de reproducirse
}

    /// <summary>
    /// Llamar desde botón de cerrar cómic
    /// </summary>
    public void EndComicAndGoToLobby()
    {
        // Simplemente cargar el lobby sin pantalla de carga
        SceneManager.LoadScene(lobbySceneName);
    }
}