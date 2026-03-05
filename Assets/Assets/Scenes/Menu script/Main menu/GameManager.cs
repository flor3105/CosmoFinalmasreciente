using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text cardText;
    private int collectibles;

    void Start()
    {
        ActualizarInterfaz();
    }

    public void AddCollectible(int amount)
    {
        collectibles += amount;
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        if (cardText != null)
        {
            cardText.text = collectibles.ToString();
        }
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
