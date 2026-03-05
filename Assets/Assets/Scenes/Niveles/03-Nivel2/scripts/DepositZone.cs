using UnityEngine;

public class DepositZone : MonoBehaviour
{
    public int requiredStars = 3;
    public GameObject door;
    public GameObject[] objectsToShow;

    private int depositedStars = 0;

    private void Start()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CustomTPController player = other.GetComponent<CustomTPController>();
        if (player == null) return;

        if (!player.HasStars()) return;
        if (depositedStars >= requiredStars) return;

        player.RemoveStar();

        if (depositedStars < objectsToShow.Length)
        {
            objectsToShow[depositedStars].SetActive(true);
        }

        depositedStars++;
        Debug.Log($"Objeto depositado: {depositedStars}/{requiredStars}");

        if (depositedStars >= requiredStars)
        {
            UnlockDoor();
        }
    }

    void UnlockDoor()
    {
        Debug.Log("¡Puerta desbloqueada!");

        if (door != null)
        {
            door.SetActive(false);
        }
    }

    public bool HasCompletedTask()
    {
        return depositedStars >= requiredStars;
    }
}
