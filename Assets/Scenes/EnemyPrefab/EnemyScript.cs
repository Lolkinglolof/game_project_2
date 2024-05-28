using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    float AttackCooldown = 0;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1, 0);
        
        
            AttackCooldown += Time.deltaTime;
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("block"))
        {
            if (collision.collider.gameObject.name == "blue box")
            {
                if (AttackCooldown >= 3)
                {
                    CoinTower cotoersiot = collision.collider.gameObject.GetComponent<CoinTower>();
                    cotoersiot.health -= 1;
                    AttackCooldown = 0;
                }
            }
            if (collision.collider.gameObject.name == "blue box")
            {
                if (AttackCooldown >= 3)
                {
                    SmallShootingTower cotoersiot = collision.collider.gameObject.GetComponent<SmallShootingTower>();
                    cotoersiot.health -= 1;
                    AttackCooldown = 0;
                }
            }
        }

    }
}
