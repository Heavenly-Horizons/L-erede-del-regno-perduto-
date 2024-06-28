using UnityEngine;

namespace Script.Dialogue.SceneManager.KingEncounter {
    public class DialogueSystem : MonoBehaviour {
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerKing;
        [SerializeField] private PlayerMovement player;
        public int i;
        public byte j;
        public bool isEnded;
        private byte k;

        public void ResetDialogueTrigger() {
            dialogueTriggerKing.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
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
                case 0:
                    if (k == 0) {
                        //reset del player
                        player.CanNotMove();
                        player.GetComponent<Animator>().SetBool("Run", false);
                        //posizione del player
                        Vector3 newPosition = player.GetComponent<Transform>().position;
                        player.GetComponent<Transform>().position = newPosition;
                        k++;
                    }

                    //inizio dialoghi
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "Finn, ti aspettavo",
                        "Il villaggio è andato",
                        "Con l’ultima sconfitta, siamo stati costretti ad accamparci fuori dalle nostre stesse mura.",
                        "Tutto ciò è straziante e non fa bene alla nostra gente",
                        "Ho bisogno di te",
                        "Dovrai partire in viaggio particolare, dovrai affrontare diverse mitologie"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Mitologie?"
                    });
                    break;
                case 2:
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "Esatto",
                        "Attraverso le radici dell’albero della vita, potrai affrontare avversari valorosi per poter diventare più forte",
                        "Tieni, questa è l’unica che siamo riusciti a salvare dopo l’attacco al castello"
                    });
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "FINN OTTIENE “Radice dell’albero della vita”"
                    });
                    break;
                case 4:
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "Basta tenerla in mano e pensare a qualche avversario che vorresti affrontare",
                        "Ti trasporterà direttamente nella sua era"
                    });
                    break;
                case 5:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Ma… perché io? Posso ancora dare una mano qui",
                        "Magari posso aiutare l’esercito a respingere l’invasore"
                    });
                    break;
                case 6:
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "Finn… sei l’unico guerriero di cui mi fido",
                        "L’unico a cui cederei il mio regno"
                    });
                    break;
                case 7:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "…",
                        "La ringrazio"
                    });
                    break;
                case 8:
                    HandleDialogue(ref i, ref j, dialogueTriggerKing, new[] {
                        "Presto, non possiamo perdere ulteriore tempo"
                    });
                    break;
                case 9:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[] {
                        "Bene, devo solo pensare a un guerriero?",
                        "Vediamo…",
                        "Ma certo… Tyr, il dio della guerra"
                    });
                    break;
                default:
                    //fine dialoghi
                    player.CanMove();
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }
    }
}