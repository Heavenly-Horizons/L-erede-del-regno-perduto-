using UnityEngine;

namespace Script.Dialogue.SceneManager.PreAsgard
{
    internal class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerFreya;
        [SerializeField] private GameObject player;
        public int i;
        public byte j;
        public bool isEnded;
        private byte k;

        public void ResetDialogueTrigger()
        {
            dialogueTriggerFreya.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
        }

        private void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences)
        {
            dialogueTrigger.dialogue.sentences = sentences;
        }

        public void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues)
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
            switch (i)
            {
                case 0:
                    if (k == 0)
                    {
                        //reset del player
                        player.GetComponent<PlayerMovement>().CanNotMove();
                        player.GetComponent<Animator>().SetBool("Run", false);
                        //posizione del player
                        var newPosition = player.GetComponent<Transform>().position;
                        player.GetComponent<Transform>().position = newPosition;
                        k++;
                    }

                    //inizio dialoghi
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Villaggero",
                        "Ho sentito di qualcuno che si è intrufolato ad Asgard e ed è riuscito a sconfiggere qualche Ladro",
                        "Dovrei ringraziarti, ma devo ricordarti che quelli che hai ucciso sono cittadini di Asgard",
                        "Come ti chiami?"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Mi scusi, ma sono qui per una missione",
                        "Il mio nome è Finn signora"
                    });
                    break;
                case 2:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "...",
                        "Freya, piacere",
                        "Vedendoti meglio non sembri di qui, da dove vieni viaggiatore?"
                    });
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Vengo da un villaggio molto lontano da qui",
                        "Il villaggio di Sant’Elena, che al momento è sotto attacco di barbari e ladri proprio come la tua città"
                    });
                    break;
                case 4:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "E come mai sei qui? Dovresti pensare a salvare il tuo popolo… Non sarai mica un codardo?"
                    });
                    break;
                case 5:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "No, affatto",
                        "Sono qui in missione",
                        "Il mio Re, mi ha spedito alla ricerca di guerrieri valorosi e che possano aiutarmi a liberare il mio villaggio",
                        "Sono qui per cercare Tyr"
                    });
                    break;
                case 6:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "TYR?!?"
                    });
                    break;
                case 7:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "..."
                    });
                    break;
                case 8:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Devi sapere che Tyr ci ha aiuato a liberare Asgaard dalla tirannia di Odino, ma subito dopo ha devastato la nostra splendida città",
                        "L’ha rasa al suolo e ha permesso agli invasori e ladri di entrare e saccheggiare la nostra gente",
                        "Se mai vorrai morire, accomodati pure"
                    });
                    break;
                case 9:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Potrei aiutarvi a liberare Asgaard, sconfiggendo Tyr"
                    });
                    break;
                case 10:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "(sottovoce) Effettivamente potrebbe aiutarmi e liberare il trono...",
                        "(sottovoce) Potrei diventare io la nuova regina di Asgaard",
                        "Va bene, mi aggiungerò alla tua spedizione per liberare Asgaard"
                    });
                    break;
                case 11:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Un momento",
                        "Una volta liberata la tua città, dovrai aiutarmi a liberare il mio villaggio"
                    });
                    break;
                case 12:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "...",
                        "Va bene, accetto",
                        "Ti aiuterò"
                    });
                    break;
                case 13:
                    var freya = GameObject.Find("Freya");
                    var newRotation = freya.GetComponent<Transform>().eulerAngles;
                    newRotation.y = 180;
                    freya.GetComponent<Transform>().eulerAngles = newRotation;
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Da questa parte"
                    });
                    break;
                default:
                    //fine dialoghi
                    player.GetComponent<PlayerMovement>().CanMove();
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }
    }
}