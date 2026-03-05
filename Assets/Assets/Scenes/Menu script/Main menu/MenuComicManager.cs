using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuComicManager : MonoBehaviour
{
    [Header("Primer Comic (4 imágenes)")]
    public GameObject firstComicCanvas;   // Canvas con las 4 imágenes
    public Image[] firstComicPanels;      // Las 4 viñetas
    public Button firstComicNextButton;   // Flecha para pasar al comic de 1 imagen
    public Sprite[] firstComicSprites;    // Sprites del primer comic (4 imágenes)

    [Header("Segundo Comic (1 imagen)")]
    public GameObject secondComicCanvas;  // Canvas del segundo comic
    public Image secondComicPanel;        // Imagen única
    public Button secondComicCloseButton; // Botón X
    public Sprite secondComicSprite;      // Sprite del segundo comic

    [Header("Escena inicial y final")]
    public GameObject menuCanvas;         // Canvas del menú principal
    public string lobbySceneName = "Inicio Puertas";

    [Header("Animaciones")]
    public float delayBetweenPanels = 1f; // Tiempo entre viñetas del primer comic
    public float fadeDuration = 1f;       // Duración del fade de cada viñeta

    private void Start()
    {
        // Ocultar todo al inicio
        if (firstComicCanvas != null) firstComicCanvas.SetActive(false);
        if (secondComicCanvas != null) secondComicCanvas.SetActive(false);

        foreach (Image panel in firstComicPanels)
            panel.gameObject.SetActive(false);

        // Asignar listeners
        if (firstComicNextButton != null)
            firstComicNextButton.onClick.AddListener(ShowSecondComic);

        if (secondComicCloseButton != null)
            secondComicCloseButton.onClick.AddListener(EndSecondComicAndGoToLobby);
    }

    /// <summary>
    /// Llamar desde el botón "Comenzar" del menú
    /// </summary>
    public void StartFirstComic()
    {
        // Ocultar el menú al comenzar
        if (menuCanvas != null)
            menuCanvas.SetActive(false);

        if (firstComicSprites.Length != firstComicPanels.Length)
        {
            Debug.LogError("El número de sprites del primer comic no coincide con los paneles.");
            return;
        }

        firstComicCanvas.SetActive(true);

        for (int i = 0; i < firstComicPanels.Length; i++)
        {
            firstComicPanels[i].sprite = firstComicSprites[i];
            firstComicPanels[i].gameObject.SetActive(false);
            CanvasGroup cg = firstComicPanels[i].GetComponent<CanvasGroup>();
            if (cg != null)
                cg.alpha = 0f;
        }

        StartCoroutine(ShowFirstComicPanels());
    }

    private IEnumerator ShowFirstComicPanels()
    {
        foreach (Image panel in firstComicPanels)
        {
            panel.gameObject.SetActive(true);
            CanvasGroup cg = panel.GetComponent<CanvasGroup>();
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                if (cg != null)
                    cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }
            if (cg != null)
                cg.alpha = 1f;

            yield return new WaitForSeconds(delayBetweenPanels);
        }

        // Activar botón flecha al terminar el primer comic
        if (firstComicNextButton != null)
            firstComicNextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Muestra el segundo comic (1 imagen)
    /// </summary>
    public void ShowSecondComic()
{
    firstComicCanvas.SetActive(false);
    secondComicCanvas.SetActive(true);

    if (secondComicPanel != null)
    {
        secondComicPanel.gameObject.SetActive(true); // ← esto faltaba
        secondComicPanel.sprite = secondComicSprite;
    }

    CanvasGroup cg = secondComicPanel.GetComponent<CanvasGroup>();
    if (cg != null)
        cg.alpha = 0f;

    StartCoroutine(FadeInSecondComic(cg));
}

    private IEnumerator FadeInSecondComic(CanvasGroup cg)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (cg != null)
                cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        if (cg != null)
            cg.alpha = 1f;
    }

    /// <summary>
    /// Cierra el segundo comic y carga la escena del lobby
    /// </summary>
public void EndSecondComicAndGoToLobby()
{
    StartCoroutine(LoadLobby());
}

IEnumerator LoadLobby()
{
    // Ocultar todo
    if (secondComicCanvas != null)
        secondComicCanvas.SetActive(false);

    if (firstComicCanvas != null)
        firstComicCanvas.SetActive(false);

    if (menuCanvas != null)
        menuCanvas.SetActive(false);

    // esperar 1 frame
    yield return null;

    // cargar escena
    SceneManager.LoadScene(lobbySceneName);
}
}