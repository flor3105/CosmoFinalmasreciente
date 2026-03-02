using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int requiredObjects = 3;
    int currentObjects = 0;

    public Animator doorAnimator;

    public void AddObject()
    {
        currentObjects++;

        Debug.Log("Objetos en puerta: " + currentObjects);

        if (currentObjects >= requiredObjects)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Debug.Log("Puerta abierta!");

        if (doorAnimator != null)
            doorAnimator.SetTrigger("Abrir");
        else
            gameObject.SetActive(false);
    }
}