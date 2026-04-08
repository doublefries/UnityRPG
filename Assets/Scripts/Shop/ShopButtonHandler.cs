using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public enum ItemType { Speed, Strength }

    [SerializeField] private ItemType itemType;
    [SerializeField] private int cost = 5;
    [SerializeField] private float speedBoostAmount = 2f;
    [SerializeField] private int strengthBoostAmount = 5;
    
     private void Awake()
        {
            EnsureEventSystemExists();
        }
    
        private static void EnsureEventSystemExists()
        {
            if (EventSystem.current != null || FindObjectOfType<EventSystem>() != null)
                return;
    
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
            Debug.Log("No EventSystem found in scene. Created one automatically for UI clicks.");
        }

    public void OnBuyClicked()
    {
        Debug.Log("Clicked");
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
