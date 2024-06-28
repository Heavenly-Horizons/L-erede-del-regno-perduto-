using UnityEngine;

namespace Script.Dialogue.SceneManager.Tyr {
    public class DialogueSceneTileMap : MonoBehaviour {
        public static byte K = 0;
        private static readonly int Defeat = Animator.StringToHash("Defeat");
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private Boss tyr;
        [SerializeField] private GameObject toNextScene;
        private BossHealth _bossHealth;

        private void Reset() {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        private void Start() {
            if (tyr != null) _bossHealth = tyr.GetComponent<BossHealth>();
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
                GameObject.FindGameObjectWithTag("Nemico").GetComponent<Animator>().SetTrigger(Defeat);
                dialogueSystem.SecondDialogue();
                toNextScene.SetActive(true);
            }

            if (dialogueSystem.isEnded) {
                tyr.isDialogueEnded = true;
                //per resettare
                Reset();
            }
            //bossFight
        }
    }
}