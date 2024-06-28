using UnityEngine;

namespace Script.Dialogue.SceneManager {
    public abstract class DialogueSceneTileMapAbstract : MonoBehaviour {
        [SerializeField] protected GameObject healthBar;
        [SerializeField] protected GameObject staminaBar;
        [SerializeField] protected GameObject coinCount;
        [SerializeField] protected GameObject coinImage;

        protected abstract void Update();

        protected void barFalse() {
            healthBar.SetActive(false);
            staminaBar.SetActive(false);
            coinCount.SetActive(false);
            coinImage.SetActive(false);
        }

        protected void barTrue() {
            healthBar.SetActive(true);
            staminaBar.SetActive(true);
            coinCount.SetActive(true);
            coinImage.SetActive(true);
        }
    }
}