using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;


    private void Awake() {
        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Debug.Log(playerStats != null
            ? "GameObject.FindGameObjectWithTag(\"Player\").GetComponent<PlayerStats>() in PlayerSettings istanziato"
            : "GameObject.FindGameObjectWithTag(\"Player\").GetComponent<PlayerStats>() in PlayerSettings non istanziato");
        if (SceneManager.GetActiveScene().buildIndex != 0 && !playerStats.isPlayerDead)
            playerStats.LoadPlayerData();
        else
            playerStats.AfterDeadPlayer();
    }
}