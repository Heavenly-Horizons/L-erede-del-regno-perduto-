using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
<<<<<<< HEAD
    private static readonly string PlayerHealthValue = "PlayerHealtValue";
    private static readonly string PlayerStaminaValue = "PlayerStaminaValue";
    private static readonly string PlayerMaxHealthValue = "PlayerMaxHealtValue";
    private static readonly string PlayerMaxStaminaValue = "PlayerMaxStaminaValue";

    private float PlayerCurrentHealth, PlayerCurrentStamina;
    private float PlayerCurrentMaxHealth, PlayerCurrentMaxStamina;

=======
>>>>>>> main
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;



    void Awake()
    {
<<<<<<< HEAD
        if(SceneManager.GetActiveScene().buildIndex != 0){
            ContinueSettings();
        }
    }

    public void ContinueSettings()
    {
        PlayerCurrentHealth = PlayerPrefs.GetFloat(PlayerHealthValue);
        PlayerCurrentStamina = PlayerPrefs.GetFloat(PlayerStaminaValue);
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);

        healthBar.maxValue = PlayerCurrentMaxHealth;
        staminaBar.maxValue = PlayerCurrentMaxStamina;

        healthBar.value = (float)Math.Floor(PlayerCurrentHealth);
        staminaBar.value = (float)Math.Floor(PlayerCurrentStamina);
    }
=======
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (SceneManager.GetActiveScene().buildIndex != 0){
            playerStats.LoadPlayerData();
        }
    }
>>>>>>> main
}
