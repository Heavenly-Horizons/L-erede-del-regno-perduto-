using TMPro;
using UnityEngine;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI atkLevelText;
    [SerializeField] private TextMeshProUGUI staminaLevelText;
    [SerializeField] private TextMeshProUGUI defLevelText;
    [SerializeField] private TextMeshProUGUI hpRegenLevelText;
    [SerializeField] private TextMeshProUGUI spRegenLevelText;
    [SerializeField] private TextMeshProUGUI MoneyAmountText;
    private PlayerStats playerStats;
    private Player_Attack _playerAttack;

    void Start(){
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();
    }

    public void LevelUpPlayerStat(int playerStat){
        if(playerStats.PlayerMoney >= 10){
            switch(playerStat){
                case 0:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.atkLevel += 1;
                    playerStats.playerDamage += playerStats.playerDamage * playerStats.atkLevel / 10;
                    _playerAttack.playerDamage = playerStats.playerDamage;
                    atkLevelText.text = "livello " + playerStats.atkLevel.ToString();
                    break;    
                case 1:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.staminaLevel += 1;
                    playerStats.maxStamina += playerStats.maxStamina * playerStats.staminaLevel / 10;
                    staminaLevelText.text = "livello " + playerStats.staminaLevel.ToString();
                    break;
                case 2:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.defLevel += 1;
                    if(playerStats.defLevel >= 5){ playerStats.playerDefence += 1; }
                    else{ playerStats.playerDefence += 0.5f; }
                    defLevelText.text = "livello " + playerStats.defLevel.ToString();
                    break;
                case 3:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.hpRegenLevel += 1;
                    hpRegenLevelText.text = "livello " + playerStats.hpRegenLevel.ToString();
                    break;
                case 4:
                    playerStats.PlayerMoney -= 10;
                    MoneyAmountText.text = playerStats.PlayerMoney.ToString();
                    playerStats.spRegenLevel += 1;
                    spRegenLevelText.text = "livello " + playerStats.spRegenLevel.ToString();
                    break;
            }

            playerStats.SavePlayerAndScene();
        }
    }
}
