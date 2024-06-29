using UnityEngine;

namespace Script.Dialogue.SceneManager.Tyr {
    public class DialogueSceneTileMap : MonoBehaviour {
        public static byte K = 0;
        private static readonly int Defeat = Animator.StringToHash("Defeat");
        [SerializeReference] public DialogueSystem dialogueSystem;
        private GameObject toNextScene;
        private BossHealth _bossHealth;
        private bool isDefeat = false;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        void Start() {
            Reset();
            K = 0;
            _bossHealth = GameObject.FindGameObjectWithTag("Nemico").GetComponent<BossHealth>();
            toNextScene = GameObject.FindGameObjectWithTag("toNextLevel");
        }

        private void Update() {
            //se la vita è maggiore di 10 e i dialoghi non sono finiti
            if (_bossHealth.bossHealth > 10 && K == 0) {
                //dialoghi
                dialogueSystem.FirstDialogue();
                toNextScene.SetActive(false);
            }
            //se la vita è inferiore o uguale a 10
            else if (_bossHealth.bossHealth <= 10 && K == 1) {
                //dialoghi
                if(!isDefeat){
                    dialogueSystem.SecondDialogue();
                    GameObject.FindGameObjectWithTag("Nemico").GetComponent<Animator>().SetTrigger(Defeat);
                    isDefeat = true;
                }
                toNextScene.SetActive(true);
            }

            if (dialogueSystem.isEnded) {
                _bossHealth.GetComponent<Boss>().isDialogueEnded = true;
                //per resettare
                Reset();
            }
            //bossFight
        }
    }
}
