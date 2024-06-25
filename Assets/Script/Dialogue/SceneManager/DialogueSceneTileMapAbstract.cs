using UnityEngine;

namespace Script.Dialogue.SceneManager
{
    public abstract class DialogueSceneTileMapAbstract : MonoBehaviour
    {
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
    }
}