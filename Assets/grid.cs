using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    private string buildpick = "white";
    public GameObject gridboxwhite;
    public GameObject gridboxblue;
    public GameObject starterbox;
    GameObject closestobject()
    {
        float closestdistance = 100;
        GameObject gameobjecttoreturn = null;
        Collider2D[] closestobjects = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach ( Collider2D objecttocheck in closestobjects)
        {
            if(objecttocheck.tag == "block")
            {
                if(Vector3.Distance(transform.position,objecttocheck.transform.position) < closestdistance)
                {
                
                    closestdistance = Vector3.Distance(transform.position, objecttocheck.transform.position);
                    gameobjecttoreturn = objecttocheck.gameObject;
                }
            }
            
        }
        return gameobjecttoreturn;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "gridblock")
        {
            Instantiate(starterbox, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-0.5f), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            Debug.Log(hit.collider != null && hit.collider.gameObject.tag == "gridblock" && hit.collider.gameObject.tag == "colorboxwhite" && hit.collider.gameObject.tag == "colorboxblue" && hit.collider.gameObject.tag == "block");
            if (hit.collider.gameObject.tag == "gridblock")
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    if (buildpick == "white")
                    {
                        Debug.Log("white build");
                        Destroy(closestobject());
                        Instantiate(gridboxwhite, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);

                    }
                    if (buildpick == "blue")
                    {
                        Debug.Log("blue build");
                        Destroy(closestobject());
                        Instantiate(gridboxblue, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -0.5f), Quaternion.identity);
                    }
                }
            }
            if (hit.collider.tag == "colorboxwhite")
            {
                buildpick = "white";
                Debug.Log(buildpick);
            }
            if (hit.collider.tag == "colorboxblue")
            {
                
                buildpick = "blue";
                Debug.Log(buildpick);
            }
            //if (hit.collider.gameObject.tag == "block")
            //{
            //    if (hit.collider.gameObject.name == gameObject.name)
            //    {
            //        Destroy(gameObject);
            //    }
            //}//
        }
    }
}