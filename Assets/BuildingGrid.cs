using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private string buildpick = "white";
    [SerializeField] public GameObject gridboxwhite;
    [SerializeField] public GameObject gridboxblue;
    [SerializeField] public GameObject coinsCollecter;
    [SerializeField] public GameObject starterbox;
    [SerializeField] public CoinManager coinManager; // Reference to the CoinManager component

    [SerializeField] public float itemCost = 10; // buying Wall, or something else.
    [SerializeField] public float itemCost2 = 50.6f; // buying cat fighter
    [SerializeField] public float itemCost3 = 156.5f; // buying catepult dirts!
    GameObject closestobject()
    {
        float closestdistance = 100;
        GameObject gameobjecttoreturn = null;
        Collider2D[] closestobjects = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach ( Collider2D objecttocheck in closestobjects)
        {
            if(objecttocheck.tag == "block")
            {
                if(Vector3.Distance(transform.position,objecttocheck.transform.position) < closestdistance)
                {
                
                    closestdistance = Vector3.Distance(transform.position, objecttocheck.transform.position);
                    gameobjecttoreturn = objecttocheck.gameObject;
                }
            }
            
        }
        return gameobjecttoreturn;
    }

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream:Assets/BuildingGrid.cs
      
=======
        Debug.Log("CoinManager reference: " + coinManager);
        if (gameObject.tag == "gridblock")
        {
            Instantiate(starterbox, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-0.5f), Quaternion.identity);
        }
>>>>>>> Stashed changes:Assets/grid.cs
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
<<<<<<< Updated upstream:Assets/BuildingGrid.cs
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            Debug.Log(hit.collider != null && hit.collider.gameObject.tag == "gridblock" && hit.collider.gameObject.tag == "colorboxwhite" && hit.collider.gameObject.tag == "colorboxblue" && hit.collider.gameObject.tag == "block");
            if (hit.collider == null)
                return;
            if (hit.collider.gameObject.tag == "gridblock")
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    if (buildpick == "white")
                    {
                        Debug.Log("white build");
                        Destroy(closestobject());
                        Instantiate(gridboxwhite, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);
=======
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
>>>>>>> Stashed changes:Assets/grid.cs

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "gridblock")
                {
                    if (hit.collider.gameObject.name == gameObject.name)
                    {
                        if (coinManager != null)
                        {
                            float playerCoins = coinManager.playerCoins;

                            // Deduct the cost of the item from the player's coins
                            float remainingCoins = playerCoins;

                            switch (buildpick)
                            {
                                case "white":
                                    remainingCoins -= itemCost;
                                    break;
                                case "blue":
                                    remainingCoins -= itemCost2;
                                    break;
                                case "collector":
                                    remainingCoins -= itemCost3;
                                    break;
                                default:
                                    Debug.LogError("Invalid build pick!");
                                    return;
                            }

                            // Check if the player has enough coins to make the purchase
                            if (remainingCoins >= 0)
                            {
                                // If the player has enough coins, update the player's coins and instantiate the item
                                coinManager.playerCoins = remainingCoins;

                                // Update UI text to reflect the remaining coins
                                coinManager.UpdateCoinsText();

                                // Handle purchasing and instantiate the item based on buildpick



                                if (buildpick == "white")
                                {
                                    Debug.Log("white build");
                                    Destroy(closestobject());
                                    Instantiate(gridboxwhite, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);

                                }
                                if (buildpick == "blue")
                                {
                                    Debug.Log("blue build");
                                    Destroy(closestobject());
                                    Instantiate(gridboxblue, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);
                                }
                                if (buildpick == "collector")
                                {
                                    Debug.Log("collector build");
                                    Destroy(closestobject());
                                    Instantiate(coinsCollecter, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);
                                }



                                //PurchaseAndInstantiateItem(hit.collider.gameObject.transform.position);

                                Debug.Log("Item bought successfully! Remaining coins: " + remainingCoins);
                            }
                            else
                            {
                                // If the player does not have enough coins, log a message indicating insufficient funds
                                Debug.Log("Not enough coins to buy the item!");
                            }
                        }
                        else
                        {
                            Debug.LogError("CoinManager reference not set!");
                        }
                    }
                }
                else if (hit.collider.CompareTag("colorboxwhite"))
                {
                    buildpick = "white";
                    Debug.Log(buildpick);
                }
                else if (hit.collider.CompareTag("colorboxblue"))
                {
                    buildpick = "blue";
                    Debug.Log(buildpick);
                }
                else if (hit.collider.CompareTag("collectorGreen"))
                {
                    buildpick = "collector";
                    Debug.Log(buildpick);
                }
            }
            else
            {
                // Handle cases where no collider is hit (e.g., clicking outside the grid)
                Debug.Log("Clicked outside the grid");
            }
        }
    }
}
