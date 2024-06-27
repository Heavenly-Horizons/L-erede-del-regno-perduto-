using TMPro;
using UnityEngine;

public class ShowPlayerMoney : MonoBehaviour
{
    private TextMeshProUGUI moneyAmountText;
    private PlayerStats playerS;

    void Start() {
        moneyAmountText = GetComponent<TextMeshProUGUI>();
        playerS = FindObjectOfType<PlayerStats>(); 
    }

    void Update() {
        if (playerS != null && moneyAmountText != null) {
            moneyAmountText.text =  playerS.PlayerMoney;
        }
    }
}
