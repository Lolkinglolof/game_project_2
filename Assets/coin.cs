using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        BuildingGrid.Cash +=10;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
