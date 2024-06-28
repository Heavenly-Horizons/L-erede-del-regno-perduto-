using UnityEngine;

namespace Script.Dialogue.SceneManager.PreAsgard {
    internal class DialogueSceneTileMap : DialogueSceneTileMapAbstract {
        public static byte k = 0;
        [SerializeField] protected DialogueSystem dialogueSystem;
        public bool start;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        protected override void Update() {
            if (start) {
                switch (k) {
                    case 0:
                        barFalse();
                        dialogueSystem.FirstDialogue();
                        break;
                    case 1:
                        start = true;
                        break;
                }

                if (dialogueSystem.isEnded)
                    //per resettare
                    Reset();
                //bossFight
            }
        }
    }
}