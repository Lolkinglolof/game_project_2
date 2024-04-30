using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using static EnemyData.EnemyDetails; // Use static members directly
public class EnemyFarmer1 : MonoBehaviour
{
    private float EnemyDamage = 1;
    private float moveSpeed = 1;
    public Rigidbody2D rig;
    private EnemyData enemyData; // Reference to the EnemyData object
    // Reference to the WaveManager object
    public EnemyData enemyDataReference; // This line is missing
    private WaveManager waveManager;
    private List<EnemyData> registeredEnemyDataList = new List<EnemyData>(); // Example initialization
    // Start is called before the first frame update
   
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        waveManager = WaveManager.instance;
        
        // Access enemy stats from the referenced EnemyData object
        EnemyDamage = enemyDataReference.enemyDetailsList[0].damage; // Assuming "damage" is the first element
        moveSpeed = enemyDataReference.enemyDetailsList[0].movementSpeed;
        // Access other stats (health, attack range, etc.) from enemyDataReference
    
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerLife"))
        {
            // Destroy the enemy GameObject (assuming this script is attached to the enemy)
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Boxes"))
        {
            // Add damage to our Enemies against PlayerTowers.
        }
    }
}