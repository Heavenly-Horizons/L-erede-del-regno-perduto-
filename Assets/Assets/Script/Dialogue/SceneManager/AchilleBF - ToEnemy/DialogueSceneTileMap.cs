using UnityEngine;

namespace Script.Dialogue.SceneManager.AchilleBF___ToEnemy
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
            //se la vita è maggiore di 10 e i dialoghi non sono finiti
            if (dialogueSystem.achilleMovement.life > 10 && k == 0)
                //dialoghi
                dialogueSystem.FirstDialogue();
            //se la vita è inferiore o uguale a 10
            else if (dialogueSystem.achilleMovement.life <= 10 && k == 1)
                // nel caso la vita di achille è inferiore a 10, la riporta a 10.
                // questo per fare la scena finale
                //if (dialogueSystem.achille_Movement.life < 10) dialogueSystem.achille_Movement.life = 10;
                //dialoghi
                dialogueSystem.SecondDialogue();

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
            //bossFight
        }
    }
}