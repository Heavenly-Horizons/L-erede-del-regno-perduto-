using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;



    void Awake()
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (SceneManager.GetActiveScene().buildIndex != 0 && !playerStats.isPlayerDead){
            playerStats.LoadPlayerData(); 
        }else{
            playerStats.AfterDeadPlayer();
        }
    }
}
