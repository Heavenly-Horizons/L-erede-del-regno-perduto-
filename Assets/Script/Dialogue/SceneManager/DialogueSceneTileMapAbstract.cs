using UnityEngine;

namespace Script.Dialogue.SceneManager
{
    public abstract class DialogueSceneTileMapAbstract : MonoBehaviour
    {
<<<<<<< HEAD
        private void Awake()
        {
            var healthBar = GameObject.Find("HealthBar");
            healthBar.SetActive(true);

            var staminaBar = GameObject.Find("StaminaBar");
            staminaBar.SetActive(true);
        }

        public abstract void Update();
=======
        protected GameObject healthBar;
        protected GameObject staminaBar;

        private void Awake()
        {
            healthBar.SetActive(false);
            staminaBar.SetActive(false);
        }

        public abstract void Update();

        protected void barTrue()
        {
            healthBar.SetActive(true);
            staminaBar.SetActive(true);
        }
>>>>>>> main
    }
}