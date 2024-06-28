using UnityEngine;

namespace Script.Dialogue.SceneManager.Tyr {
    public class DialogueSceneTileMap : MonoBehaviour {
        public static byte k = 0;
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private Boss tyr;
        BossHealth bossHealth;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        void Start(){
            if(tyr != null){
                bossHealth = tyr.GetComponent<BossHealth>();
            }
        }

        private void Update() {
            //se la vita è maggiore di 10 e i dialoghi non sono finiti
            if (bossHealth.bossHealth > 10 && k == 0){
                //dialoghi
                dialogueSystem.FirstDialogue();
                GameObject.Find("toNextScene").SetActive(false);
            }
            //se la vita è inferiore o uguale a 10
            else if (bossHealth.bossHealth <= 10 && k == 1){
                //dialoghi
                GameObject.FindGameObjectWithTag("Nemico").GetComponent<Animator>().SetTrigger("Defeat");
                dialogueSystem.SecondDialogue();
                GameObject.Find("toNextScene").SetActive(true);
            }

            if (dialogueSystem.isEnded){
                tyr.isDialogueEnded = true;
                //per resettare
                Reset();
            }
            //bossFight
        }
    }
}
