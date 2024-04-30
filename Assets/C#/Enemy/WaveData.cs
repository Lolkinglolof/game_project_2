using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyWaveData", menuName = "WaveData")]
public class WaveData : ScriptableObject
{
    public string waveName; // Name of the wave for identification
    public List<EnemyData> enemySpawns; // Now private // List of enemy data for this wave
    public float spawnInterval; // Time interval between enemy spawns within a wave
    public int totalEnemies; // Total number of enemies to spawn in this wave (optional)
    public int requiredKills; // Required kills for this wave (added)

 

    public void UseEnemyData(EnemyManager enemyManager)
    {
       EnemyData enemyData = enemyManager.GetComponent<EnemyData>();
       // Use the enemyData object here
    }
    public int GetNumEnemies()
    {
        return enemySpawns.Count; // Public method to access data
    }
}
