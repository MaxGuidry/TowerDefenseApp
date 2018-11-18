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
    public TowerBehavior TowerBehavior => Tower.GetComponent<TowerBehavior>();
    private Text ItemName => GetComponentInChildren<Text>();
    public Image ItemImage => GetComponentInChildren<Image>();
    public Button BuyItem => GetComponentInChildren<Button>();
    private static PlaceTowerAroundHub PlaceTower => FindObjectOfType<PlaceTowerAroundHub>();

    /// <summary>
    ///     Create the shop item with the correct data
    /// </summary>
    private void Start()
    {
        var towerName = TowerBehavior.data.TowerName ?? "No Name";
        ItemName.text = towerName;
        ItemImage.sprite = TowerBehavior.data.TowerSprite;
        ItemImage.color = TowerBehavior.gameObject.GetComponent<Renderer>().sharedMaterial.color;
        ItemImage.preserveAspect = true;
        BuyItem.GetComponentInChildren<Text>().text = TowerBehavior.data.buyCost.ToString();
        BuyItem.onClick.AddListener(() => PlaceTower.BuyTower(Tower));
        BuyItem.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
    }
}
