using UnityEngine;

namespace Script.Dialogue.SceneManager.PreAsgard {
    internal class DialogueTriggerStart : MonoBehaviour {
        [SerializeField] private DialogueSceneTileMap dialogueSceneTileMap;
        [SerializeField] private GameObject dialoguePanel;

        private void Update() {
            dialoguePanel.SetActive(dialogueSceneTileMap.start);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            switch (other.gameObject.CompareTag("Player")) {
                case true:
                    dialogueSceneTileMap.start = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                    break;
            }
        }
    }
}