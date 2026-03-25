using UnityEngine;

public class Sword : Weapon
{
    public Sword(int cost, int damage)
        : base("Sword", cost, damage)
    {
    }

    public override void ApplyEffect(PlayerStats player)
    {
        player.EquipWeapon(this);
        Debug.Log($"Equipped {ItemName} with {Damage} damage.");
    }
}
