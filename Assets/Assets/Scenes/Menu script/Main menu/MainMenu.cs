using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
   
    void Start()
    {
        
    }

    
    void Update()
    {
    }

    public void LoadSceneint(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
