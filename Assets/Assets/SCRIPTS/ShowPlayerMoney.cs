using TMPro;
using UnityEngine;

public class ShowPlayerMoney : MonoBehaviour
{
    private static readonly string PlayerMoneyAmount = "PlayerMoneyAmount";
    private int moneyAmount;
    private TextMeshProUGUI moneyAmountText;
 
    void Awake()
    {
        moneyAmountText = GetComponent<TextMeshProUGUI>();
        moneyAmount = PlayerPrefs.GetInt(PlayerMoneyAmount);
    }

    void Update() {
        moneyAmountText.text = moneyAmount.ToString();
    }

}
