using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 2.0f;
    public Vector3 spawnAreaSize = new Vector3(3, 1, 3);

    // Nueva variable para la velocidad inicial de los objetos que caen
    public float fallSpeed = 5f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Calcula una posición aleatoria dentro del área de generación
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Genera el objeto en la posición aleatoria
            GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            // Aplica la velocidad y movimiento inicial
            Rigidbody rb = newObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplica una velocidad inicial hacia abajo
                rb.velocity = -transform.up * fallSpeed;
                // Opcional: Para una trampa más avanzada, puedes añadir movimiento lateral
                // rb.velocity = new Vector3(Random.Range(-1f, 1f), -1f, Random.Range(-1f, 1f)) * fallSpeed;
            }
        }
    }

    // Opcional: Para visualizar el área de generación en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}
