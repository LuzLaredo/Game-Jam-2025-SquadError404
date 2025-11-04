using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // El objeto Transform a seguir (el jugador)

    [Header("Configuración de Distancia")]
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Posición relativa de la cámara (Y, Z)
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento

    // Usamos LateUpdate para asegurar que el jugador se mueva PRIMERO
    void LateUpdate()
    {
        // 1. Calcular la posición deseada de la cámara
        // La posición deseada e    s la posición del target (jugador) MÁS el offset
        Vector3 desiredPosition = target.position + offset;

        // 2. Suavizar la transición a la posición deseada
        // Usamos Lerp para un movimiento suave, evitando que la cámara se "sacuda"
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 50f);

        // 3. Aplicar la posición
        transform.position = smoothedPosition;

        // 4. Asegurar que la cámara siempre mire al jugador (opcional, pero recomendado)
        transform.LookAt(target);
    }
}
