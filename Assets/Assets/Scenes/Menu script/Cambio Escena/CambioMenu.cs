using UnityEngine;

public class CambioMenu : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu; 
    [SerializeField] private int sceneIndex = 0; 
    [SerializeField] private DepositZone depositZone; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (depositZone != null && depositZone.HasCompletedTask())
            {
               
                if (mainMenu != null)
                {
                    mainMenu.LoadSceneint(sceneIndex);
                }
                else
                {
                    Debug.LogError("No se ha asignado el script MainMenu en el Inspector.");
                }
            }
            else
            {
                Debug.Log("No se han completado las tareas necesarias para cambiar de escena.");
            }
        }
    }
}
