using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5f;
    [Header("Configuración de Salto")]
    public float jumpForce = 7f; // Fuerza vertical del salto
    public Transform groundCheck; // Objeto vacío para verificar el suelo
    public LayerMask groundLayer; // Layer del suelo
    public float groundDistance = 0.4f; // Distancia de chequeo del suelo (radio de la esfera)

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded; // Indica si el jugador está en el suelo

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Detección de Suelo (Corre antes de la lógica de salto)
        CheckIfGrounded();

        // 2. Lógica de Salto
        if (Input.GetButtonDown("Jump") && isGrounded) // 'Jump' es la entrada por defecto (tecla Espacio)
        {
            Jump();
        }

        // Leer input (WASD)
        float moveX = Input.GetAxisRaw("Horizontal"); // A(-1) / D(+1)
        float moveZ = Input.GetAxisRaw("Vertical");    // S(-1) / W(+1)

        // Crear vector de movimiento
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        Quaternion cameraRotation = Camera.main.transform.rotation;

        cameraRotation.x = 0;
        cameraRotation.z = 0;
        cameraRotation = cameraRotation.normalized;
        // Si hay movimiento
        if (movement.magnitude > 0)
        {
            Vector3 rotatedMovement = cameraRotation * movement;
            // Rota el PlayerRoot para que mire en la dirección del movimiento (sin inclinación)
            transform.forward = rotatedMovement;

            // Mueve al jugador usando el nuevo vector rotado
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            // Nota: Aquí podrías querer usar un float para la velocidad en lugar de un bool
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }

            // Rotar hacia la dirección de movimiento
            transform.forward = movement;

            // Mover con Rigidbody (uso de FixedUpdate es más recomendable para Rigidbody)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        else
        {
            // Detener animación si no hay input
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void CheckIfGrounded()
    {
        // Usa una SphereCast o un OverlapSphere para detectar si el 'groundCheck' toca el suelo.
        // SphereCast es más robusto para verificar si se está "en el aire".
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // Opcional: Si tienes animaciones, puedes enviar el estado de 'isGrounded' al Animator
        // if (animator != null) { animator.SetBool("IsGrounded", isGrounded); }
    }

    private void Jump()
    {
        // Limpia la velocidad vertical existente (para asegurar un salto consistente)
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Aplica la fuerza de salto
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Opcional: Activar animación de salto
        // if (animator != null) { animator.SetTrigger("Jump"); }
    }
}
