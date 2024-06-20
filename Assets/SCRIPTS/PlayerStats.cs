using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";
    private static readonly string PlayerHealthValue = "PlayerHealthValue";
    private static readonly string PlayerStaminaValue = "PlayerStaminaValue";
    private static readonly string PlayerMaxHealthValue = "PlayerMaxHealthValue";
    private static readonly string PlayerMaxStaminaValue = "PlayerMaxStaminaValue";
    private static readonly string PlayerCurrentScene = "PlayerCurrentScene";

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

    private void InitializeNewPlayer()
    {
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        healthBar.value = maxHealth;
        staminaBar.value = maxStamina;

        PlayerCurrentHealth = maxHealth;
        PlayerCurrentStamina = maxStamina;

        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);

        PlayerPrefs.SetInt(PlayerCurrentScene, 1);
        PlayerPrefs.SetInt(PlayerNewGameplay, -1);
    }

    private void LoadPlayerData()
    {
        PlayerCurrentHealth = PlayerPrefs.GetFloat(PlayerHealthValue);
        PlayerCurrentStamina = PlayerPrefs.GetFloat(PlayerStaminaValue);
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);

        healthBar.maxValue = PlayerCurrentMaxHealth;
        staminaBar.maxValue = PlayerCurrentMaxStamina;

        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
        staminaBar.value = Mathf.Floor(PlayerCurrentStamina);
    }

    public void SavePlayerHealthStaminaScene()
    {
        PlayerPrefs.SetFloat(PlayerHealthValue, healthBar.value);
        PlayerPrefs.SetFloat(PlayerStaminaValue, staminaBar.value);
        PlayerPrefs.SetFloat(PlayerMaxHealthValue, healthBar.maxValue);
        PlayerPrefs.SetFloat(PlayerMaxStaminaValue, staminaBar.maxValue);
        PlayerPrefs.SetInt(PlayerCurrentScene, SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            SavePlayerHealthStaminaScene();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerCurrentHealth < 1)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        PlayerCurrentHealth = Mathf.Max(PlayerCurrentHealth - amount, 0);
        healthBar.value = Mathf.Floor(PlayerCurrentHealth);

        if (PlayerCurrentHealth <= 0)
        {
            Die();
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

    private void Die()
    {
        //Debug.Log("You died!");
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
