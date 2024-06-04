using System.Collections;
using UnityEngine;

public class MudBullet : MonoBehaviour
{
    public float speed = 10f;
    public float fallTime = 2f; // Time before the bullet falls
    private bool isFalling = false;

    private float damage1;
    private float damage2;
    private float damageArea;

    public void SetDamageParameters(float d1, float d2, float area)
    {
        damage1 = d1;
        damage2 = d2;
        damageArea = area;
    }

    private void Start()
    {
        StartCoroutine(MoveBullet());
    }

    private IEnumerator MoveBullet()
    {
        // Move the bullet to the right for a certain time
        float timer = 0f;
        while (timer < fallTime)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Bullet has reached its destination, start falling
        isFalling = true;

        // Wait for a moment before the bullet falls
        yield return new WaitForSeconds(0.5f);

        // Enable gravity for the bullet to fall
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    private void Update()
    {
        if (isFalling)
        {
            // Do additional falling behavior here if needed
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            float damage = Random.Range(damage1, damage2);
            Debug.Log("Damage: " + damage1 + damage2);
            collision.collider.GetComponent<EnemyData>().TakeDamage(damage);

            // Apply damage to surrounding area if needed
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageArea);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float areaDamage = damage * 0.1f; // 10% of the damage applied to the enemy
                    collider.GetComponent<EnemyData>().TakeDamage(areaDamage);
                    Debug.Log("Damage: " + areaDamage);
                }
            }

            // Destroy the mud bullet after colliding with an enemy
            Destroy(gameObject);
        }
    }
}