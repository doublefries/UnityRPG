using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Transform itemButtonContainer;
    [SerializeField] private GameObject itemButtonPrefab;

    private ShopManager shopManager;
    private PlayerStats playerStats;

    public void Show(ShopManager manager, PlayerStats player)
    {
        shopManager = manager;
        playerStats = player;
        shopPanel.SetActive(true);
        BuildUI();
        Refresh();
    }

    public void Hide()
    {
        shopPanel.SetActive(false);
    }

    public void Refresh()
    {
        if (playerStats != null)
        {
            coinsText.text = "Coins: " + playerStats.Coins;
        }
    }

    private void BuildUI()
    {
        foreach (Transform child in itemButtonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < shopManager.ShopItems.Count; i++)
        {
            int index = i;
            GameObject buttonObj = Instantiate(itemButtonPrefab, itemButtonContainer);

            TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            Button button = buttonObj.GetComponent<Button>();

            ShopItem item = shopManager.ShopItems[i];
            text.text = item.ItemName + " - " + item.Cost + " coins";

            button.onClick.AddListener(() => shopManager.BuyItem(index));
        }
    }
}