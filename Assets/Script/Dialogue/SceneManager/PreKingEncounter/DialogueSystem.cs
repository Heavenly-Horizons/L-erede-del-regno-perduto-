using UnityEngine;
using UnityEngine.Playables;

namespace Script.Dialogue.SceneManager.PreKingEncounter {
    public class DialogueSystem : MonoBehaviour {
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerNarrator;
        [SerializeField] private PlayableDirector timeline;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject staminaBar;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private Rigidbody2D playerRb;
        public int i;
        public byte j;
        public bool isEnded;

        private void Awake() {
            healthBar.SetActive(false);
            staminaBar.SetActive(false);
        }

        public void ResetDialogueTrigger() {
            dialogueTriggerNarrator.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
        }

        private void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences) {
            dialogueTrigger.dialogue.sentences = sentences;
        }

        private void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues) {
            if (j == 0) {
                j++;
                LoadNewDialogue(dialogueTrigger, dialogues);
                dialogueTrigger.TriggerDialogue();
                dialogueTrigger.dialogue.isEnded = false;
                isEnded = false;
            }

            if (dialogueTrigger.dialogue.isEnded) {
                i++;
                j = 0;
            }
        }

        public void FirstDialogue() {
            switch (i) {
                case 0:
                    //disattiva la gravità
                    playerRb.bodyType = RigidbodyType2D.Static;
                    player.CanNotMove();
                    timeline.enabled = false;
                    HandleDialogue(ref i, ref j, dialogueTriggerNarrator, new[] {
                        "Dopo numerose battaglie e altrettante sconfitte, il villaggio di Sant’Elia è andato perduto",
                        "Ormai esiliati dalle loro stesse mura, il popolo di Sant’Elia si rifugia nella foresta"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerNarrator, new[] {
                        "Dalle macerie, esce stremato e quasi del tutto sconfitto, un cavaliere",
                        "Il suo nome è Finn"
                    });
                    break;
                case 2:
                    timeline.enabled = true;
                    timeline.Play();
                    healthBar.SetActive(true);
                    staminaBar.SetActive(true);
                    i++;
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerNarrator, new[] {
                        "Ricevuta la comunicazione dal proprio re, Finn deve dirigersi alla sua tenda"
                    });
                    break;
                case 4:

                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "E’ arrivata la convocazione dal Re, chissà cosa vorrà da me"
                    });
                    break;
                //inizio tutorial
                default:
                    //attiva la gravità
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    player.CanMove();
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }
    }
}