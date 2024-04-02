using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    private string buildpick = "white";
    public GameObject gridboxwhite;
    public GameObject gridboxblue;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gridboxwhite, new Vector2(-15, -8), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 worldPos = Input.mousePosition;
        worldPos.x = worldPos.x /76.5f;
        worldPos.y = worldPos.y /80;
        if (Input.GetMouseButtonUp(0))
        {

            Debug.Log(worldPos);
        }
        if (worldPos.y <= 0.95f )
        {
            if (worldPos.y >= 0.05f) 
            { 
                if (worldPos.x <= 0.95f)
                {
                    if (worldPos.x >= 0.05f)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            if ( buildpick == "white")
                            {
                                Destroy(GameObject.FindWithTag("11"));
                                Instantiate(gridboxwhite, new Vector2(-15, -8), Quaternion.identity);
                            }
                            if ( buildpick == "blue")
                            {
                                Destroy(GameObject.FindWithTag("11"));
                                Instantiate(gridboxblue, new Vector2(-15, -8), Quaternion.identity);
                            }
                            Debug.Log(worldPos);
                        }
                    }
                }
            }
        }
        if (worldPos.y <= 0.95f)
        {
            if (worldPos.y >= 0.05f)
            {
                if (worldPos.x <= 8f)
                {
                    if (worldPos.x >= 7.05f)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            var buildpick = "white";
                            Debug.Log("white");

                        }
                    }
                }
            }
        }
        if (worldPos.y <= 0.95f)
        {
            if (worldPos.y >= 0.05f)
            {
                if (worldPos.x <= 9f)
                {
                    if (worldPos.x >= 8.05f)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            var buildpick = "blue";
                            Debug.Log("blue");

                        }
                    }
                }
            }
        }

    }

    private void OnMouseUp()
    {

    }
}