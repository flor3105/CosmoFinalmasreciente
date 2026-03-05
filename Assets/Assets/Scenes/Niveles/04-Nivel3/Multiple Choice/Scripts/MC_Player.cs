using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Player : MonoBehaviour
{
    [Header("Camera Controls")]
    public float sensitivity;
    public float xLimit;

    float xRotation;
    float yRotation;

    public GameObject camObj;
    public Transform camPos;

    [Header("Movement")]
    public float speed;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        camObj.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

            //La rotacion horizontal es sobre el eje Y
            yRotation += mouseX;
            //La rotacion vertical es sobre el eje X
            xRotation -= mouseY;

            Move();

            //Clamp es para asegurarnos de que no mire mas abajo ni arriba de cierto limite
            xRotation = Mathf.Clamp(xRotation, -xLimit, xLimit);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            camObj.transform.position = camPos.transform.position;
            camObj.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    void Move()
    {
        //Multiplicamos los GetAxisRaw por transform.right y forward para que el movimiento varie segun hacia donde miramos
        //A su vez, lo normalizamos para que no se mueva mas rapido cuando va en diagonal
        Vector3 dir = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
