
using UnityEngine;

public class StrengthPotion : Potion
{
    public int StrengthBoost { get; private set; }

    public StrengthPotion(int cost, float duration, int strengthBoost)
        : base("Strength Potion", cost, duration)
    {
        StrengthBoost = strengthBoost;
    }

    public override void ApplyEffect(PlayerStats player)
    {
        player.IncreaseStrength(StrengthBoost);
        Debug.Log($"Bought {ItemName}. Strength +{StrengthBoost}");
    }
}