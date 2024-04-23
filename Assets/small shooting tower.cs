using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallshootingtower : MonoBehaviour
{
    public GameObject bullet;
    public float shoottimer;
    public float shoottime;
    public float damagetime;
    public float damagetimer;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shoottimer = Time.deltaTime + shoottimer;
        if (shoottimer >= shoottime)
        {
            Instantiate(bullet);
            shoottimer = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        damagetimer = Time.deltaTime;
        if (damagetimer >= damagetime)
        {
            health = health - 1;
            damagetimer = 0;
        }
    }
}
