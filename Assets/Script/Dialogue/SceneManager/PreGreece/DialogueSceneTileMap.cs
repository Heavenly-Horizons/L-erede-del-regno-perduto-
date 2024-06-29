using UnityEngine;

namespace Script.Dialogue.SceneManager.PreGreece {
    public class DialogueSceneTileMap : MonoBehaviour {
        public static byte k = 0;
        [SerializeReference] private DialogueSystem dialogueSystem;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        void Start()
        {
            Reset();
            k = 0;
        }

        private void Update() {
            switch (k) {
                case 0:
                    dialogueSystem.FirstDialogue();
                    break;
            }

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
            //bossFight
        }
    }
}
