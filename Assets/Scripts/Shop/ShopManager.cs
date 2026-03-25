using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private List<ShopItem> shopItems = new List<ShopItem>();

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ShopUI shopUI;

    public IReadOnlyList<ShopItem> ShopItems => shopItems;

    private void Awake()
    {
        shopItems.Add(new StrengthPotion(20, 10f, 5));
        shopItems.Add(new SpeedPotion(15, 8f, 2f));
        shopItems.Add(new Axe(50, 25));
        shopItems.Add(new Sword(40, 18));
    }

    public void OpenShop()
    {
        shopUI.Show(this, playerStats);
    }

    public void CloseShop()
    {
        shopUI.Hide();
    }

    public bool BuyItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
            return false;

        ShopItem item = shopItems[index];

        if (!playerStats.SpendCoins(item.Cost))
        {
            Debug.Log("Not enough coins.");
            return false;
        }

        item.ApplyEffect(playerStats);
        shopUI.Refresh();
        return true;
    }
}