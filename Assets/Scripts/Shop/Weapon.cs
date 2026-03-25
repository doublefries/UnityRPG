using UnityEngine;
public abstract class Weapon : ShopItem
{
    public int Damage { get; protected set; }

    public Weapon(string itemName, int cost, int damage)
        : base(itemName, cost)
    {
        Damage = damage;
    }
}
