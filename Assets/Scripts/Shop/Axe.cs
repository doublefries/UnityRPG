using UnityEngine;

public class Axe : Weapon
{
    public Axe(int cost, int damage)
        : base("Axe", cost, damage)
    {
    }

    public override void ApplyEffect(PlayerStats player)
    {
        player.EquipWeapon(this);
        Debug.Log($"Equipped {ItemName} with {Damage} damage.");
    }
}
