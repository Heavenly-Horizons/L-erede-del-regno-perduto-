using UnityEngine;

namespace Script.Dialogue.SceneManager.KingEncounter {
    public class DialogueTriggerStart : MonoBehaviour {
        [SerializeField] private DialogueSceneTileMap dialogueSceneTileMap;
        [SerializeField] private GameObject dialoguePanel;

        private void Update() {
            dialoguePanel.SetActive(dialogueSceneTileMap.start);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                dialogueSceneTileMap.start = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}