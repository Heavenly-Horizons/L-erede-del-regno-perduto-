using System.Collections;
using UnityEngine;

namespace Script.Dialogue.SceneManager.AchilleBF___ToEnemy {
    public class DialogueSystem : MonoBehaviour {
        [SerializeField] public BossHealth bossHealthScript;
        [SerializeField] private DialogueTrigger dialogueTriggerAchille;
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private Rigidbody2D finnRb;
        [SerializeField] private PlayerMovement finnMovement;

        public int i;
        public byte j;
        public bool isEnded;

        public void ResetDialogueTrigger() {
            dialogueTriggerAchille.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
        }

        private static void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences) {
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
                    //finnRb.bodyType = RigidbodyType2D.Static
                    finnMovement.CanNotMove();
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[] {
                        "Guarda guarda",
                        "Per caso gli dei ti hanno mandato per uccidermi?"
                    });
                    break;
                case 1:
                    //parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Dei? Mandato per ucciderti?",
                        "Io sono qui per diventare più forte e salvare il…"
                    });
                    break;
                case 2:
                    //parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[] {
                        "Blah Blah",
                        "Ecco qui un altro scagnozzo di Zeus",
                        "Sappi che non avrò pietà di te"
                    });
                    break;
                //inizia la sfida
                case 3:
                    finnRb.bodyType = RigidbodyType2D.Dynamic;
                    finnMovement.CanMove();
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }

        public void SecondDialogue() {
            GameObject.Find("BossHealthBar").GetComponent<CanvasGroup>().alpha = 0;
            switch (i) {
                case 0:
                    finnRb.bodyType = RigidbodyType2D.Static;
                    finnMovement.CanNotMove();
                    // parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[] {
                        "(affannato e morente) Maledizione….",
                        "(affannato e morente) Finalmente gli Dei hanno avuto quello che volevano"
                    });
                    break;

                case 1:
                    // parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Non sono qui per ucciderti",
                        "Sono qui per la tua arma, e nel caso… del tuo aiuto"
                    });
                    break;

                case 2:
                    // parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[] {
                        "(affannato e morente) Aiuto?",
                        "(affannato e morente) Ragazzo ma ti rendi conto che siamo in un’arena dove si combatte per la vita e la morte?",
                        "Se vorrai la mia arma dovrai uccidermi"
                    });
                    break;

                case 3:
                    // hit scriptato da parte di achille per finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "SCRIPT DA FARE",
                        ":)",
                        "<3"
                    });
                    break;
                case 4:
                    // parla finn e finisce la scena
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "…",
                        "Maledizione, un combattente come lui sarebbe stato d’aiuto",
                        "Poi cosa voleva dire con “Gli Dei ti hanno mandato per uccidermi“"
                    });
                    break;
                case 5:
                    //Finn ottiene la lancia
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "FINN OTTIENE LA LANCIA"
                    });
                    break;
                case 6:
                    // parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "E’ ora di tornare da Efesto"
                    });
                    break;
                case 7:
                    finnRb.bodyType = RigidbodyType2D.Dynamic;
                    finnMovement.CanMove();
                    DialogueSceneTileMap.k = 2;
                    isEnded = true;
                    break;
            }
        }
    }
}
