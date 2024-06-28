using UnityEngine;

namespace Script.Dialogue.SceneManager.AchilleBF___ToEnemy {
    public class DialogueSceneTileMap : MonoBehaviour {
        public static byte k = 0;
        private static readonly int DialogueEnded = Animator.StringToHash("DialogueEnded");
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private Animator animator;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        private void Update() {
            //se la vita è maggiore di 10 e i dialoghi non sono finiti
            if (dialogueSystem.bossHealthScript.bossHealth > 10 && k == 0)
            {
                //dialoghi
                dialogueSystem.FirstDialogue();
            }
            //se la vita è inferiore o uguale a 10
            else if (dialogueSystem.bossHealthScript.bossHealth <= 10 && k == 1) {
                //dialoghi
                GameObject.FindGameObjectWithTag("Nemico").GetComponent<Animator>().SetTrigger("Defeat");
                dialogueSystem.SecondDialogue();
            }

            if (dialogueSystem.isEnded)
                //per resettare
                Reset();
            //bossFight
        }
    }
}
