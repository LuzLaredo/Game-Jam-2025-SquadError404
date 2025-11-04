using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    // Evento que se activa cuando la entidad muere
    public UnityEvent OnDie;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " received " + damage + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie.Invoke(); // Notifica a otros scripts que la entidad ha muerto
        Debug.Log(gameObject.name + " has been defeated.");

        // El objeto se destruirá a sí mismo
        Destroy(gameObject);
    }
}
