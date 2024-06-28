using UnityEngine;

namespace Script.Dialogue.SceneManager.KingEncounter {
    public class DialogueSceneTileMap : DialogueSceneTileMapAbstract {
        public static byte K = 0;
        [SerializeField] protected DialogueSystem dialogueSystem;
        public bool start;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        protected override void Update() {
            if (start) {
                switch (K) {
                    case 0:
                        barFalse();
                        dialogueSystem.FirstDialogue();
                        break;
                    default:
                        barTrue();
                        start = false;
                        break;
                }

                if (dialogueSystem.isEnded)
                    Reset();
            }
        }
    }
}