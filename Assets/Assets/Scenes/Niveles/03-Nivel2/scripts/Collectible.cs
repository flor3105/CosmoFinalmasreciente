using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CustomTPController player = other.GetComponent<CustomTPController>();

            if (player != null)
            {
                // Añade este objeto a la lista del jugador
                player.AddStar(gameObject);

                Debug.Log("Objeto recolectado!");

                // Destruye el objeto de la escena
                Destroy(gameObject);
            }
        }
    }
}
