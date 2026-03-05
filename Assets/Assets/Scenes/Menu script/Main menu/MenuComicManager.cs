using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuComicManager : MonoBehaviour
{
    [Header("Primer Comic (4 imágenes)")]
    public GameObject firstComicCanvas;   
    public Image[] firstComicPanels;      
    public Button firstComicNextButton;   
    public Sprite[] firstComicSprites;    

    [Header("Segundo Comic (1 imagen)")]
    public GameObject secondComicCanvas;  
    public Image secondComicPanel;        
    public Button secondComicCloseButton; 
    public Sprite secondComicSprite;      

    [Header("Escena inicial y final")]
    public GameObject menuCanvas;         
    public string lobbySceneName = "Inicio Puertas";

    [Header("Animaciones")]
    public float delayBetweenPanels = 1f; 
    public float fadeDuration = 1f;       

    private void Start()
    {
        if (firstComicCanvas != null) firstComicCanvas.SetActive(false);
        if (secondComicCanvas != null) secondComicCanvas.SetActive(false);

        foreach (Image panel in firstComicPanels)
            panel.gameObject.SetActive(false);

        if (firstComicNextButton != null)
            firstComicNextButton.onClick.AddListener(ShowSecondComic);

        if (secondComicCloseButton != null)
            secondComicCloseButton.onClick.AddListener(EndSecondComicAndGoToLobby);
    }

    public void StartFirstComic()
    {
        if (menuCanvas != null)
            menuCanvas.SetActive(false);

        if (firstComicSprites.Length != firstComicPanels.Length)
        {
            Debug.LogError("El número de sprites no coincide con los paneles.");
            return;
        }

        firstComicCanvas.SetActive(true);

        for (int i = 0; i < firstComicPanels.Length; i++)
        {
            firstComicPanels[i].sprite = firstComicSprites[i];
            firstComicPanels[i].gameObject.SetActive(false);
            CanvasGroup cg = firstComicPanels[i].GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 0f;
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
            if (cg != null) cg.alpha = 1f;

            yield return new WaitForSeconds(delayBetweenPanels);
        }

        if (firstComicNextButton != null)
            firstComicNextButton.gameObject.SetActive(true);
    }

    public void ShowSecondComic()
    {
        firstComicCanvas.SetActive(false);
        secondComicCanvas.SetActive(true);

        if (secondComicPanel != null)
        {
            secondComicPanel.gameObject.SetActive(true);
            secondComicPanel.sprite = secondComicSprite;
        }

        CanvasGroup cg = secondComicPanel.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 0f;

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
        if (cg != null) cg.alpha = 1f;
    }

    public void EndSecondComicAndGoToLobby()
    {
        if (secondComicCloseButton != null)
            secondComicCloseButton.interactable = false;

        StartCoroutine(LoadLobbyRoutine());
    }

    IEnumerator LoadLobbyRoutine()
    {
        if (menuCanvas != null)
            menuCanvas.SetActive(false);

        SceneManager.LoadScene(lobbySceneName);

        yield break; 
    }
}