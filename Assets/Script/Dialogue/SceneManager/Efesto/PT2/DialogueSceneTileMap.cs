using UnityEngine;

namespace Script.Dialogue.SceneManager.Efesto.PT2
{
    public class DialogueSceneTileMap : MonoBehaviour
    {
        public static byte K = 0;
        [SerializeField] private DialogueSystem dialogueSystem;

        private void Reset()
        {
            dialogueSystem.ResetDialogueTrigger();
        }

        private void Update()
        {
            switch (K)
            {
                case 0:
                    dialogueSystem.FirstDialogue();
                    break;
            }

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
        }
    }
}