using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        UpdateCoinText();
    }

    private void Update()
    {
        if (ProgressionSystem.Instance == null)
            return;

        coinText.text = ProgressionSystem.Instance.coins.ToString();
    }

    private void UpdateCoinText()
    {
        if (ProgressionSystem.Instance == null)
        {
            coinText.text = "0";
            return;
        }

        coinText.text = ProgressionSystem.Instance.coins.ToString();
    }
}