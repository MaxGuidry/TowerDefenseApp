using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject ItemPrefab;

	// Use this for initialization
	void Start () {
		Test();
	}

    void Test()
    {
        var item = Instantiate(ItemPrefab, transform);
        item.GetComponent<ShopItem>().Tower = Resources.Load("TowerPrefab/Long") as GameObject;
    }
}
