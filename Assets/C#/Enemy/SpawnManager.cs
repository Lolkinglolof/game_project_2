using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // List of possible enemy prefabs
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private float minSpawnTime = 5f; // Minimum time between spawns
    [SerializeField] private float maxSpawnTime = 10f; // Maximum time between spawns

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length); // Choose a random spawn point
        int enemyIndex = Random.Range(0, enemyPrefabs.Count); // Choose a random enemy prefab

        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}