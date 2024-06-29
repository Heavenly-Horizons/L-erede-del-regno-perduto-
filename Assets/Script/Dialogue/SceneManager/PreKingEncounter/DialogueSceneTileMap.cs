using UnityEngine;

namespace Script.Dialogue.SceneManager.PreKingEncounter {
    public class DialogueSceneTileMap : DialogueSceneTileMapAbstract {
        public static byte k = 0;
        [SerializeReference] private DialogueSystem dialogueSystem;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        void Start() {
            Reset();
            k = 0;
        }

        protected override void Update() {
            switch (k) {
                case 0:
                    barFalse();
                    dialogueSystem.FirstDialogue();
                    break;
                default:
                    barTrue();
                    break;
            }

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
            //bossFight
        }
    }
}
