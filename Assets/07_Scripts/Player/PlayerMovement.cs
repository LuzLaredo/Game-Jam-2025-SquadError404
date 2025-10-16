using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Leer input (WASD)
        float moveX = Input.GetAxisRaw("Horizontal"); // A(-1) / D(+1)
        float moveZ = Input.GetAxisRaw("Vertical");   // S(-1) / W(+1)

        // Crear vector de movimiento
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Si hay movimiento
        if (movement.magnitude > 0)
        {
            // Activar animación de caminar
            animator.SetBool("isWalking", true);

            // Rotar hacia la dirección de movimiento
            transform.forward = movement;

            // Mover con Rigidbody
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        else
        {
            // Detener animación si no hay input
            animator.SetBool("isWalking", false);
        }
    }
}
