using System;
using System.Collections;
using System.Collections.Generic;
using Tower;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject Tower;
    private TowerBehavior TowerBehavior => Tower.GetComponent<TowerBehavior>();
    private Text ItemName => GetComponentInChildren<Text>();
    private Image ItemImage => GetComponentInChildren<Image>();
    private Button BuyItem => GetComponentInChildren<Button>();
    private static PlaceTowerAroundHub PlaceTower => FindObjectOfType<PlaceTowerAroundHub>();

    // Use this for initialization
    private void Awake()
    {
        var towerName = TowerBehavior.data.TowerName ?? "No Name";
        ItemName.text = towerName;
        ItemImage.sprite = TowerBehavior.data.TowerSprite;
        BuyItem.GetComponentInChildren<Text>().text = TowerBehavior.data.buyCost.ToString();
        BuyItem.onClick.AddListener(() => PlaceTower.BuyTower(Tower));
    }
}
