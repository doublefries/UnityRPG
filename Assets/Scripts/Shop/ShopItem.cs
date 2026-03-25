using UnityEngine;

public abstract class ShopItem
{
    public string ItemName { get; protected set; }
    public int Cost { get; protected set; }

    public ShopItem(string itemName, int cost)
    {
        ItemName = itemName;
        Cost = cost;
    }

    public abstract void ApplyEffect(PlayerStats player);
}
