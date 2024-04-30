using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public List<EnemyDetails> enemyDetailsList = new List<EnemyDetails>();
    public float spawnWeight = 1.0f;
    
    private EnemySpawnManager spawnManager; // Reference to the EnemySpawnManager
    [System.Serializable]
    public struct EnemyDetails
    {
        public GameObject enemyPrefab;
        public float health;
        public float damage;
        public float movementSpeed;
        //public string enemyTag; // Add the enemyTag property
        // Add other enemy properties as needed (e.g., attack range, attack rate)
    }
    void Start()
    {
        // Find the EnemySpawnManager in the scene (assuming there's only one)
        spawnManager = FindObjectOfType<EnemySpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("EnemyData: Could not find EnemySpawnManager in scene!");
        }
        else
        {
            // Register with the EnemySpawnManager (assuming you have a RegisterEnemyData method)
            spawnManager.RegisterEnemyData(this);
        }
    }


    void OnDestroy()
    {
        // If the EnemyData gets destroyed, unregister itself from the manager
        if (spawnManager != null)
        {
            spawnManager.UnregisterEnemyData(this);
        }
    }
}