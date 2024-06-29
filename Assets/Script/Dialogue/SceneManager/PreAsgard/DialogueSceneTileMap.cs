using UnityEngine;

namespace Script.Dialogue.SceneManager.PreAsgard {
    internal class DialogueSceneTileMap : DialogueSceneTileMapAbstract {
        public static byte k = 0;
        [SerializeReference] public DialogueSystem dialogueSystem;
        public bool start;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        void Awake()
        {
            Reset();
            k = 0;
        }

        protected override void Update() {
            if (start) {
                switch (k) {
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
                    //per resettare
                    Reset();
                //bossFight
            }
        }
    }
}
