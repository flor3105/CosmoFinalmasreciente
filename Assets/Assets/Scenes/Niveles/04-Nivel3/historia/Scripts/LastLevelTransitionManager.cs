using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LastLevelTransitionManager : MonoBehaviour
{
    [Header("Canvas y Panel")]
    public GameObject comicCanvas;  
    public Image comicPanel;        
    public bool IsPlaying { get; private set; } = false;

    [Header("Escena")]
    public string menuSceneName;

    [Header("Aparición automática")]
    public float fadeDuration = 1f; 

    private void Start()
    {
        comicCanvas.SetActive(false);
        if (comicPanel != null)
            comicPanel.gameObject.SetActive(false);
    }

    public void TriggerLevelEnd(Sprite storySprite)
{
    comicCanvas.SetActive(true);
    comicPanel.sprite = storySprite;
    comicPanel.gameObject.SetActive(true);

    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    // Iniciar el fade
    StartCoroutine(FadeInPanel());
}

    private IEnumerator FadeInPanel()
    {
        float t = 0f;
        CanvasGroup cg = comicPanel.GetComponent<CanvasGroup>();
        cg.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        cg.alpha = 1f;
    }

    public void EndComicAndGoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}