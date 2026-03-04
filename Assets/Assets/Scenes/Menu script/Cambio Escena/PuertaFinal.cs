using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Sprite storySprite; // La imagen que quieres mostrar para este nivel

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Llama al LevelTransitionManager y pasa la imagen correspondiente
            FindObjectOfType<LevelTransitionManager>().TriggerLevelEnd(storySprite);
        }
    }
}