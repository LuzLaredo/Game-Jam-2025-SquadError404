using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damage = 20f;
    private bool isActive = false;

    // Método para activar la trampa (los pinchos salen)
    public void ActivateTrap()
    {
        if (!isActive)
        {
            isActive = true;
            Debug.Log("Spike Trap Activated!");
            // Aquí podrías llamar a un método del script de movimiento
        }
    }

    // Método para desactivar la trampa (los pinchos se esconden)
    public void DeactivateTrap()
    {
        if (isActive)
        {
            isActive = false;
            Debug.Log("Spike Trap Deactivated!");
        }
    }

    // Detección de colisión con el jugador
    void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            // Aquí podrías llamar a un método para restar vida al jugador
            Debug.Log("Player ha recibido daño!");
        }
    }
}
