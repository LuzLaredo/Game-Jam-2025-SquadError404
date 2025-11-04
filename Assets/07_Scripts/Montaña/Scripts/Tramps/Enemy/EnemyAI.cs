using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    // VARIABLES DE PATRULLAJE
    public Transform[] patrolPoints;
    public float patrolSpeed = 3.5f;

    private NavMeshAgent agent;
    private int currentPatrolIndex;

    void Start()
    {
        // Obtiene el componente NavMeshAgent.
        agent = GetComponent<NavMeshAgent>();

        // Si hay puntos de patrulla asignados, empieza a moverse.
        if (patrolPoints.Length > 0)
        {
            GoToNextPatrolPoint();
        }
    }

    void Update()
    {
        // Si el agente ha llegado a su destino y no está calculando una nueva ruta,
        // elige el siguiente punto de patrulla.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    public void SetPatrolPoints(Transform[] points)
    {
        patrolPoints = points;
        // Inicia el movimiento al primer punto
        if (patrolPoints.Length > 0)
        {
            GoToNextPatrolPoint();
        }
    }
    void GoToNextPatrolPoint()
    {
        // Se asegura de que haya puntos de patrulla para evitar errores.
        if (patrolPoints.Length == 0) return;

        // Establece el destino al siguiente punto de patrulla.
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // Avanza al siguiente punto de la lista (o vuelve al inicio).
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

}
