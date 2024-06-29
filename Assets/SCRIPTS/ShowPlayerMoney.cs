using TMPro;
using UnityEngine;

public class ShowPlayerMoney : MonoBehaviour {
    private TextMeshProUGUI moneyAmountText;
    private PlayerStats playerS;

    private void Start() {
        //moneyAmountText
        moneyAmountText = GetComponent<TextMeshProUGUI>();
        Debug.Log(moneyAmountText != null
            ? "GetComponent<TextMeshProUGUI>() in ShowPlayerMoney istanziato"
            : "GetComponent<TextMeshProUGUI>() in ShowPlayerMoney non istanziato");

        //playerS
        playerS = FindObjectOfType<PlayerStats>();
        Debug.Log(moneyAmountText != null
            ? "FindObjectOfType<PlayerStats>() in ShowPlayerMoney istanziato"
            : "FindObjectOfType<PlayerStats>() in ShowPlayerMoney non istanziato");
    }

    private void Update() {
        if (playerS != null && moneyAmountText != null) moneyAmountText.text = playerS.PlayerMoney.ToString();
    }
}