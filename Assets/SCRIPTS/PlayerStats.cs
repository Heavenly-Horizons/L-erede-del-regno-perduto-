<<<<<<< HEAD
using System;
=======
using TMPro;
>>>>>>> main
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
<<<<<<< HEAD
=======
    // Campi per la gestione dei valori del player
>>>>>>> main
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";
    private static readonly string PlayerHealthValue = "PlayerHealthValue";
    private static readonly string PlayerStaminaValue = "PlayerStaminaValue";
    private static readonly string PlayerMaxHealthValue = "PlayerMaxHealthValue";
    private static readonly string PlayerMaxStaminaValue = "PlayerMaxStaminaValue";
    private static readonly string PlayerCurrentScene = "PlayerCurrentScene";
<<<<<<< HEAD

    private float maxHealth = 100;
    private float maxStamina = 5;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;

    private float PlayerCurrentHealth, PlayerCurrentStamina;
    private float PlayerCurrentMaxHealth, PlayerCurrentMaxStamina;

    private void Awake()
    {
        int PlayerNewGameplayInt = PlayerPrefs.GetInt(PlayerNewGameplay);
        Debug.Log(PlayerNewGameplayInt);

        if (PlayerNewGameplayInt == 0)
        {
            InitializeNewPlayer();
        }
        else
        {
            LoadPlayerData();
        }
        SavePlayerHealthStaminaScene();
    }
=======
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
    public float staminaRegenRatio;
    
    // Statistiche attuali del player
    private float PlayerCurrentHealth, PlayerCurrentStamina;
    private float PlayerCurrentMaxHealth, PlayerCurrentMaxStamina, PlayerCurrentDamage, PlayerCurrentDefence;
    private int CurrentAtkLevel, CurrentStaminaLevel, CurrentDefenseLevel;
    private int CurrentHPRegenLevel, CurrentSPRegenLevel;

    // Altro
    private bool dead;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    public Animator animator;
    public static bool isNewGameplay = true;




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

    staminaRegenRatio = maxStamina / secondsToFullStamina;
    SavePlayerAndScene();
    animator = GetComponent<Animator>();
}
>>>>>>> main

    private void InitializeNewPlayer()
    {
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        healthBar.value = maxHealth;
        staminaBar.value = maxStamina;

        PlayerCurrentHealth = maxHealth;
        PlayerCurrentStamina = maxStamina;

<<<<<<< HEAD
=======
        PlayerMoney = 0;

>>>>>>> main
        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);
<<<<<<< HEAD
=======
        PlayerPrefs.SetFloat(PlayerDamageValue, playerDamage);
        PlayerPrefs.SetFloat(PlayerDefenceValue, playerDefence);

        PlayerPrefs.SetInt(PlayerAttackLevel, atkLevel);
        PlayerPrefs.SetInt(PlayerStaminaLevel, staminaLevel);
        PlayerPrefs.SetInt(PlayerDefenceLevel, defLevel);
        PlayerPrefs.SetInt(PlayerHPRegenLevel, hpRegenLevel);
        PlayerPrefs.SetInt(PlayerSPRegenLevel, spRegenLevel);

        PlayerPrefs.SetInt(PlayerMoneyAmount, PlayerMoney);
>>>>>>> main

        PlayerPrefs.SetInt(PlayerCurrentScene, 1);
        PlayerPrefs.SetInt(PlayerNewGameplay, -1);
    }

<<<<<<< HEAD
    private void LoadPlayerData()
=======
    public void LoadPlayerData()
>>>>>>> main
    {
        PlayerCurrentHealth = PlayerPrefs.GetFloat(PlayerHealthValue);
        PlayerCurrentStamina = PlayerPrefs.GetFloat(PlayerStaminaValue);
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);
<<<<<<< HEAD
=======
        PlayerCurrentDamage = PlayerPrefs.GetFloat(PlayerDamageValue);
        PlayerCurrentDefence = PlayerPrefs.GetFloat(PlayerDefenceValue);

        CurrentAtkLevel = PlayerPrefs.GetInt(PlayerAttackLevel);
        CurrentStaminaLevel = PlayerPrefs.GetInt(PlayerStaminaLevel);
        CurrentDefenseLevel = PlayerPrefs.GetInt(PlayerDefenceLevel);
        CurrentHPRegenLevel = PlayerPrefs.GetInt(PlayerHPRegenLevel);
        CurrentSPRegenLevel = PlayerPrefs.GetInt(PlayerSPRegenLevel);

>>>>>>> main

        healthBar.maxValue = PlayerCurrentMaxHealth;
        staminaBar.maxValue = PlayerCurrentMaxStamina;

        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
        staminaBar.value = Mathf.Floor(PlayerCurrentStamina);
<<<<<<< HEAD
    }

    public void SavePlayerHealthStaminaScene()
=======
        playerDamage = PlayerCurrentDamage;

        atkLevel = CurrentAtkLevel;
        staminaLevel = CurrentStaminaLevel;
        defLevel = CurrentDefenseLevel;
        hpRegenLevel = CurrentHPRegenLevel;
        spRegenLevel = CurrentSPRegenLevel;
    }

    public void SavePlayerAndScene()
>>>>>>> main
    {
        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);
<<<<<<< HEAD
=======
        PlayerPrefs.SetFloat(PlayerDamageValue, playerDamage);
        PlayerPrefs.SetFloat(PlayerDefenceValue, playerDefence);

        PlayerPrefs.SetInt(PlayerAttackLevel, atkLevel);
        PlayerPrefs.SetInt(PlayerStaminaLevel, staminaLevel);
        PlayerPrefs.SetInt(PlayerDefenceLevel, defLevel);
        PlayerPrefs.SetInt(PlayerHPRegenLevel, hpRegenLevel);
        PlayerPrefs.SetInt(PlayerSPRegenLevel, spRegenLevel);

        PlayerPrefs.SetInt(PlayerMoneyAmount, PlayerMoney);

>>>>>>> main
        PlayerPrefs.SetInt(PlayerCurrentScene, SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
<<<<<<< HEAD
            SavePlayerHealthStaminaScene();
=======
            SavePlayerAndScene();
>>>>>>> main
        }
    }

    private void FixedUpdate()
    {
        if (PlayerCurrentHealth < 1)
        {
<<<<<<< HEAD
            Die();
=======
         //   Die()
>>>>>>> main
        }
    }

    public void TakeDamage(float amount)
    {
        PlayerCurrentHealth = Mathf.Max(PlayerCurrentHealth - amount, 0);
        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
<<<<<<< HEAD

        if (PlayerCurrentHealth <= 0)
        {
            Die();
        }
=======
        Debug.Log(healthBar.value);

        if (PlayerCurrentHealth > 0){
            animator.SetTrigger("hurt");
        }
        else{
            if(!dead){
                Die();
            }
        }
        
>>>>>>> main
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

    private void Die()
    {
<<<<<<< HEAD
        //Debug.Log("You died!");
=======
        //Debug.Log("You died!")
>>>>>>> main
        // Play death animation
        // Activate death screen
        // ...
    }

    public float GetPlayerCurrentHealth()
    {
        return PlayerCurrentHealth;
    }

    public float GetPlayerCurrentStamina()
    {
        return PlayerCurrentStamina;
    }
}
