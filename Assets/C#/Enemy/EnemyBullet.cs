using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletDamage = 0; //damage
    public float velositySpeed = 0; //bullet speed

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletVeleosity = Vector3.left;
        bulletVeleosity.x = velositySpeed * Time.deltaTime;


    }
}
