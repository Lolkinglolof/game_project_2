using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class WaveManager : MonoBehaviour
{

    
    public WaveData[] waves; // Array of WaveData objects defining the waves
    public GameObject[] enemySpawnPoints; // Array of GameObjects representing enemy spawn points
    //public float totalSpawnWeight = 1.0f; // Total spawn weight for weighted enemy selection
    public EnemyData[] enemies;
    private List<float> spawnProbabilities; // Calculated spawn probabilities based on weights
    private int currentWaveIndex = 0; // Tracks the currently running wave index (added)  // This line is added
    private int spawnedEnemies = 0; // Track spawned enemies for current wave (added)
    private int killCount = 0; // Track enemy kills for current wave (added) // This line is added
    //public int requiredKillsPerWave; // Public variable for required kills per wave (Option 1)
    public WaveChangedEvent waveChangedEvent; // Public event to signal wave change
    public static WaveManager instance; // Public static instance
    private static WaveManager _instance;
    private WaveData loadedWaveData; // Declare a variable to hold the instance
    public TextMeshProUGUI waveNumberText; // Reference to TextMesh Pro UI element
    public TextMeshProUGUI killCountText; // Reference to TextMesh Pro UI element for kill count
    
    private void Awake()
    {
        loadedWaveData = Resources.Load<WaveData>("WavesData"); // Assuming "WavesData" is an asset
        currentWaveIndex = 0; // Example initializing a variable

        if (waveNumberText != null)
        {
            waveNumberText.text = "Wave: " + (currentWaveIndex + 1); // Assuming indexing starts at 0
        }
        else
        {
            Debug.LogError("Wave number UI text not assigned!");
        }
    }
    public WaveData GetCurrentWaveData()
    {
        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Length)
        {
            return waves[currentWaveIndex];
        }
        else
        {
            Debug.LogError("WaveManager: GetCurrentWaveData called when there's no current wave!");
            return null;
        }
    }
    public IEnumerator SpawnWaveCoroutine(WaveData wave)
    {
        if (wave.enemySpawns.Count == 0)
        {
            Debug.LogError("WaveManager: Wave " + wave.waveName + " has no enemy data!");
            yield break;
        }

        int spawnedEnemies = 0;
        while (spawnedEnemies < wave.totalEnemies || wave.totalEnemies == 0)
        {
            EnemyData chosenEnemyData = ChooseRandomEnemyData(wave.enemySpawns); // Choose an enemy from the wave's data

            int chosenSpawnPointIndex = Random.Range(0, enemySpawnPoints.Length);

            yield return new WaitForSeconds(wave.spawnInterval);
            Instantiate(chosenEnemyData.enemyDetailsList[0].enemyPrefab, enemySpawnPoints[chosenSpawnPointIndex].transform.position, enemySpawnPoints[chosenSpawnPointIndex].transform.rotation);
            spawnedEnemies++;
        }

        // Handle wave completion (you can add logic here, e.g., display wave complete message, start next wave)
        Debug.Log("Wave " + wave.waveName + " completed!");
    }

    private EnemyData ChooseRandomEnemyData(List<EnemyData> enemyData)
    {
        // Implement weighted random selection logic here (assuming you copied this method from EnemySpawnManager)
        float randomValue = Random.value;
        float cumulativeWeight = 0.0f;

        for (int i = 0; i < spawnProbabilities.Count; i++)
        {
            cumulativeWeight += spawnProbabilities[i];
            if (randomValue <= cumulativeWeight)
            {
                return enemyData[i];
            }
        }

        // If no enemy is chosen due to rounding errors, return the last element
        return enemyData[enemyData.Count - 1];
    }

    public void CalculateSpawnProbabilities() // Made public (Option 2, less preferred)
    {
        spawnProbabilities = new List<float>();

        float currentWeight = 0.0f;
        foreach (EnemyData enemy in enemies)
        {
            // Ensure weighting is positive and does not exceed the total
            float weight = enemy.spawnWeight; // Use enemy's individual spawnWeight directly
            currentWeight += weight;
            spawnProbabilities.Add(weight);
        }

        // Normalize probabilities to ensure they sum to 1
        if (currentWeight > Mathf.Epsilon)
        {
            for (int i = 0; i < spawnProbabilities.Count; i++)
            {
                spawnProbabilities[i] /= currentWeight;
            }
        }
    }

    void Start()
    {
        killCountText.text = "Kills: " + killCount;
        
        CalculateSpawnProbabilities(); // Now uncommented if using Option 2

    }

    public void StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            killCount = 0; // Reset kill count for the new wave
            Debug.Log("All waves completed!");
            // Handle all waves completed (e.g., victory screen)
            return;
        }

        StartCoroutine(SpawnWaveCoroutine(waves[currentWaveIndex]));
        // Increment wave index after starting the coroutine (modified)
        currentWaveIndex++;

        waveChangedEvent.Invoke(currentWaveIndex); // Pass current wave index
    }
    public class WaveChangedEvent : UnityEngine.Events.UnityEvent<int> 
    { 

    } // Pass current wave index
    public void OnEnemySpawned(EnemyFarmer1 enemy)
    {
        spawnedEnemies++;
    }

    public void OnEnemyKilled(EnemyFarmer1 enemy)
    {
        if (enemy.transform.position.y < -5.0f) // Enemy fell out of bounds?
        {
            // Don't count kill
            return;
        }
        Debug.LogError("Enemy Killed!"); // Add this line
        killCount++;
        CheckWaveCompletion();
    
        // Update kill count UI element
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + killCount;
            Debug.LogError("Kill count updated: " + killCount); // Add this line
        }
    }
    private void CheckWaveCompletion()
    {
        WaveData currentWave = GetCurrentWaveData();
        // Check for completion based on either total enemies or required kills
        if (currentWave.totalEnemies > 0 && spawnedEnemies >= currentWave.totalEnemies)
        {
            // Handle wave completion based on total enemies
            Debug.Log("Wave " + currentWave.waveName + " completed (total enemies reached)");
            StartNextWave();
        }
        else if (currentWave.requiredKills > 0 && killCount >= currentWave.requiredKills)
        {
            // Handle wave completion based on required kills
            Debug.Log("Wave " + currentWave.waveName + " completed (required kills reached)");
            StartNextWave();
        }
    }
}