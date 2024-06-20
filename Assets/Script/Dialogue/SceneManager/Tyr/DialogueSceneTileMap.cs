using UnityEngine;

namespace Script.Dialogue.SceneManager.Tyr
{
    public class DialogueSceneTileMap : MonoBehaviour
    {
        public static byte k = 0;
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private readonly TyrMovement tyr_Movement = new();

        private void Reset()
        {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        private void Update()
        {
            //se la vita è maggiore di 10 e i dialoghi non sono finiti
            if (tyr_Movement.life > 10 && k == 0)
                //dialoghi
                dialogueSystem.FirstDialogue();
            //se la vita è inferiore o uguale a 10
            else if (tyr_Movement.life <= 10 && k == 1)
                //dialoghi
                dialogueSystem.SecondDialogue();

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
            //bossFight
        }

        private class TyrMovement
        {
            public readonly float life = 100;
        }
    }
}