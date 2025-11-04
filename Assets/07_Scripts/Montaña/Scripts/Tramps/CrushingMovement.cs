using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingMovement : MonoBehaviour
{
    // Define el rango de movimiento en el eje X.
    public float movementRange = 3f;
    // Define la velocidad del movimiento.
    public float speed = 1f;

    // Si es positivo, se moverá de 0 a X. Si es negativo, de 0 a -X.
    public bool movePositiveX = true;

    private Vector3 initialPosition;
    private float directionMultiplier;

    void Start()
    {
        initialPosition = transform.position;
        // Asigna 1 si se mueve en X positivo, -1 si es negativo.
        directionMultiplier = movePositiveX ? 1f : -1f;
    }

    void Update()
    {
        // Calcula la nueva posición usando una función seno.
        float xOffset = Mathf.Sin(Time.time * speed) * movementRange * directionMultiplier;
        Vector3 newPosition = initialPosition + new Vector3(xOffset, 0, 0);
        transform.position = newPosition;
    }
}
