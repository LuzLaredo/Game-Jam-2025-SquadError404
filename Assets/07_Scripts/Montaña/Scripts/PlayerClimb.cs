using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{

    // ... (Variables públicas sin cambios)
    public float climbSpeed = 5f;
    public float climbDistance = 1.5f;
    public LayerMask climbableLayer;
    public Transform raycastOrigin;
    public float climbJumpForce = 5f;

    private Rigidbody rb;
    private PlayerMovements playerMovement; // Usé 'PlayerMovements' según tu script anterior
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Asegúrate de que el nombre del script coincida con el que usas: 'PlayerMovements'
        playerMovement = GetComponent<PlayerMovements>();
    }

    void Update()
    {
        // ... (Tu lógica de detección de pared y entrada se mantiene)
        if (Physics.Raycast(raycastOrigin.position, transform.forward, out RaycastHit hit, climbDistance, climbableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
            {
                // Pasamos la información de la pared al StartClimb
                StartClimb(hit.point, hit.normal);
            }
        }

        // ... (Lógica de escalada activa y soltar pared se mantiene)
        if (isClimbing)
        {
            ClimbMovement();

            if (Input.GetKeyDown(KeyCode.R)) // Soltar la pared
            {
                StopClimb();
            }
        }
    }

    void StartClimb(Vector3 hitPoint, Vector3 hitNormal)
    {
        isClimbing = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // 1. Posicionamiento: Ajusta al jugador a la pared
        transform.position = hitPoint + hitNormal * 0.5f;

        // 2. Rotación: Rota al jugador para que mire en la dirección opuesta a la pared
        // Esto es crucial: la dirección 'transform.forward' del jugador ahora es perpendicular a la pared.
        transform.forward = -hitNormal;
    }

    void StopClimb()
    {
        isClimbing = false;
        rb.useGravity = true;

        // Vuelve a activar el script de movimiento
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    void ClimbMovement()
    {
        // Obtener inputs (W/S para Vertical, A/D para Horizontal)
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // 1. Calcular la velocidad vertical (Arriba/Abajo)
        // Usamos el eje UP del jugador (transform.up), que ahora es paralelo a la pared.
        Vector3 verticalClimb = transform.up * verticalInput * climbSpeed;

        // 2. Calcular la velocidad horizontal (Izquierda/Derecha)
        // Usamos el eje RIGHT del jugador (transform.right), que es perpendicular a la pared.
        Vector3 horizontalClimb = transform.right * horizontalInput * climbSpeed;

        // 3. Establecer la velocidad total de escalada
        Vector3 finalVelocity = verticalClimb + horizontalClimb;

        // Si hay algún input, aplica la velocidad
        if (verticalInput != 0 || horizontalInput != 0)
        {
            rb.velocity = finalVelocity;
        }
        else
        {
            // Si no hay input, el Rigidbody se detiene en la pared
            rb.velocity = Vector3.zero;
        }
    }
}
