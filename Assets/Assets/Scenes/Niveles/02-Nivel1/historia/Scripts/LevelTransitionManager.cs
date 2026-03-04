using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionManager : MonoBehaviour
{
    public GameObject comicCanvas;      // Canvas que contiene la historia
    public Image comicImage;            // Imagen que se mostrará
    public string lobbySceneName = "Inicio Puertas";

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
    }

    public void EndComicAndGoToLobby()
    {
        CustomTPController.Instance.canMove = true;
        SceneManager.LoadScene(lobbySceneName);
    }
}
