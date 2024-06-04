using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtShooter : MonoBehaviour
{
    [SerializeField] public GameObject mudPrefab; // Prefab of the dirt mud bullet
    [SerializeField] public Transform launchPoint; // Point from where the mud will be launched
    [SerializeField] public float launchForce = 10f; // Force applied to the mud bullet
    [SerializeField] public float reloadMudShotTime = 1.0f; // Time it takes before the next mud shot can be fired
    [SerializeField] public float mudRangeFire = 1.0f; // Range within which the enemy needs to be for firing
    [SerializeField] public float mudRangeDistanceFireLimit = 1.0f; // Max distance the mud can be fired
    [SerializeField] public float mudDamageArea = 1.0f; // Area of damage around the mud impact
    [SerializeField] public float mudDamage1; // Damage parameter for mud bullet
    [SerializeField] public float mudDamage2; // Damage parameter for mud bullet

    private float lastShotTime; // To keep track of the last time we fired

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -reloadMudShotTime; // Set initial value for lastShotTime to allow immediate first shot
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last shot
        if (Time.time - lastShotTime >= reloadMudShotTime)
        {
            // Find all enemies within mudRangeFire
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, mudRangeFire);
            foreach (Collider2D collider in colliders)
            {
                // Check if the collider is an enemy
                if (collider.CompareTag("Enemy"))
                {
                    // Calculate distance to the enemy
                    float distanceToEnemy = Vector2.Distance(transform.position, collider.transform.position);

                    // Check if the enemy is within shooting distance
                    if (distanceToEnemy <= mudRangeDistanceFireLimit)
                    {
                        // Fire mud
                        FireMud(collider.gameObject);
                        break; // Exit the loop after firing at one enemy
                    }
                }
            }
        }
    }

    void FireMud(GameObject enemy)
    {
        // Instantiate mud prefab
        GameObject mud = Instantiate(mudPrefab, launchPoint.position, Quaternion.identity);

        // Calculate direction towards the enemy
        Vector2 direction = (enemy.transform.position - launchPoint.position).normalized;

        // Apply force to the mud
        Rigidbody2D rb = mud.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);

        // Set the mud's damage parameters
        MudBullet mudBulletScript = mud.GetComponent<MudBullet>();
        if (mudBulletScript != null)
        {
            Debug.Log("Damage: " + mudDamage1 + ", " + mudDamage2 + ", " + mudDamageArea);
            mudBulletScript.SetDamageParameters(mudDamage1, mudDamage2, mudDamageArea);
            Debug.Log("Damage: " + mudDamage1 + ", " + mudDamage2 + ", " + mudDamageArea);
        }

        // Update last shot time
        lastShotTime = Time.time;
    }
}