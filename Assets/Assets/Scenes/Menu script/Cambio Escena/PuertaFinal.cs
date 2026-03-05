using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Sprite[] storySprites; // Para niveles normales
    public Sprite storySprite;    // Para último nivel

    // Indica si es el último nivel
    public bool isLastLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isLastLevel)
        {
            // Llama al LastLevelTransitionManager
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
            // Llama al LevelTransitionManager normal
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