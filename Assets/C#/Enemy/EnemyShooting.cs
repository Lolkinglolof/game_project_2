using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float bulletReloadTime = 0;
    public float bulletDespawnTimer = 10;
    

    public GameObject bullet;
    public Transform bulletPoint;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        bulletReloadTime -= Time.deltaTime;
     
        if( rb != null ) 
        {
            if( bulletReloadTime <= 0 )
            { 
                shoot();
                bulletReloadTime = 10; // set bulletReloadTime back to default. 
            }
            
        }
      
    }
    public void shoot()
    {
        Rigidbody2D p = Instantiate (bullet, transform.position,quaternion.identity).GetComponent<Rigidbody2D>();   
        Destroy ( p.gameObject,10); // destroy bullet, however, just the clone only. 
    }
}
