using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class PlayerStats : MonoBehaviour {
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
    public static bool isNewGameplay = true;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int PlayerDead = Animator.StringToHash("playerDead");

    // Statistiche iniziali del player
    public float maxHealth = 100;
    public float maxStamina = 100;
    public int PlayerMoney;
    public int atkLevel = 1, staminaLevel = 1, defLevel = 1;
    public int hpRegenLevel = 1, spRegenLevel = 1;
    public float playerDamage = 15f;
    public float playerDefence;
    public float staminaTookForParry = 10, healthTookForParry = 10;
    public float staminaTookForPerfectParry = 5, healthTookForPerfectParry;
    public float secondsToFullStamina = 8f;
    public double staminaRegenRatio;
    public bool isInvulnerable;

    // Altro
    public Slider healthBar;
    public Slider staminaBar;
    public Animator animator;
    [SerializeField] private GameObject deathScreen;

    public bool
        isPlayerDead; // serve come controllo in PlayerSettings ed anche per far smettere di inseguire il player dai boss una volta morto

    public float staminaRecoveryCooldown; //probabilmente è ancora poco, forse sta da raddoppiare, poi vediamo
    private int CurrentAtkLevel, CurrentStaminaLevel, CurrentDefenseLevel, PlayerCurrentMoney;
    private int CurrentHPRegenLevel, CurrentSPRegenLevel;
    private Animator deathScreenAnimator;

    // Statistiche attuali del player
    private float PlayerCurrentHealth, PlayerCurrentStamina;
    private float PlayerCurrentMaxHealth, PlayerCurrentMaxStamina, PlayerCurrentDamage, PlayerCurrentDefence;

    private void Awake() {
        if (isNewGameplay) {
            InitializeNewPlayer();
            isNewGameplay = false; // Imposta a false dopo l'inizializzazione
        }
        else {
            LoadPlayerData();
        }

        staminaRegenRatio = Math.Round(maxStamina / secondsToFullStamina, 2);
        SavePlayerAndScene();
        animator = GetComponent<Animator>();
        deathScreen.SetActive(false);
        deathScreenAnimator = deathScreen.GetComponent<Animator>();
    }

    private void OnApplicationFocus(bool focusStatus) {
        if (!focusStatus) SavePlayerAndScene();
    }

    private void InitializeNewPlayer() {
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        healthBar.value = maxHealth;
        staminaBar.value = maxStamina;

        PlayerCurrentHealth = maxHealth;
        PlayerCurrentMaxHealth = maxHealth;
        PlayerCurrentStamina = maxStamina;
        PlayerCurrentMaxStamina = maxStamina;

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

    public void LoadPlayerData() {
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
        PlayerMoney = PlayerCurrentMoney;

        atkLevel = CurrentAtkLevel;
        staminaLevel = CurrentStaminaLevel;
        defLevel = CurrentDefenseLevel;
        hpRegenLevel = CurrentHPRegenLevel;
        spRegenLevel = CurrentSPRegenLevel;
    }

    public void SavePlayerAndScene() {
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

    public void AfterDeadPlayer() {
        PlayerCurrentMaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthValue);
        PlayerCurrentMaxStamina = PlayerPrefs.GetFloat(PlayerMaxStaminaValue);
        PlayerCurrentHealth = PlayerCurrentMaxHealth;
        PlayerCurrentStamina = PlayerCurrentMaxStamina;
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

    public void TakeDamage(float amount) {
        if (isInvulnerable) return;

        if (PlayerCurrentHealth - (amount + playerDefence) > 0) {
            PlayerCurrentHealth = PlayerCurrentHealth - (amount + playerDefence);
            healthBar.value = Mathf.Floor(PlayerCurrentHealth);
            animator.SetTrigger(Hurt);
        }
        else {
            PlayerCurrentHealth = 0;
            healthBar.value = PlayerCurrentHealth;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Player_Attack>().enabled = false;
            isPlayerDead = true;
            animator.SetTrigger(Die);
            deathScreen.SetActive(true);
            deathScreenAnimator.SetTrigger(PlayerDead);
            gameObject.tag = "Untagged";
        }
    }

    private IEnumerator PlayDeathAnimation() {
        yield return new WaitForSeconds(1f);
    }

    public void UseStamina(float amount) {
        PlayerCurrentStamina = Mathf.Max(PlayerCurrentStamina - amount, 0);
        staminaBar.value = Mathf.Floor(PlayerCurrentStamina);
    }

    public void HealPlayer(float amount) {
        PlayerCurrentHealth = Mathf.Min(PlayerCurrentHealth + amount, maxHealth);
        healthBar.value = Mathf.Floor(PlayerCurrentHealth);
    }

    public float GetPlayerCurrentHealth() {
        return PlayerCurrentHealth;
    }

    public float GetPlayerCurrentStamina() {
        return PlayerCurrentStamina;
    }

    public bool RecoverStamina() {
        if (PlayerCurrentStamina >= PlayerCurrentMaxStamina) return true;
        PlayerCurrentStamina += (float) (staminaRegenRatio * Time.deltaTime);
        staminaBar.value = PlayerCurrentStamina;
        return false;
    }
}
