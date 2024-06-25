using UnityEngine;
using UnityEngine.Playables;

namespace Script.Dialogue.SceneManager.Tyr
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTriggerTyr;
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerFreya;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private PlayableDirector timeline;
        [SerializeField] private Animator tyrAnimator;
        public int i;
        public byte j;
        public bool isEnded;

        public void ResetDialogueTrigger()
        {
            dialogueTriggerTyr.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
            dialogueTriggerFreya.dialogue.isEnded = false;
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
                    player.CanNotMove();
                    timeline.enabled = false;
                    timeline.playOnAwake = false;
                    HandleDialogue(ref i, ref j, dialogueTriggerTyr, new[]
                    {
                        "Maledizione, chi osa irrompere nel mio palazzo ?",
                        "Freya? Sei tu?",
                        "E chi sarebbe quel coso li?"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "TYR",
                        "Sono qui per porre fine al tuo regno del terrore"
                    });
                    break;
                case 2:
                    HandleDialogue(ref i, ref j, dialogueTriggerTyr, new[]
                    {
                        "Risata",
                        "E come intendi farlo?",
                        "Con l’aiuto di una traditrice?"
                    });
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Traditrice?"
                    });
                    break;
                case 4:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "..."
                    });
                    break;
                case 5:
                    HandleDialogue(ref i, ref j, dialogueTriggerTyr, new[]
                    {
                        "Devi sapere che Freya mi ha aiutato a sgominare il regno di Odino",
                        "ma a quanto vedo ora ha interessi diversi"
                    });
                    break;
                case 6:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Tyr...",
                        "Hai permesso a ladri e barbari di interrompere la nostra quiete e di mettere al repentaglio la vita della nostra gente",
                        "il tuo regno terminerà qui e ORA"
                    });
                    break;
                //inizia la sfida
                default:
                    player.CanMove();
                    DialogueSceneTileMap.k = 1;
                    isEnded = true;
                    break;
            }
        }

        public void SecondDialogue()
        {
            switch (i)
            {
                case 0:
                    // parla tyr
                    player.CanNotMove();

                    tyrAnimator.SetTrigger("Hurt");
                    HandleDialogue(ref i, ref j, dialogueTriggerTyr, new[]
                    {
                        "(affannato) FERMI",
                        "Soldato"
                    });
                    break;
                case 1:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "..."
                    });
                    break;
                case 2:
                    HandleDialogue(ref i, ref j, dialogueTriggerTyr, new[]
                    {
                        "(affannato) Cosa ti spinge qui? Cosa ti spinge a terminare il mio regno?"
                    });
                    break;
                case 3:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Vedi...",
                        "Il mio villaggio è stato vittima di un attacco da parte di barbari",
                        "Ed esattamente come ad Asgaard, anche a Sant’Elena hanno distrutto tutto, costringendoci a scappare nelle foreste",
                        "Io sono qui per richiedere il tuo aiu..."
                    });
                    break;
                case 4:
                    timeline.enabled = true;
                    timeline.Play();
                    i++;
                    break;
                case 5:
                    if (timeline.state == PlayState.Paused)
                    {
                        tyrAnimator.SetTrigger("Defeat");
                        HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                        {
                            "...",
                            "(sorpreso) Freya ?"
                        });
                    }

                    break;
                case 6:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Scusami, ma non sopportavo più la sua presenza in questo regno"
                    });
                    break;
                case 7:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Ma un suo aiuto ci sarebbe stato comodo"
                    });
                    break;
                case 8:
                    var freya = GameObject.Find("Freya");
                    var newRotation = freya.GetComponent<Transform>().eulerAngles;
                    newRotation.y = 180;
                    freya.GetComponent<Transform>().eulerAngles = newRotation;

                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Era un debole",
                        "Ci avrebbe solo che ostacolato",
                        "Ora sono io la regina di Asgaard, e riporterò questo regno alla sua gloria"
                    });
                    break;
                case 9:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "...",
                        "(nella sua testa) Me lo auguro",
                        "Presto vediamo di prendere qualcosa e andiamo avanti",
                        "Non possiamo perdere tempo"
                    });
                    break;
                case 10:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "(annoiata) Si si, andiamo",
                        "Tieni, prendi questo"
                    });
                    break;
                case 11:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "FINN OTTIENE SCUDO DISTRUTTO DI TYR"
                    });
                    break;
                case 12:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "E’ mal ridotto, forse un fabbro potrà aiutarci a ripararlo"
                    });
                    break;
                case 13:
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Mh...",
                        "Dove possiamo andare? Ho bisogno di un’altra arma...",
                        "La mia spada è quasi andata",
                        "Ma certo, l’antica Grecia è il posto più adatto"
                    });
                    break;
                case 14:
                    HandleDialogue(ref i, ref j, dialogueTriggerFreya, new[]
                    {
                        "Allora? Andiamo?",
                        "Ho molta fretta di sistemare il mio regno"
                    });
                    break;
                default:
                    player.CanMove();
                    DialogueSceneTileMap.k = 2;
                    isEnded = true;
                    break;
            }
        }
    }
}