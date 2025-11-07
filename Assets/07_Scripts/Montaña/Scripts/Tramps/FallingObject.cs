using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    // Tiempo de vida del objeto en segundos antes de que se destruya solo.
    public float lifetime = 5f;

    void Start()
    {
        // Destruye el objeto automáticamente después de 'lifetime' segundos.
        Destroy(this.gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Puedes agregar una etiqueta para el suelo o los objetos con los que debe colisionar
        // Por ejemplo, si colisiona con el "Player" o el "Ground"
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Trampa") || collision.gameObject.CompareTag("Enemy"))
        {
            // Destruye el objeto inmediatamente si colisiona con el Player o el suelo.
            Destroy(this.gameObject);
        }
    }
}
