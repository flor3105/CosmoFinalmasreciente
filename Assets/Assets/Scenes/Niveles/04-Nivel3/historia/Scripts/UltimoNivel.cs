using UnityEngine;
using UnityEngine.UI;

public class HistoriaUltimoNivel : MonoBehaviour
{
    public GameObject comicCanvas;   // Canvas del último nivel
    public Image comicImage;         // Imagen grande
    public Sprite historiaSprite;    // Sprite de la historia del nivel

    void Start()
    {
        comicCanvas.SetActive(false); // Ocultar al inicio
    }

    public void TriggerLevelEnd()
    {
        comicCanvas.SetActive(true);
        comicImage.sprite = historiaSprite;
        comicImage.gameObject.SetActive(true);
    }
}
