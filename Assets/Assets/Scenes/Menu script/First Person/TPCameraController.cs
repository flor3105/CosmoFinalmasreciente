using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraController : MonoBehaviour
{
    public float sensitivity = 100f; // Sensibilidad del mouse
    private float xRotation;
    private float yRotation;
    public float minVerticalAngle = -30f;  // cuánto puede mirar hacia arriba
    public float maxVerticalAngle = 60f;   // cuánto puede mirar hacia abajo
    public Transform player; // Referencia al jugador

    public float verticalOffset = 2f; // Altura de la cámara sobre el jugador
    public float playerDistance = 4f; // Distancia de la cámara al jugador
    public LayerMask environmentMask; // Máscara de colisión para las paredes u objetos

    // Start is called before the first frame update
    void Start()
{
    Vector3 angles = transform.eulerAngles;

    xRotation = angles.x;

    if (xRotation > 180f)
        xRotation -= 360f;

    yRotation = angles.y;
}

    // Update is called once per frame
    void Update()
    {
        // Control del movimiento de la cámara con el ratón
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Clampear la rotación vertical para que no pase los límites
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        // Aplicar la rotación a la cámara
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = player.position + Vector3.up * verticalOffset - transform.forward * playerDistance;

        // Ajustar la posición de la cámara si colisiona con un objeto
        Vector3 adjustedPosition = AdjustCameraPosition(desiredPosition);

        // Aplicar la posición ajustada
        transform.position = adjustedPosition;
    }

    // Ajusta la posición de la cámara si detecta colisión con el entorno
    private Vector3 AdjustCameraPosition(Vector3 desiredPosition)
    {
        RaycastHit hit;

        // Lanzar un rayo desde el jugador hacia la posición deseada de la cámara
        if (Physics.Raycast(player.position + Vector3.up * verticalOffset, 
                            (desiredPosition - (player.position + Vector3.up * verticalOffset)).normalized, 
                            out hit, 
                            playerDistance, 
                            environmentMask))
        {
            // Si hay colisión, ajustar la posición de la cámara al punto de impacto
            return hit.point + hit.normal * 0.1f; // Retroceder un poco para evitar solapamientos
        }

        // Si no hay colisión, usar la posición deseada
        return desiredPosition;
    }
}