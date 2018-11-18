using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;
using Object = UnityEngine.Object;

public class ShopManager : MonoBehaviour
{
    public List<Object> Towers;
    public GameObject ItemPrefab;
    private readonly List<ShopItem> _activeShopItems = new List<ShopItem>();

    /// <summary>
    ///     Used to call each method once the shop object is enabled
    /// </summary>
    private void OnEnable()
    {
        CreateShopList(Towers);
        StartCoroutine(PurchasableItems());
    }

    /// <summary>
    ///     Clears the shop items and stops all running coroutines
    /// </summary>
    private void OnDisable()
    {
        _activeShopItems.ForEach(item => Destroy(item.gameObject));
        _activeShopItems.Clear();
        StopAllCoroutines();
    }

    /// <summary>
    ///      Creates each shop item based on the prefabs in the TowerPrefab folder
    /// </summary>
    /// <param name="towerList"></param>
    private void CreateShopList([NotNull] List<Object> towerList)
    {
        towerList = Resources.LoadAll("TowerPrefab", typeof(GameObject)).ToList();

        if (towerList == null)
        {
            throw new ArgumentNullException(nameof(towerList));
        }

        if (towerList.Count == 0)
        {
            throw new ArgumentException("Value cannot to be an empty collection.", nameof(towerList));
        }

        foreach (var obj in towerList)
        {
            var shopItem = Instantiate(ItemPrefab, transform);
            shopItem.GetComponent<ShopItem>().Tower = obj as GameObject;
            _activeShopItems.Add(shopItem.GetComponent<ShopItem>());
        }

        _activeShopItems.ForEach(item => item.BuyItem.interactable = false);

        var sortedItems = _activeShopItems.OrderBy(item => item.TowerBehavior.data.buyCost).ToList();

        for (var i = 0; i < sortedItems.Count; i++)
            sortedItems[i].transform.SetSiblingIndex(i);
    }

    /// <summary>
    ///     If the item is able to purchace allow them to click it else don't
    /// </summary>
    /// <returns></returns>
    private IEnumerator PurchasableItems()
    {
        while (gameObject.activeInHierarchy)
        {
            foreach (var shopItem in _activeShopItems)
            {
                var shopItemColor = shopItem.BuyItem.GetComponent<Image>().color;

                if (InGameUIManager.Instance.Currency.Stat < shopItem.TowerBehavior.data.buyCost)
                {
                    shopItemColor = new Color(shopItemColor.r, shopItemColor.g, shopItemColor.b, 50);

                    shopItem.BuyItem.interactable = false;
                }
                else
                {
                    shopItemColor = new Color(shopItemColor.r, shopItemColor.g, shopItemColor.b, 255);

                    shopItem.BuyItem.interactable = true;
                }
            }
            yield break;
        }
    }
}
