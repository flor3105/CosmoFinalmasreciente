using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Sprite[] storySprites; 
    public Sprite storySprite;    

    public bool isLastLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isLastLevel)
        {
            LastLevelTransitionManager lastManager = FindObjectOfType<LastLevelTransitionManager>();
            if (lastManager != null && storySprite != null)
            {
                lastManager.TriggerLevelEnd(storySprite);
            }
            else
            {
                Debug.LogError("No se encontró LastLevelTransitionManager o falta storySprite");
            }
        }
        else
        {
            LevelTransitionManager manager = FindObjectOfType<LevelTransitionManager>();
            if (manager != null && storySprites != null && storySprites.Length > 0)
            {
                manager.TriggerLevelEnd(storySprites);
            }
            else
            {
                Debug.LogError("No se encontró LevelTransitionManager o falta storySprites");
            }
        }
    }
}