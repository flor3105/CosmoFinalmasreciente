using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("Puerta abierta!");
        gameObject.SetActive(false); 
    }
}
