using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    [SerializeField] private float health; // Health points of the block
    private float timeSinceLastDamage = 0f; // Time since last damage application
    [SerializeField] private float damageInterval = 5f; // Time between damage applications (in seconds)
    public bool enemyInRange = false; // Flag to indicate enemy is within attack range

    [SerializeField]public float towerDefence1, towerDefence2;
    public int EnemyLayerMask
    {
        get { return enemyLayerMask; }
    }

    private int enemyLayerMask; // Declare without initialization

    void Awake()
    {
       enemyLayerMask = LayerMask.GetMask("EnemieLayers"); // Replace "Enemy" with your actual layer name
    }

    void Update()
    {
        timeSinceLastDamage += Time.deltaTime;

        // Check if enough time has passed since last damage (optional)
        if (timeSinceLastDamage >= damageInterval)
        {
            // Combine layer mask and tag check
            int enemyLayerMask = LayerMask.GetMask("EnemieLayers");

            // Replace these values with your actual enemy detection logic (origin and direction)
            Vector2 origin = transform.position; // Replace with your enemy detection origin
            Vector2 direction = Vector2.up; // Replace with your enemy detection direction (adjust based on your enemy position)
            float distance = 10f; // Replace with your desired raycast distance
            Debug.Log("Using layer mask: " + enemyLayerMask);

            // Perform raycast to check for enemies
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, enemyLayerMask);

            if (Input.GetKeyDown(KeyCode.Space)) // Draw raycast on space key press
            {
                Debug.DrawRay(transform.position, Vector2.left * 0.5f, Color.red, 0.5f);
            }

            if (hit.collider != null && hit.collider.gameObject.activeInHierarchy && hit.collider.gameObject.CompareTag("Enemy"))
            {
              
                Debug.Log("Enemy hit the block!1"); // New debug log
                // Enemy hit with the "Enemy" tag, proceed with damage logic
                EnemyData enemyData = hit.collider.gameObject.GetComponent<EnemyData>();
                Debug.Log("Enemy hit the block!2");
                
                if (enemyData != null)
                {
               
                    health -= Random.Range(enemyData.minDamage, enemyData.maxDamage);
                 
                    timeSinceLastDamage = 0f;
                   
                }
                else
                {
                    enemyData.animiator.SetBool("EnemyTakesDamage", false); // if they are not being attack they won't show damage effect.
                }
            }
        }

        // Check for destruction
        if (health <= 0)
        {
            Destroy(gameObject);

        }
    }

    private RaycastHit2D RaycastForEnemy()
    {
        // Replace these values with your actual raycast origin and direction based on your enemy detection logic
        Vector2 origin = transform.position; // Replace with your enemy detection origin
        Vector2 direction = Vector2.up; // Replace with your enemy detection direction (adjust based on your enemy position)
        float distance = 1f; // Replace with your desired raycast distance

        return Physics2D.Raycast(origin, direction, distance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Building" tag (optional, modify if needed)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Set enemyInRange to true on building collision
            enemyInRange = true;
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy hit the block!"); // New debug log
        float effectiveDamage = Mathf.Max(damage - Random.Range(towerDefence1, towerDefence2), 0);

        // Apply the effective damage to health
        health -= effectiveDamage;

        // Check for minimum health or destroy the tower (optional)
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Block destroyed!");
        }
    }
}