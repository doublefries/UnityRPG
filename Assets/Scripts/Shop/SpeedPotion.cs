//namepsace shop?
using UnityEngine;

public class SpeedPotion : Potion
{
    public float SpeedBoost { get; private set; }

    public SpeedPotion(int cost, float duration, float speedBoost)
        : base("Speed Potion", cost, duration)
    {
        SpeedBoost = speedBoost;
    }

    public override void ApplyEffect(PlayerStats player)
    {
        player.IncreaseSpeed(SpeedBoost);
        if (ProgressionSystem.Instance != null)
            ProgressionSystem.Instance.AddSpeedBonus(SpeedBoost);
        Debug.Log($"Bought {ItemName}. Speed +{SpeedBoost}");
    }
}