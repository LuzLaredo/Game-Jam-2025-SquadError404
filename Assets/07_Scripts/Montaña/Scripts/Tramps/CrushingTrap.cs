using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingTrap : MonoBehaviour
{
    public float damage = 50f;

    void OnCollisionEnter(Collision collision)
    {
        // Si el objeto que colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Opcional: Podrías añadir lógica aquí para comprobar
            // si el jugador está realmente "aplastado" (por ejemplo,
            // si la velocidad de las paredes es alta o si está en el medio).

            // Simplemente causamos daño al colisionar.
            // En un sistema de vida, se llamaría a un método como 'TakeDamage'.
            Debug.Log("¡El jugador ha sido aplastado! Recibió " + damage + " de daño.");

            // Aquí puedes llamar a una función para matar al jugador, por ejemplo.
            // Ejemplo: PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // if (playerHealth != null) { playerHealth.TakeDamage(damage); }
        }
    }
}
