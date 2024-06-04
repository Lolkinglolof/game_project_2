<<<<<<< Updated upstream
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
=======
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using Color = UnityEngine.Color;


public class EnemyData : MonoBehaviour
{
    //[SerializeField] public Animator animatorTakeDamage; // Eenemies lossing health effect
    [SerializeField] public Animator animiator; // attack animation
    [SerializeField] private float health; // Enemy's health points
    [SerializeField] private float movementSpeed; // Enemy's movement speed
    [SerializeField] public float minDamage, maxDamage;// Direct damage dealt per attack (can be edited in Inspector)
    [SerializeField] private float attackRange; // Distance to detect blocks using Raycast
    [SerializeField] public float defence1, defence2;
    private Rigidbody2D rb;
    private bool isAttacking; // Flag to indicate if enemy is currently attacking
    [SerializeField] public CoinDrop coinDrop; // Reference to the CoinDrop component attached to this enemy
    private float timeSinceLastDamage = 0f;
    public float damageInterval = 5f; // Time between damage application (in seconds)


  
    public int EnemyLayerMask
    {
        get { return enemyLayerMask; }
    }

    private int enemyLayerMask; // Declare without initialization

    void Awake()
    {
        animiator.SetBool("EnemyTakesDamage", false);
        enemyLayerMask = LayerMask.GetMask("TowerDefencer"); // Replace "Enemy" with your actual layer name
    }
    private void Start()
    {
        animiator.SetBool("EnemyAttack", false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        // LayerMask to exclude enemy layer from Raycast
        int enemyLayerMask = LayerMask.GetMask("TowerDefencer"); // Invert the mask
        Debug.Log("Using layer mask: " + enemyLayerMask);
        // Raycast for block detection with limited range
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange, LayerMask.GetMask("TowerDefencer"));

        Debug.DrawRay(transform.position, Vector2.left * attackRange, Color.green, 0.1f);

        // Check for block and update attacking state
        isAttacking = (hit.collider != null && hit.collider.CompareTag("block") || CompareTag("bullet"));
        if(hit)
        {
            Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
            Debug.Log("Hit something: " + hit.collider.name); // Log name (if available)
        }
        if (hit.collider != null)
        {
            Debug.Log("Hit something: " + hit.collider.name); // Log name (if available)
            Debug.Log("Hit collider type: " + hit.collider.GetType()); // Log collider type
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Draw raycast on space key press
        {
            Debug.DrawRay(transform.position, Vector2.left * attackRange, Color.red, 0.5f);
        }
        // Movement control based on attacking state
        if (isAttacking)
        {
            rb.velocity = Vector2.zero; // Stop movement
            animiator.SetBool("EnemyAttack", true);
        }
        else
        {
            rb.velocity = Vector2.left * movementSpeed * Time.deltaTime; // Move leftward
            animiator.SetBool("EnemyAttack", false);
        }

        if (isAttacking && hit.collider != null && hit.collider.GetComponent<BlockHealth>() != null)
        {
            hit.collider.GetComponent<BlockHealth>().enemyInRange = true; //check if enemyInRange = true or false.
        }
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= damageInterval)
        {
            if (hit.collider != null && hit.collider.CompareTag("block") && isAttacking || CompareTag("bullet") && isAttacking)
            {
                // Get the block's health script (assuming it has one)
                
                BlockHealth blockHealth = hit.collider.gameObject.GetComponent<BlockHealth>();
                blockHealth.TakeDamage(Random.Range(minDamage, maxDamage));
                
                // ... rest of the code for applying damage to blockHealth ...
                timeSinceLastDamage = 0f; // Reset timer
            }
            else if (hit.collider == null)
            {
                Debug.Log("Hit something but no collider detected!");
            }
        }
       
    }


    public void TakeDamage(float damage)
    {
       

        float effectiveDamage = Mathf.Max(damage - Random.Range(defence1, defence2), 0);
        // Apply the effective damage to health
        health -= effectiveDamage;
        animiator.SetBool("EnemyTakesDamage", true); // trigger the hurt Animations for the Enemy whenever they are taking damage
       
        if (health <= 0)
        {
            animiator.SetBool("EnemyTakesDamage", false);
            Destroy(gameObject); // Or handle enemy death in another way
            if (coinDrop != null)
            {
                coinDrop.DropCoins();
            }
>>>>>>> Stashed changes
        }
    }
}