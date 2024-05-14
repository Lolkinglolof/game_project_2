using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class randomcoinscript : MonoBehaviour
{
    public float spawnchance = 80;
    public float chance = 0;
    public GameObject coin;
    private float spawntimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        chance = spawnchance / 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        spawntimer -= Time.deltaTime;
        if (spawntimer <= 0)
        {
            Debug.Log("count end");
            spawntimer = 10f;
            if (Random.value < chance)
            {
                Instantiate(coin, new Vector3(Random.Range(-10f,6f),Random.Range(-4f,3f),-3f),Quaternion.identity);
                
            }
        }
    }
}
