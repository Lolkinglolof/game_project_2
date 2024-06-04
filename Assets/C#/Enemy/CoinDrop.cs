using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField] public float minCoinDrop = 0.5f; // Minimum number of coins dropped
    [SerializeField] public float maxCoinDrop = 10.0f; // Maximum number of coins dropped
    [SerializeField] public float dropChance = 0.5f; // Chance of dropping coins (50% in this case)
   
    // Method to handle coin drops when an enemy is killed
    public void DropCoins()
    {
        Debug.Log("DropCoins method called");
        // Check if the drop chance is successful
        if (Random.value <= dropChance)
        {
            // Generate a random number of coins within the specified range
            float coinsDropped = Random.Range(minCoinDrop, maxCoinDrop);
            Debug.Log("Coins dropped: " + coinsDropped);

            // Increase the player's coins by the dropped amount
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.CollectCoin(coinsDropped);
            }
            else
            {
                Debug.LogError("CoinManager not found in the scene.");
            }
        }
    }
}