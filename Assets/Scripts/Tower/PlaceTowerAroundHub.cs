using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerAroundHub : MonoBehaviour
{
    public GameObject towerPrefab;
    // Use this for initialization
    void Start()
    {

        TESTOBJ = GameObject.Instantiate(towerPrefab);
        rend = TESTOBJ.GetComponent<Renderer>();
    }

    bool canPlace;
#if UNITY_EDITOR
    Vector3 mouse => Input.mousePosition;
    public GameObject TESTOBJ;
#endif
    public Camera cam;
    public GameObject Hub;
    // Update is called once per frame
    void Update()
    {

        //in editor use mouse
#if UNITY_EDITOR
        canPlace = false;
        var worldPoint = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, (Hub.transform.position - cam.transform.position).magnitude));
        TESTOBJ.transform.position = worldPoint;
        var cols = Physics.OverlapBox(new Vector3(worldPoint.x, 0, worldPoint.z), new Vector3(towerPrefab.transform.localScale.x/2, 1, towerPrefab.transform.localScale.z/2));
       
        for(int i = 0;i<cols.Length;i++)
        {
            if(cols[i].gameObject.tag == "Tower"||cols[i].gameObject.tag == "Hub")
            {
                canPlace = false;
                break;
            }
            if (cols[i].gameObject.tag == "Area")
                canPlace = true;
        }
#endif
        if (canPlace)
            rend.material = Green;
        else
            rend.material = Red;
    }
    public Material Red;
    public Material Green;
    Renderer rend;
}
