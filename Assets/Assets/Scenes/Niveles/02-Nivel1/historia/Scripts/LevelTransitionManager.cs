using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    [Header("Canvas y Paneles")]
    public GameObject comicCanvas; 
    public Image[] comicPanels;  
    public Image separators;
    public bool IsPlaying { get; private set; } = false;

    [Header("Escena")]
    public string lobbySceneName = "Inicio Puertas";

    [Header("Aparición automática")]
    public float delayBetweenPanels = 1f; 

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

    IsPlaying = false;
}

    public void TriggerLevelEnd(Sprite[] storySprites)
    {
        IsPlaying = true;

    comicCanvas.SetActive(true);

        comicCanvas.SetActive(true);

        for (int i = 0; i < comicPanels.Length; i++)
        {
            comicPanels[i].sprite = storySprites[i];
            comicPanels[i].gameObject.SetActive(false);
        }

        StartCoroutine(ShowPanelsOneByOne());
    }

public void HideComic()
{
    if (comicCanvas != null)
        comicCanvas.SetActive(false);
}

    private IEnumerator ShowPanelsOneByOne()
{
    IsPlaying = true; 

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

    IsPlaying = false; 
}

    public void EndComicAndGoToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
}