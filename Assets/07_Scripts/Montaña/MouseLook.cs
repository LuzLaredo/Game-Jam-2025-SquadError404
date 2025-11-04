using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Objetivos")]
    public Transform target; // El jugador (PlayerRoot)

    [Header("Rotación de Mouse")]
    public float mouseSensitivity = 100f;
    public float verticalRotationLimit = 80f;

    [Header("Seguimiento de Posición")]
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Distancia desde el jugador
    public float smoothSpeed = 0.125f; // Suavizado del movimiento

    private float xRotation = 0f; // Rotación vertical acumulada (Inclinación)
    private float yRotation = 0f; // Rotación horizontal acumulada (Giro)

    void Start()
    {
        // Bloquea el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Inicializa la rotación horizontal con el giro actual de la cámara
        yRotation = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // 1. Lógica de ROTACIÓN (Vista del Mouse)
        HandleRotationInput();

        // 2. Lógica de SEGUIMIENTO (Posición)
        HandlePositionFollow();
    }

    private void HandleRotationInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Giro Horizontal (Eje Y)
        yRotation += mouseX;

        // Inclinación Vertical (Eje X)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);

        // Aplicamos la rotación combinada a la cámara
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void HandlePositionFollow()
    {
        if (target == null) return;

        // La posición deseada es la posición del target MÁS el offset
        // PERO rotado por el giro horizontal de la cámara (yRotation)

        // Creamos un Quaternion solo con el giro horizontal de la cámara
        Quaternion planarRotation = Quaternion.Euler(0f, yRotation, 0f);

        // Aplicamos la rotación al offset para que la cámara siempre se quede 'detrás' del jugador
        Vector3 rotatedOffset = planarRotation * offset;

        // Posición deseada: Target + Offset Rotado
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Suavizado de la posición
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 50f);

        // Aplicar la posición
        transform.position = smoothedPosition;

        // Aseguramos que la cámara esté orientada al jugador (opcional, ya lo hace transform.rotation)
        // transform.LookAt(target); 
    }
}
