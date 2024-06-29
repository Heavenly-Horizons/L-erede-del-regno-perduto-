using TMPro;
using UnityEngine;

public class LevelUpButton : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI atkLevelText;
    [SerializeField] private TextMeshProUGUI staminaLevelText;
    [SerializeField] private TextMeshProUGUI defLevelText;
    [SerializeField] private TextMeshProUGUI hpRegenLevelText;
    [SerializeField] private TextMeshProUGUI spRegenLevelText;
    [SerializeField] private TextMeshProUGUI MoneyAmountText;
    private PlayerStats playerStats;

    private void Awake() {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Debug.Log(playerStats != null
            ? "GameObject.FindGameObjectWithTag(\"Player\").GetComponent<PlayerStats>() in LevelUpButton istanziato"
            : "GameObject.FindGameObjectWithTag(\"Player\").GetComponent<PlayerStats>() in LevelUpButton non istanziato");
    }

    public void LevelUpPlayerStat(int playerStat) {
        if (playerStats.PlayerMoney >= 10) {
            switch (playerStat) {
                case 0:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.atkLevel += 1;
                    playerStats.playerDamage += playerStats.playerDamage * playerStats.atkLevel / 10;
                    atkLevelText.text = "livello " + playerStats.atkLevel;
                    break;
                case 1:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.staminaLevel += 1;
                    playerStats.maxStamina += playerStats.maxStamina * playerStats.staminaLevel / 10;
                    staminaLevelText.text = "livello " + playerStats.staminaLevel;
                    break;
                case 2:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.defLevel += 1;
                    if (playerStats.defLevel >= 5)
                        playerStats.playerDefence += 1;
                    else
                        playerStats.playerDefence += 0.5f;
                    defLevelText.text = "livello " + playerStats.defLevel;
                    break;
                case 3:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.hpRegenLevel += 1;
                    hpRegenLevelText.text = "livello " + playerStats.hpRegenLevel;
                    break;
                case 4:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.spRegenLevel += 1;
                    spRegenLevelText.text = "livello " + playerStats.spRegenLevel;
                    break;
            }

            playerStats.SavePlayerAndScene();
        }
    }
}