using UnityEngine;

namespace Script.Dialogue.SceneManager
{
    public abstract class DialogueSceneTileMapAbstract : MonoBehaviour
    {
        private void Awake()
        {
            var healthBar = GameObject.Find("HealthBar");
            healthBar.SetActive(true);

            var staminaBar = GameObject.Find("StaminaBar");
            staminaBar.SetActive(true);
        }

        public abstract void Update();
    }
}