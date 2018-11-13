using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerAroundHub : MonoBehaviour
{


    void Start()
    {
        //BuyTower();
    }

    public GameObject towerPrefab;
    bool canPlace;

    Vector3 screenPos;

    public Camera cam;
    public GameObject Hub;
    [HideInInspector]
    public GameObject newTower;

    Renderer rend;

    public void BuyTower(GameObject tower)
    {
        var currentTower = tower.GetComponent<Tower.TowerBehavior>().data;
        if (Player.PlayerData.Money < currentTower.buyCost)
        {
            //TODO: Enable popup saying you cannot buy or have this check before the button is available to press (greyed out buttons)
            return;
        }
        Player.PlayerData.Money -= currentTower.buyCost;
        newTower = GameObject.Instantiate(tower);
        rend = newTower.GetComponent<Renderer>();

        StartCoroutine(PlaceTower(currentTower));
    }
    private IEnumerator PlaceTower(Tower.TowerData tower)
    {
        bool placed = false;
        while (!placed)
        {
#if UNITY_EDITOR
            screenPos = Input.mousePosition;
            //TODO: make button or something with touchscreen that can cancel purchase
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelTowerPurchase(tower);
                yield break;
            }

#endif
            canPlace = false;
            var worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, (Hub.transform.position - cam.transform.position).magnitude));
            worldPoint.y = .5f;
            newTower.transform.position = worldPoint;
            var cols = Physics.OverlapBox(new Vector3(worldPoint.x, worldPoint.y, worldPoint.z), new Vector3(towerPrefab.transform.localScale.x * 1.75f, 1, towerPrefab.transform.localScale.z * 1.75f));

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


    }

    public void CancelTowerPurchase(Tower.TowerData tower)
    {
        Destroy(newTower);
        Player.PlayerData.Money += tower.buyCost;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (newTower != null)
            Gizmos.DrawWireCube(new Vector3(newTower.transform.position.x, 0, newTower.transform.position.z), new Vector3(towerPrefab.transform.localScale.x * 1.75f, 1, towerPrefab.transform.localScale.z * 1.75f) * 2);
        
    }





#endif
}
