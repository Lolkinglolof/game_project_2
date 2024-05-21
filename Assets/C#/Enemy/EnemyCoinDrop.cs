using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoinDrop : MonoBehaviour
{
    [SerializeField] public CurrencyManger currencyManger;
    [SerializeField] private float addCurrency1, addCurrency2; // add the amount of currency
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null) //checks if the enemy is dead or alive.
        {
            Random.Range(addCurrency1, addCurrency2 += currencyManger.currencyCoins); //adds the random amount of currency to the currencyManager. 
            
            
        }
        else
        {
            Debug.Log("EnemyCoinDrop not detected");
        }
    }
}
