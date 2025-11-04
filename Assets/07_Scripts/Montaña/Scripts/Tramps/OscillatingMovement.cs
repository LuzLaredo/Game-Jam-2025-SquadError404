using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingMovement : MonoBehaviour
{
    public float movementRange = 2f;
    public float speed = 1f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calcula la nueva posición usando una función seno
        float zOffset = Mathf.Sin(Time.time * speed) * movementRange;
        Vector3 newPosition = initialPosition + new Vector3(0, 0, zOffset);
        transform.position = newPosition;
    }
}
