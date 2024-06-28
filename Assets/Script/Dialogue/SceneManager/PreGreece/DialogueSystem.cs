using System.Collections;
using UnityEngine;

namespace Script.Dialogue.SceneManager.PreGreece {
    public class DialogueSystem : MonoBehaviour {
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerNarrator;
        [SerializeField] private DialogueTrigger dialogueTriggerFreya;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private CanvasGroup blackPanel;
        public int i;
        public byte j;
        public bool isEnded;

        public void ResetDialogueTrigger() {
            dialogueTriggerNarrator.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
            dialogueTriggerFreya.dialogue.isEnded = false;
        }

        private void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences) {
            dialogueTrigger.dialogue.sentences = sentences;
        }

        public void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues) {
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
                //schermo nero
                case 0:
                    //disattiva la gravità
                    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    player.CanNotMove();
                    blackPanel.alpha = 1;
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Dannazione",
                        "Non l’ha nemmeno risparmiato",
                        "Una volta fratello e sorella...",
                        "E poi nemici",
                        "Che sia solo una rincorsa al potere?"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerNarrator, new[] {
                        "Dopo un brevissimo viaggio i due si ritrovano a Sparta"
                    });
                    break;

                //si vedono i player
                case 2:
                    StartCoroutine(FadeOut());
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Bene, siamo arrivati in Grecia",
                        "Dobbiamo trovare un buon fabbro che mi possa riparare lo scudo"
                    });
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[] {
                        "Proviamo da questa parte, dobbiamo muoverci ragazzo"
                    });
                    break;
                default:
                    player.CanMove();
                    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }

        private IEnumerator FadeOut() {
            // Calcola quanto decrementare alpha in ogni frame basandosi sulla durata del fade
            float fadeStep = blackPanel.alpha / 1f * Time.deltaTime;

            // Continua a eseguire finché alpha è maggiore di 0
            while (blackPanel.alpha > 0) {
                // Decrementa il valore di alpha
                blackPanel.alpha -= fadeStep;

                // Aspetta il prossimo frame
                yield return null;
            }

            // Assicurati che alpha sia esattamente 0 alla fine
            blackPanel.alpha = 0;
        }
    }
}