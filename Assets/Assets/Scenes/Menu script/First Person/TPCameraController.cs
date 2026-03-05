using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraController : MonoBehaviour
{
    public float sensitivity = 100f;
    private float xRotation;
    private float yRotation;
    public float minVerticalAngle = -30f; 
    public float maxVerticalAngle = 60f; 
    public Transform player;

    public float verticalOffset = 2f; 
    public float playerDistance = 4f;
    public LayerMask environmentMask; 

    void Start()
{
    Vector3 angles = transform.eulerAngles;

    xRotation = angles.x;

    if (xRotation > 180f)
        xRotation -= 360f;

    yRotation = angles.y;
}

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        Vector3 desiredPosition = player.position + Vector3.up * verticalOffset - transform.forward * playerDistance;

        Vector3 adjustedPosition = AdjustCameraPosition(desiredPosition);

        transform.position = adjustedPosition;
    }

    private Vector3 AdjustCameraPosition(Vector3 desiredPosition)
    {
        RaycastHit hit;

        if (Physics.Raycast(player.position + Vector3.up * verticalOffset, 
                            (desiredPosition - (player.position + Vector3.up * verticalOffset)).normalized, 
                            out hit, 
                            playerDistance, 
                            environmentMask))
        {
            return hit.point + hit.normal * 0.1f;
        }

        return desiredPosition;
    }
}