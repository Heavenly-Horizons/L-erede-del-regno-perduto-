using UnityEngine;

namespace Script.Dialogue.SceneManager
{
    public abstract class DialogueSceneTileMapAbstract : MonoBehaviour
    {
        private void Awake()
        {
            var HealthBar = GameObject.Find("HealthBar");
            HealthBar.SetActive(true);

            var StaminaBar = GameObject.Find("StaminaBar");
            StaminaBar.SetActive(true);
        }

        public abstract void Update();
    }
}