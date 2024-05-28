using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public List<EnemyDetails> enemyDetailsList = new List<EnemyDetails>();
    public float spawnWeight = 1.0f;
    
    private EnemySpawnManager spawnManager; 
    [System.Serializable]
    public struct EnemyDetails
    {
        public GameObject enemyPrefab;
        public float health;
        public float damage;
        public float movementSpeed;
      
    }
    void Start()
    {
        
        spawnManager = FindObjectOfType<EnemySpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("EnemyData: Could not find EnemySpawnManager in scene!");
        }
        else
        {
          
            spawnManager.RegisterEnemyData(this);
        }
    }


    void OnDestroy()
    {

        if (spawnManager != null)
        {
            spawnManager.UnregisterEnemyData(this);
        }
    }
}