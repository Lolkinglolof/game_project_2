using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cointower : MonoBehaviour
{
    private float ShootTimer;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ShootTimer >= 3)
        {

            Instantiate(projectile, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, -0.3f), Quaternion.identity);
        }
        else
        {
            ShootTimer += Time.deltaTime;
            Debug.Log(ShootTimer);
        }
    }
}