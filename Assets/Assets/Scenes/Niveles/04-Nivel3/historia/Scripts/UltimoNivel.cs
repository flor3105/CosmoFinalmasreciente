using UnityEngine;
using UnityEngine.UI;

public class HistoriaUltimoNivel : MonoBehaviour
{
    public GameObject comicCanvas;  
    public Image comicImage;        
    public Sprite historiaSprite;  

    void Start()
    {
        comicCanvas.SetActive(false); 
    }

    public void TriggerLevelEnd()
    {
        comicCanvas.SetActive(true);
        comicImage.sprite = historiaSprite;
        comicImage.gameObject.SetActive(true);
    }
}
