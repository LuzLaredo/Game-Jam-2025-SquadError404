using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int enemyCount = 5;
    public float spawnInterval = 2f;

    private float nextSpawnTime;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        nextSpawnTime = Time.time;
    }

    void Update()
    {
        // Verifica si el número de enemigos activos es menor que el límite
        // Y si ya ha pasado el tiempo para la próxima generación
        if (activeEnemies.Count < enemyCount && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(newEnemy);

            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.SetPatrolPoints(patrolPoints);
                enemyAI.GetComponent<Health>().OnDie.AddListener(() => OnEnemyDeath(newEnemy));
            }
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
