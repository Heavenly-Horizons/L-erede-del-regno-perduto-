using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    // Campi per la gestione dei valori del player
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";
    private static readonly string PlayerHealthValue = "PlayerHealthValue";
    private static readonly string PlayerStaminaValue = "PlayerStaminaValue";
    private static readonly string PlayerMaxHealthValue = "PlayerMaxHealthValue";
    private static readonly string PlayerMaxStaminaValue = "PlayerMaxStaminaValue";
    private static readonly string PlayerCurrentScene = "PlayerCurrentScene";
    private static readonly string PlayerMoneyAmount = "PlayerMoneyAmount";
    private static readonly string PlayerDamageValue = "PlayerDamageValue";
    private static readonly string PlayerDefenceValue = "PlayerDefenceValue";

    // Campi per la gestione dei livelli delle statistiche
    private static readonly string PlayerAttackLevel = "PlayerAttackLevel";
    private static readonly string PlayerStaminaLevel = "PlayerStaminaLevel";
    private static readonly string PlayerDefenceLevel = "PlayerDefenceLevel";
    private static readonly string PlayerHPRegenLevel = "PlayerHPRegenLevel";
    private static readonly string PlayerSPRegenLevel = "PlayerSPRegenLevel";

    // Statistiche iniziali del player
    public float maxHealth = 100;
    public float maxStamina = 100;
    public int PlayerMoney = 0;
    public int atkLevel = 1, staminaLevel = 1, defLevel = 1;
    public int hpRegenLevel = 1, spRegenLevel = 1;
    public float playerDamage = 15f;
    public float playerDefence = 0f;
    public float staminaTookForParry = 10, healthTookForParry = 10;
    public float staminaTookForPerfectParry = 5, healthTookForPerfectParry = 0;
    public float secondsToFullStamina = 8f;
    public double staminaRegenRatio;

    // Statistiche attuali del player
    private float PlayerCurrentHealth, PlayerCurrentStamina;
    private float PlayerCurrentMaxHealth, PlayerCurrentMaxStamina, PlayerCurrentDamage, PlayerCurrentDefence;
    private int CurrentAtkLevel, CurrentStaminaLevel, CurrentDefenseLevel, PlayerCurrentMoney;
    private int CurrentHPRegenLevel, CurrentSPRegenLevel;

    // Altro
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    public Animator animator;
    public static bool isNewGameplay = true;
    public bool isPlayerDead = false; // serve come controllo in PlayerSettings ed anche per far smettere di inseguire il player dai boss una volta morto
    public float staminaRecoveryCooldown; //probabilmente è ancora poco, forse sta da raddoppiare, poi vediamo

    void Awake()
    {
        if (isNewGameplay)
        {
            InitializeNewPlayer();
            isNewGameplay = false;  // Imposta a false dopo l'inizializzazione
        }
        else
        {
            LoadPlayerData();
        }

        staminaRegenRatio = Math.Round(maxStamina / secondsToFullStamina, 2);
        SavePlayerAndScene();
        animator = GetComponent<Animator>();
    }

    private void InitializeNewPlayer()
    {
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        healthBar.value = maxHealth;
        staminaBar.value = maxStamina;

        PlayerCurrentHealth = maxHealth;
        PlayerCurrentStamina = maxStamina;

        PlayerMoney = 0;

        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);
        PlayerPrefs.SetFloat(PlayerDamageValue, playerDamage);
        PlayerPrefs.SetFloat(PlayerDefenceValue, playerDefence);

        PlayerPrefs.SetInt(PlayerAttackLevel, atkLevel);
        PlayerPrefs.SetInt(PlayerStaminaLevel, staminaLevel);
        PlayerPrefs.SetInt(PlayerDefenceLevel, defLevel);
        PlayerPrefs.SetInt(PlayerHPRegenLevel, hpRegenLevel);
        PlayerPrefs.SetInt(PlayerSPRegenLevel, spRegenLevel);

        PlayerPrefs.SetInt(PlayerMoneyAmount, PlayerMoney);

        PlayerPrefs.SetInt(PlayerCurrentScene, 1);
        PlayerPrefs.SetInt(PlayerNewGameplay, -1);
    }

    public void LoadPlayerData()
    {
        PlayerCurrentHealth = PlayerPrefs.GetFloat(PlayerHealthValue);
        PlayerCurrentStamina = PlayerPrefs.GetFloat(PlayerStaminaValue);
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);
        PlayerCurrentDamage = PlayerPrefs.GetFloat(PlayerDamageValue);
        PlayerCurrentDefence = PlayerPrefs.GetFloat(PlayerDefenceValue);
        PlayerCurrentMoney = PlayerPrefs.GetInt(PlayerMoneyAmount);

        CurrentAtkLevel = PlayerPrefs.GetInt(PlayerAttackLevel);
        CurrentStaminaLevel = PlayerPrefs.GetInt(PlayerStaminaLevel);
        CurrentDefenseLevel = PlayerPrefs.GetInt(PlayerDefenceLevel);
        CurrentHPRegenLevel = PlayerPrefs.GetInt(PlayerHPRegenLevel);
        CurrentSPRegenLevel = PlayerPrefs.GetInt(PlayerSPRegenLevel);


        healthBar.maxValue = PlayerCurrentMaxHealth;
        staminaBar.maxValue = PlayerCurrentMaxStamina;

        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
        staminaBar.value = Mathf.Floor(PlayerCurrentStamina);
        playerDamage = PlayerCurrentDamage;

        atkLevel = CurrentAtkLevel;
        staminaLevel = CurrentStaminaLevel;
        defLevel = CurrentDefenseLevel;
        hpRegenLevel = CurrentHPRegenLevel;
        spRegenLevel = CurrentSPRegenLevel;
    }

    public void SavePlayerAndScene()
    {
        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);
        PlayerPrefs.SetFloat(PlayerDamageValue, playerDamage);
        PlayerPrefs.SetFloat(PlayerDefenceValue, playerDefence);

        PlayerPrefs.SetInt(PlayerAttackLevel, atkLevel);
        PlayerPrefs.SetInt(PlayerStaminaLevel, staminaLevel);
        PlayerPrefs.SetInt(PlayerDefenceLevel, defLevel);
        PlayerPrefs.SetInt(PlayerHPRegenLevel, hpRegenLevel);
        PlayerPrefs.SetInt(PlayerSPRegenLevel, spRegenLevel);

        PlayerPrefs.SetInt(PlayerMoneyAmount, PlayerMoney);

        PlayerPrefs.SetInt(PlayerCurrentScene, SceneManager.GetActiveScene().buildIndex);
    }

    public void AfterDeadPlayer()
    {
        PlayerCurrentHealth = PlayerPrefs.GetFloat(PlayerHealthValue);
        PlayerCurrentStamina = PlayerPrefs.GetFloat(PlayerStaminaValue);
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);
        PlayerCurrentDamage = PlayerPrefs.GetFloat(PlayerDamageValue);
        PlayerCurrentDefence = PlayerPrefs.GetFloat(PlayerDefenceValue);
        PlayerCurrentMoney = PlayerPrefs.GetInt(PlayerMoneyAmount);

        CurrentAtkLevel = PlayerPrefs.GetInt(PlayerAttackLevel);
        CurrentStaminaLevel = PlayerPrefs.GetInt(PlayerStaminaLevel);
        CurrentDefenseLevel = PlayerPrefs.GetInt(PlayerDefenceLevel);
        CurrentHPRegenLevel = PlayerPrefs.GetInt(PlayerHPRegenLevel);
        CurrentSPRegenLevel = PlayerPrefs.GetInt(PlayerSPRegenLevel);

        healthBar.value = PlayerCurrentMaxHealth;
        staminaBar.value = PlayerCurrentMaxStamina;

        playerDamage = PlayerCurrentDamage;
        atkLevel = CurrentAtkLevel;
        staminaLevel = CurrentStaminaLevel;
        defLevel = CurrentDefenseLevel;
        hpRegenLevel = CurrentHPRegenLevel;
        spRegenLevel = CurrentSPRegenLevel;

        isPlayerDead = false;

        SavePlayerAndScene();
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            SavePlayerAndScene();
        }
    }

    public void TakeDamage(float amount)
    {
        if (PlayerCurrentHealth > 0)
        {

            PlayerCurrentHealth = Mathf.Max(PlayerCurrentHealth - (amount - playerDefence), 0);
            healthBar.value = Mathf.Floor(PlayerCurrentHealth);
            Debug.Log(healthBar.value);

            animator.SetTrigger("hurt");
        }
        else
        {
            Debug.Log("You died!");
            isPlayerDead = true;
            GameObject.FindGameObjectWithTag("DeathScreen").SetActive(true);
            Debug.Log("Menu morte attivo: " + GameObject.FindGameObjectWithTag("DeathScreen").activeSelf);
            // ...
        }
    }

    public void UseStamina(float amount)
    {
        PlayerCurrentStamina = Mathf.Max(PlayerCurrentStamina - amount, 0);
        staminaBar.value = Mathf.Floor(PlayerCurrentStamina);
    }

    public void HealPlayer(float amount)
    {
        PlayerCurrentHealth = Mathf.Min(PlayerCurrentHealth + amount, maxHealth);
        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
    }

    public float GetPlayerCurrentHealth()
    {
        return PlayerCurrentHealth;
    }

    public float GetPlayerCurrentStamina()
    {
        return PlayerCurrentStamina;
    }

    public bool RecoverStamina()
    {
        if (PlayerCurrentStamina >= PlayerCurrentMaxStamina) { return true; }
        PlayerCurrentStamina += (float)(staminaRegenRatio * Time.deltaTime);
        staminaBar.value = PlayerCurrentStamina;
        return false;
    }
}
