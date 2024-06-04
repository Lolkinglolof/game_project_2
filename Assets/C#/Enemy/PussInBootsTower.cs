using System.Collections;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PussInBootsTower : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private float attackDamage1 = 10f;
    [SerializeField] private float attackDamage2 = 15f;
    [SerializeField] private float critialDamage = 20f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float bonusChance = 0.1f; // 10% chance for bonus damage
    [SerializeField] private Transform swordTransform;
    [SerializeField] private LayerMask EnemieLayers;
    [SerializeField] public float swordRange = 2f;
    public bool canAttack = true;

    private void Start()
    {
        animator.SetBool("IsAttacking", false);
        if (swordTransform == null)
        {
            Debug.LogError("Sword Transform not assigned!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (canAttack)
        {
            // Check if enemies are in range and attack
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, swordRange, EnemieLayers);
            if (enemiesInRange.Length > 0)
            {
                StartCoroutine(AttackCooldown());
                Attack();
            }
        }
    }


    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(swordTransform.position, Vector2.right, swordRange, EnemieLayers);
        Debug.DrawRay(transform.position, transform.right * swordRange, Color.green);
        if (hit.collider != null)
        {
            float damage = Random.Range(attackDamage1, attackDamage2);
            if (Random.value < bonusChance)
            {
                damage += critialDamage;
                Debug.Log("Critical Hit!");
            }

            // Deal damage to target
            EnemyData enemy = hit.collider.GetComponent<EnemyData>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Hit " + hit.collider.gameObject.name + " for " + damage + " damage!");
            }
            else
            {
                Debug.Log("Hit " + hit.collider.gameObject.name + ", but it does not have an EnemyData component!");
            }
            // Trigger sword swing animation
            animator.SetBool("IsAttacking", true); 
        }
        else
        {
            // If no enemy is in range, set animation to false
            animator.SetBool("IsAttacking", false);
        }

    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}