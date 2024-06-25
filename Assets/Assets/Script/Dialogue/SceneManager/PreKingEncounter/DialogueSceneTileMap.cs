using UnityEngine;

namespace Script.Dialogue.SceneManager.PreKingEncounter
{
    public class DialogueSceneTileMap : MonoBehaviour
    {
        public static byte k = 0;
        [SerializeField] private DialogueSystem dialogueSystem;

        private void Reset()
        {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        private void Update()
        {
            switch (k)
            {
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