using UnityEngine;

public class EnemyStats : ScriptableObject
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnWeight = 1.0f; // Spawn probability weight for this enemy type
}