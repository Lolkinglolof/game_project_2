using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyData> registeredEnemyDataList = new List<EnemyData>(); // List to store registered enemy data
    public GameObject[] enemySpawnPoints;
    private List<float> spawnProbabilities; 
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
        
        CalculateSpawnProbabilities();
        StartCoroutine(SpawnEnemyCoroutine()); // Start the coroutine for spawning
    }

    public void SpawnEnemy()
    {
        if (registeredEnemyDataList.Count == 0 || enemySpawnPoints.Length == 0) 
        {
            Debug.LogError("EnemySpawnManager: Missing enemy or spawn point prefabs!");
            return;
        }

        
        WaveData currentWave = waveManager.GetCurrentWaveData();
                                                                 
        List<EnemyData> waveEnemyData = currentWave.enemySpawns; 
        int chosenEnemyIndex = ChooseRandomEnemyIndex(waveEnemyData); 

        EnemyData chosenEnemyData = registeredEnemyDataList[chosenEnemyIndex]; 

        int chosenSpawnPointIndex = Random.Range(0, enemySpawnPoints.Length);


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
 
            float weight = Mathf.Clamp(enemy.spawnWeight, 0.0f, Mathf.Infinity); 
            currentWeight += weight;
            spawnProbabilities.Add(weight);
        }

       
        if (currentWeight <= Mathf.Epsilon)
        {
            Debug.LogError("EnemySpawnManager: Sum of enemy spawn weights is zero! Please adjust weights.");
          
        }
        else
        {
     
            foreach (EnemyData enemy in registeredEnemyData)
            {
                float weight = Mathf.Clamp(enemy.spawnWeight, 0.0f, Mathf.Infinity);
                spawnProbabilities.Add(weight / currentWeight); 
            }
        }
    }

    private int ChooseRandomEnemyIndex(List<EnemyData> enemyDataList) 
    {
        float randomValue = Random.value;
        float cumulativeWeight = 0.0f;

        for (int i = 0; i < enemyDataList.Count;  
             i++)
        {
            cumulativeWeight += enemyDataList[i].spawnWeight;  

            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        
        return enemyDataList.Count - 1;
    }

    public List<EnemyData> registeredEnemyData = new List<EnemyData>(); 

    public void RegisterEnemyData(EnemyData enemyData)
    {
        registeredEnemyData.Add(enemyData); 
    }

    public void UnregisterEnemyData(EnemyData enemyData)
    {
        registeredEnemyData.Remove(enemyData); 
    }
}