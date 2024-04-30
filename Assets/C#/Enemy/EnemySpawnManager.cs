using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyData> registeredEnemyDataList = new List<EnemyData>(); // List to store registered enemy data
    //public EnemyData[] enemies;
    public GameObject[] enemySpawnPoints;
    //public float totalSpawnWeight = 1.0f;
    //public float interval = 10; // The amount of time before spawning new enemies.

    private List<float> spawnProbabilities; // Calculated spawn probabilities based on weights
    private WaveManager waveManager;
    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            WaveData currentWave = waveManager.GetCurrentWaveData();
            yield return new WaitForSeconds(currentWave.spawnInterval);
            SpawnEnemy();
        }
    }

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        // ... (rest of your code)
        CalculateSpawnProbabilities();
        StartCoroutine(SpawnEnemyCoroutine()); // Start the coroutine for spawning
    }

    public void SpawnEnemy()
    {
        if (registeredEnemyDataList.Count == 0 || enemySpawnPoints.Length == 0) // Use Count instead of Length
        {
            Debug.LogError("EnemySpawnManager: Missing enemy or spawn point prefabs!");
            return;
        }

        // Get the current wave data (assuming you have a reference to the WaveManager)
        WaveData currentWave = waveManager.GetCurrentWaveData(); // Get wave data from WaveManager
                                                                 // Choose a random enemy from the current wave's enemy data
        List<EnemyData> waveEnemyData = currentWave.enemySpawns; // Get wave's enemy data
        int chosenEnemyIndex = ChooseRandomEnemyIndex(waveEnemyData); // Choose from wave data

        EnemyData chosenEnemyData = registeredEnemyDataList[chosenEnemyIndex]; // Access enemy data using the chosen index
        // Choose a random spawn point index
        int chosenSpawnPointIndex = Random.Range(0, enemySpawnPoints.Length);

        // Instantiate the chosen enemy prefab at the chosen spawn point
        GameObject spawnedEnemy = Instantiate(registeredEnemyDataList[chosenEnemyIndex].enemyDetailsList[0].enemyPrefab,
                                       enemySpawnPoints[chosenSpawnPointIndex].transform.position,
                                       enemySpawnPoints[chosenSpawnPointIndex].transform.rotation);

    }

    public void CalculateSpawnProbabilities()
    {
        spawnProbabilities = new List<float>();

        float currentWeight = 0.0f;
        foreach (EnemyData enemy in registeredEnemyData)
        {
            // Ensure weighting is positive and does not exceed the total
            float weight = Mathf.Clamp(enemy.spawnWeight, 0.0f, Mathf.Infinity); // No upper bound for individual weights
            currentWeight += weight;
            spawnProbabilities.Add(weight);
        }

        // Normalize probabilities to ensure they sum to 1
        if (currentWeight <= Mathf.Epsilon)
        {
            Debug.LogError("EnemySpawnManager: Sum of enemy spawn weights is zero! Please adjust weights.");
            // You can choose to handle this error (e.g., assign default weights)
        }
        else
        {
            // If weights are valid, calculate probabilities
            foreach (EnemyData enemy in registeredEnemyData)
            {
                float weight = Mathf.Clamp(enemy.spawnWeight, 0.0f, Mathf.Infinity);
                spawnProbabilities.Add(weight / currentWeight); // Normalize weight based on total
            }
        }
    }

    private int ChooseRandomEnemyIndex(List<EnemyData> enemyDataList) // Add parameter
    {
        float randomValue = Random.value;
        float cumulativeWeight = 0.0f;

        for (int i = 0; i < enemyDataList.Count;  // Use enemyDataList.Count
             i++)
        {
            cumulativeWeight += enemyDataList[i].spawnWeight;  // Use enemyDataList[i].spawnWeight

            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        // If no enemy is chosen due to rounding errors, return the last index
        return enemyDataList.Count - 1; // Use enemyDataList.Count
    }

    public List<EnemyData> registeredEnemyData = new List<EnemyData>(); // List to store registered enemy data

    public void RegisterEnemyData(EnemyData enemyData)
    {
        registeredEnemyData.Add(enemyData); // Use the renamed variable
    }

    public void UnregisterEnemyData(EnemyData enemyData)
    {
        registeredEnemyData.Remove(enemyData); // Remove the enemy data from the list
    }
}