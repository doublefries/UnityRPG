using UnityEngine;

public class ShopButtonHandler : MonoBehaviour
{
    public enum ItemType { Speed, Strength }

    [SerializeField] private ItemType itemType;
    [SerializeField] private int cost = 5;
    [SerializeField] private float speedBoostAmount = 2f;
    [SerializeField] private int strengthBoostAmount = 5;

    public void OnBuyClicked()
    {
        if (ProgressionSystem.Instance == null)
        {
            Debug.LogWarning("ProgressionSystem not found.");
            return;
        }

        if (!ProgressionSystem.Instance.SpendCoins(cost))
        {
            Debug.Log("Not enough coins!");
            return;
        }

        switch (itemType)
        {
            case ItemType.Speed:
                ProgressionSystem.Instance.AddSpeedBonus(speedBoostAmount);
                Debug.Log($"Bought Speed boost! +{speedBoostAmount} speed.");
                break;

            case ItemType.Strength:
                ProgressionSystem.Instance.AddStrengthBonus(strengthBoostAmount);
                Debug.Log($"Bought Strength boost! +{strengthBoostAmount} damage.");
                break;
        }
    }
}
