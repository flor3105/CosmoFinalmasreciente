using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
    public Animator PuertaAnim;

    void OnTriggerEnter(Collider other)
    {
        PuertaAnim.Play("Abrir");
    }

    void OnTriggerExit(Collider other)
    {
        PuertaAnim.Play("Cerrar");
    }
}

