using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.Playables;
public class CurrencyManger : MonoBehaviour
{
    [SerializeField] public float currencyCoins;
    [SerializeField] private TextMeshProUGUI currencyDisplay;


    [SerializeField] private string currencySaved ="$"; // used to save our currencyData. 



    // Start is called before the first frame update
    void Start()
    {
        currencyDisplay.text = currencyCoins+"$".ToString();
        currencySaved = currencyDisplay.text;
        LoadSavedCurrency(currencySaved); //load saved currency to current game.


    }

    // Update is called once per frame
    void Update()
    {

        currencySaved = currencyDisplay.text;

        if (Input.GetKeyDown(KeyCode.P))
        {
            currencyCoins++;
            CurrencyDisplay();
            SavedCurrency(currencySaved); // calls the SavedCurrency to ensure the currency would be updated daily. 
        }
        CurrencyDisplay();
        SavedCurrency(currencySaved); // calls the SavedCurrency to ensure the currency would be updated daily. 

    }
    public void CurrencyDisplay()
    {

        currencyDisplay.text = currencyCoins + "$".ToString(); //display the amount of currency the player have collected. 

        
    }

    public void SavedCurrency(string currency) //saves the amount of Curreny the Player has collected. 
    {
        string currencySaved = currency;

        PlayerPrefs.SetString(currency,"$"); //saves the data
        PlayerPrefs.SetFloat("$", currencyCoins); // save the amount of money.
        
    }
    public void LoadSavedCurrency(string currency) //loads the SavedCurrency to the game. 
    {
        string currencySaved = currency;


        currency = PlayerPrefs.GetString("$");//loads the saved currency when game loads the secne. 
        currencyCoins = PlayerPrefs.GetFloat("$");
    }
    public void DeliteSavedCurrency() //removes players currency.
    {
        PlayerPrefs.DeleteKey("$"); // removes the currency. 

    }
}
