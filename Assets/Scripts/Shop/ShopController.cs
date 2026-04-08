using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopController : MonoBehaviour
{
    [Header("Costs")]
    [SerializeField] private int speedCost = 15;
    [SerializeField] private int strengthCost = 20;

    [Header("Boost Amounts")]
    [SerializeField] private float speedAmount = 1f;
    [SerializeField] private int strengthAmount = 2;

    [Header("UI Text (optional)")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Navigation")]
    [SerializeField] private string returnSceneName = "IsometricScene";

    private void Start()
    {
        RefreshUI();
    }

    public void BuySpeed()
    {
        if (ProgressionSystem.Instance == null) return;

        if (ProgressionSystem.Instance.SpendCoins(speedCost))
        {
            ProgressionSystem.Instance.IncreaseSpeed(speedAmount);
            ShowMessage("Speed Boost purchased!");
        }
        else
        {
            ShowMessage("Not enough coins!");
        }

        RefreshUI();
    }

    public void BuyStrength()
    {
        if (ProgressionSystem.Instance == null) return;

        if (ProgressionSystem.Instance.SpendCoins(strengthCost))
        {
            ProgressionSystem.Instance.IncreaseStrength(strengthAmount);
            ShowMessage("Strength Boost purchased!");
        }
        else
        {
            ShowMessage("Not enough coins!");
        }

        RefreshUI();
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene(returnSceneName);
    }

    private void RefreshUI()
    {
        if (ProgressionSystem.Instance == null) return;

        if (coinsText != null)
            coinsText.text = "Coins: " + ProgressionSystem.Instance.coins;

        if (speedText != null)
            speedText.text = "Speed: +" + ProgressionSystem.Instance.speedBoost.ToString("F1");

        if (strengthText != null)
            strengthText.text = "Strength: +" + ProgressionSystem.Instance.strengthBoost;
    }

    private void ShowMessage(string msg)
    {
        if (messageText != null)
            messageText.text = msg;

        Debug.Log(msg);
    }
}
