using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for accessing UI components

public class CoinManager : MonoBehaviour
{
    [SerializeField] private const string COIN_KEY = "PlayerCoins"; // Key to save and load the number of coins

    [SerializeField] public float playerCoins = 0; // Number of coins collected by the player
    [SerializeField] public float playerStarterCoins = 0; // Number of coins collected by the player
    [SerializeField] public TextMeshProUGUI coinsText; // Reference to the UI Text component to display the number of coins

    // Start is called before the first frame update
    void Start()
    {
        
        playerCoins = playerStarterCoins; //Players will always max start with 300 Coins



        //LoadPlayerCoins(); // Load the number of coins when the game starts
        UpdateCoinsText(); // Update the UI Text with the current number of coins
    }

    // Method to increase the number of coins
    public void CollectCoin(float coinsDropped)
    {
        playerCoins += coinsDropped; // Increase the number of coins
        SavePlayerCoins(); // Save the updated number of coins
        UpdateCoinsText(); // Update the UI Text with the current number of coins
    }

    // Method to save the number of coins using PlayerPrefs
    public void SavePlayerCoins()
    {
        PlayerPrefs.SetFloat(COIN_KEY, playerCoins); // Save the current number of coins
        PlayerPrefs.Save(); // Save the changes to PlayerPrefs
    }

    // Method to load the number of coins from PlayerPrefs
    private void LoadPlayerCoins()
    {
        if (PlayerPrefs.HasKey(COIN_KEY))
        {
            playerCoins = PlayerPrefs.GetFloat(COIN_KEY); // Use GetFloat to load the correct value
        }
    }

    // Method to update the UI Text with the current number of coins
    public void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = playerCoins.ToString("F3") + "$"; // Update the text with the current number of coins
        }
    }

    // Method to reset the number of coins (for testing purposes)
    public void ResetPlayerCoins()
    {
        PlayerPrefs.DeleteKey(COIN_KEY); // Delete the saved number of coins
        playerCoins = 0; // Reset the number of coins in the game
        UpdateCoinsText(); // Update the UI Text with the current number of coins
    }
}