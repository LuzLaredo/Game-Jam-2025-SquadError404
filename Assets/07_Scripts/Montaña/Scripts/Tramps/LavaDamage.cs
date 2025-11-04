using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public float damageInterval = 1.0f; // Daño cada 1 segundo. Puedes ajustarlo en el Inspector
    public float damageAmount = 2.0f; // Daño por cada tick

    private Coroutine damageCoroutine;

    // Se activa cuando otro objeto entra en la zona de lava (el trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia la coroutine para hacer daño cada cierto tiempo
            damageCoroutine = StartCoroutine(ApplyDamage(other.gameObject));
            Debug.Log("Player entró en la lava. ¡Recibiendo daño!");
        }
    }

    // Se activa cuando otro objeto sale de la zona de lava
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Detiene la coroutine para que el daño no continúe
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                Debug.Log("Player salió de la lava. ¡El daño se detiene!");
            }
        }
    }

    // Coroutine para aplicar daño continuo
    IEnumerator ApplyDamage(GameObject player)
    {
        while (true) // Bucle infinito
        {
            // Espera el tiempo definido antes de hacer el siguiente tick de daño
            yield return new WaitForSeconds(damageInterval);

            // Aquí se aplicaría el daño al jugador
            Debug.Log("Player recibió " + damageAmount + " de daño por la lava.");

            // Ejemplo de cómo llamar a un método de salud del jugador (requiere un PlayerHealth.cs)
            // PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damageAmount);
            // }
        }
    }
}
