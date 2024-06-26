using System.Collections;
using UnityEngine;

namespace Script.Dialogue.SceneManager.Efesto
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private GameObject blackPanel;
        [SerializeField] private CanvasGroup blackPanelCanvasGroup;
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerEfesto;
        [SerializeField] private DialogueTrigger dialogueTriggerFreya;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Rigidbody2D playerRb;
        public bool isEnded;
        private int _i;
        private byte _j;

        public void ResetDialogueTrigger()
        {
            dialogueTriggerEfesto.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
            dialogueTriggerFreya.dialogue.isEnded = false;
            _i = 0;
            _j = 0;
        }

        private static void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences)
        {
            dialogueTrigger.dialogue.sentences = sentences;
        }

        private void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues)
        {
            if (j == 0)
            {
                j++;
                LoadNewDialogue(dialogueTrigger, dialogues);
                dialogueTrigger.TriggerDialogue();
                dialogueTrigger.dialogue.isEnded = false;
                isEnded = false;
            }

            if (dialogueTrigger.dialogue.isEnded)
            {
                i++;
                j = 0;
            }
        }

        public void FirstDialogue()
        {
            switch (_i)
            {
                case 0:
                    //disattiva la gravità
                    playerRb.bodyType = RigidbodyType2D.Static;
                    //il player non si  può muovere
                    playerMovement.CanNotMove();

                    //blackPanel 1
                    blackPanel.SetActive(true);
                    blackPanelCanvasGroup.alpha = 1;

                    HandleDialogue(ref _i, ref _j, dialogueTriggerFreya, new[]
                    {
                        "Officina di Efesto",
                        "Credo che lui possa aiutarci"
                    });
                    break;
                case 1:
                    //i due entrano in scena
                    StartCoroutine(FadeOut());
                    blackPanel.SetActive(false);

                    _i++;
                    break;
                case 2:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "(Dorme) ZZZZZ"
                    });
                    break;
                case 3:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFreya, new[]
                    {
                        "COFF COFF"
                    });
                    break;
                case 4:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Chi va la?"
                    });
                    break;
                case 5:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "Stiamo cercando un fabbro"
                    });
                    break;
                case 6:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Un fabbro?",
                        "(Risata)",
                        "Si dia il caso che tu sia entrato nell'officina del miglior fabbro di sempre."
                    });
                    break;
                case 7:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFreya, new[]
                    {
                        "(sottovoce) Che fortuna...",
                        "Presto Finn consegna lo scudo a quel vecchio"
                    });
                    break;
                case 8:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Scusa?"
                    });
                    break;
                case 9:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "FINN CONSEGNA LO SCUDO A EFESTO"
                    });
                    break;
                case 10:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Bene... Messo malino questo scudo",
                        "Ma non impossibile da riparare",
                        "EFESTO SBATTE IL SUO MARTELLO SULLO SCUDO",
                        "Bene, credo che il tuo scudo sia pronto"
                    });
                    break;
                case 11:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "Di già",
                        "FINN OTTIENE LO SCUDO DA EFESTO, E LO EQUIPAGGIA",
                        "Questo mi darà una mano",
                        "Mi scusi",
                        "Avrei bisogno di un'altra arma",
                        "La mia spada è quasi rotta",
                        "Potrebbe darmi una delle sue armi?"
                    });
                    break;
                case 12:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "A cosa ti serve un'arma ragazzo?"
                    });
                    break;
                case 13:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "A salvare il villaggio"
                    });
                    break;
                case 14:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "...",
                        "Bene, si dia il caso che nella città in cui ti trovi c'è un guerriero che sta diventando un po' troppo potente",
                        "Il suo nome è Achille",
                        "Potreste sconfiggerlo e prendervi la sua lancia",
                        "Si narra che quella lancia sia l'arma più forte mai fabbricata"
                    });
                    break;
                case 15:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "E' proprio quello che mi serve"
                    });
                    break;
                case 16:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFreya, new[]
                    {
                        "Vecchio",
                        "Dove possiamo trovare questo guerriero?"
                    });
                    break;
                case 17:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Guarda che non sono poi così veccio...",
                        "Ho solo 150 anni",
                        "Achille si trova non molto distante da qui, si trova nella sua Arena"
                    });
                    break;
                case 18:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "Presto Freya, non possiamo perdere tempo"
                    });
                    break;
                case 19:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Calma ragazzo, non potrai affrontare Achille con alleati in campo",
                        "Lo scontro sarà fatto a primo sangue",
                        "Tu contro lui, e chi perderà... morirà",
                        "E' la legge dei nostri guerrieri, combattono per il loro onore"
                    });
                    break;
                case 20:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "..."
                    });
                    break;
                case 21:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFreya, new[]
                    {
                        "Bene ragazzo, sembra che questa volta dipenderà tutto da te"
                    });
                    break;
                case 22:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "Andiamo, non perdiamo tempo"
                    });
                    break;
                case 23:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        "Ah ragazzo, quando batterai Achille",
                        "Sempre che non sia lui a uccidere te, torna qui e ti clonerò la sua arma",
                        "Non puoi abbandonare il nostro regno con le nostre armi"
                    });
                    break;
                case 24:
                    HandleDialogue(ref _i, ref _j, dialogueTriggerFinn, new[]
                    {
                        "La ringrazio"
                    });
                    break;
                default:
                    playerMovement.CanMove();
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    DialogueSceneTileMap.K++;
                    isEnded = true;
                    break;
            }
        }

        private IEnumerator FadeOut()
        {
            // Calcola quanto decrementare alpha in ogni frame basandosi sulla durata del fade
            var fadeStep = blackPanelCanvasGroup.alpha / 1f * Time.deltaTime;

            // Continua a eseguire finché alpha è maggiore di 0
            while (blackPanelCanvasGroup.alpha > 0)
            {
                // Decrementa il valore di alpha
                blackPanelCanvasGroup.alpha -= fadeStep;

                // Aspetta il prossimo frame
                yield return null;
            }

            // Assicurati che alpha sia esattamente 0 alla fine
            blackPanelCanvasGroup.alpha = 0;
        }
    }
}