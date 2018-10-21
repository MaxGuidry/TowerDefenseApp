using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerAroundHub : MonoBehaviour
{


    void Start()
    {
        BuyTower();
    }

    public GameObject towerPrefab;
    bool canPlace;

    Vector3 screenPos;

    public Camera cam;
    public GameObject Hub;
    [HideInInspector]
    public GameObject newTower;

    Renderer rend;
    public void BuyTower()
    {
        newTower = GameObject.Instantiate(towerPrefab);
        rend = newTower.GetComponent<Renderer>();
        StartCoroutine(PlaceTower());
    }
    public IEnumerator PlaceTower()
    {

        bool placed = false;
        while (!placed)
        {
#if UNITY_EDITOR
            screenPos = Input.mousePosition;
#endif
            canPlace = false;
            var worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, (Hub.transform.position - cam.transform.position).magnitude));
            newTower.transform.position = worldPoint;
            var cols = Physics.OverlapBox(new Vector3(worldPoint.x, 0, worldPoint.z), new Vector3(towerPrefab.transform.localScale.x / 2, 1, towerPrefab.transform.localScale.z / 2));

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject == newTower)
                    continue;
                if (cols[i].gameObject.tag == "Tower" || cols[i].gameObject.tag == "Hub")
                {
                    canPlace = false;
                    break;
                }
                if (cols[i].gameObject.tag == "Area")
                    canPlace = true;
            }
            //TODO: change to button for placing.
            if (canPlace && Input.GetMouseButton(0))
            {
                placed = true;
            }

            if (canPlace)
                rend.material.color = Color.green;
            else
                rend.material.color = Color.red;
            yield return null;
        }
        rend.material.color = Color.blue;

        //TODO: REMOVE WHEN BUYING IS ACTUALLY SET UP THIS IS FOR TESTING
        BuyTower();
    }
}
