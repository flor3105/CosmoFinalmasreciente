using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
   
   public TMP_Text cardText;
   public int collectibles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cardText.text = collectibles.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
