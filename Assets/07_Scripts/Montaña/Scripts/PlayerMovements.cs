using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f; // Fuerza de salto normal
    public float mountainJumpForce = 15f; // Nueva variable para la fuerza de salto en la montaña
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask mountainLayer; // Nueva variable para la capa de la montaña

    private Rigidbody rb;
    private bool isGrounded;
    private bool onMountainTop; // Nueva variable de estado

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Detección de suelo normal
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        // 2. Detección de la capa "PuntaMontaña"
        onMountainTop = Physics.CheckSphere(groundCheck.position, 0.2f, mountainLayer);

        // Obtener la entrada del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calcula el ángulo de rotación
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Crea la rotación objetivo
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Rota el personaje de manera fluida
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            // Aplica el movimiento
            rb.velocity = new Vector3(transform.forward.x * moveSpeed, rb.velocity.y, transform.forward.z * moveSpeed);
        }
        else
        {
            // Detiene el movimiento horizontal
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // 3. Lógica de salto condicional
        if (Input.GetButtonDown("Jump") && (isGrounded || onMountainTop))
        {
            float currentJumpForce = jumpForce;
            // Si el jugador está sobre la capa de la montaña, usa la fuerza del salto más potente
            if (onMountainTop)
            {
                currentJumpForce = mountainJumpForce;
            }

            rb.velocity = new Vector3(rb.velocity.x, currentJumpForce, rb.velocity.z);
        }
    }
}
