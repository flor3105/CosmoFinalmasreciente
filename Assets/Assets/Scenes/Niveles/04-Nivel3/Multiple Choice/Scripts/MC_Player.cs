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

    
    void Start()
    {
        camObj.transform.rotation = transform.rotation;
    }

    
    void Update()
    {
        if(canMove)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

            
            yRotation += mouseX;
            
            xRotation -= mouseY;

            Move();

            
            xRotation = Mathf.Clamp(xRotation, -xLimit, xLimit);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            camObj.transform.position = camPos.transform.position;
            camObj.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    void Move()
    {
       
        Vector3 dir = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
