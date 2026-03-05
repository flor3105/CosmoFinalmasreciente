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
                
                player.AddStar(gameObject);

                Debug.Log("Objeto recolectado!");

                
                Destroy(gameObject);
            }
        }
    }
}
