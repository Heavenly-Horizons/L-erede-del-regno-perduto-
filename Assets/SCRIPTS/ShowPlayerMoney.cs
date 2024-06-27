using TMPro;
using UnityEngine;

public class ShowPlayerMoney : MonoBehaviour {
    private TextMeshProUGUI moneyAmountText;
    private PlayerStats playerS;

    private void Start() {
        moneyAmountText = GetComponent<TextMeshProUGUI>();
        playerS = FindObjectOfType<PlayerStats>();
    }

    private void Update() {
        if (playerS != null && moneyAmountText != null) moneyAmountText.text = ": " + playerS.PlayerMoney;
    }
}