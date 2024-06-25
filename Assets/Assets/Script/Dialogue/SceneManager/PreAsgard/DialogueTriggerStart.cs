using UnityEngine;

namespace Script.Dialogue.SceneManager.PreAsgard
{
    internal class DialogueTriggerStart : MonoBehaviour
    {
        [SerializeField] private DialogueSceneTileMap dialogueSceneTileMap;
        [SerializeField] private GameObject dialoguePanel;

        private void Update()
        {
            switch (dialogueSceneTileMap.GetComponent<DialogueSceneTileMap>().start)
            {
                case false:
                    dialoguePanel.SetActive(false);
                    break;
                case true:
                    dialoguePanel.SetActive(true);
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.CompareTag("Player"))
            {
                case true:
                    dialogueSceneTileMap.start = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                    break;
            }
        }
    }
}