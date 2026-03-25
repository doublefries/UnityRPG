using UnityEngine;

public abstract class Potion : ShopItem
{
    public float Duration { get; protected set; }

    public Potion(string itemName, int cost, float duration)
        : base(itemName, cost)
    {
        Duration = duration;
    }
}
