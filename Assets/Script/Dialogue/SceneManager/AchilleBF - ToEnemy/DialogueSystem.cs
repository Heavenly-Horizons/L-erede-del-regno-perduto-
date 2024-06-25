using UnityEngine;

namespace Script.Dialogue.SceneManager.AchilleBF___ToEnemy
{
    public class DialogueSystem : MonoBehaviour
    {
<<<<<<< HEAD
        [SerializeField] public Achille_Movement achille_Movement;
        public int i;
        public byte j;

        public bool isEnded;

        private DialogueTrigger dialogueTriggerAchille;
        private DialogueTrigger dialogueTriggerFinn;

        private void Awake()
        {
            //inizializza i dialogue trigger achille e finn
            var finn = GameObject.Find("3_knight_");
            var achille = GameObject.Find("Achille");
            dialogueTriggerAchille = achille.GetComponent<DialogueTrigger>();
            dialogueTriggerFinn = finn.GetComponent<DialogueTrigger>();
        }

=======
        [SerializeField] public Achille_Movement achilleMovement;
        [SerializeField] private DialogueTrigger dialogueTriggerAchille;
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private Rigidbody2D finnRb;
        [SerializeField] private PlayerMovement finnMovement;

        public int i;
        public byte j;
        public bool isEnded;

>>>>>>> main
        public void ResetDialogueTrigger()
        {
            dialogueTriggerAchille.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
        }

<<<<<<< HEAD
        private void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences)
=======
        private static void LoadNewDialogue(DialogueTrigger dialogueTrigger, string[] sentences)
>>>>>>> main
        {
            dialogueTrigger.dialogue.sentences = sentences;
        }

<<<<<<< HEAD
        public void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues)
=======
        private void HandleDialogue(ref int i, ref byte j, DialogueTrigger dialogueTrigger, string[] dialogues)
>>>>>>> main
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
<<<<<<< HEAD
=======
                    //finnRb.bodyType = RigidbodyType2D.Static;
                    finnMovement.CanNotMove();
>>>>>>> main
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[]
                    {
                        "Guarda guarda",
                        "Per caso gli dei ti hanno mandato per uccidermi?"
                    });
                    break;
                case 1:
                    //parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Dei? Mandato per ucciderti?",
                        "Io sono qui per diventare più forte e salvare il…"
                    });
                    break;
                case 2:
                    //parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[]
                    {
                        "Blah Blah",
                        "Ecco qui un altro scagnozzo di Zeus",
                        "Sappi che non avrò pietà di te"
                    });
                    break;
                //inizia la sfida
                case 3:
<<<<<<< HEAD
=======
                    finnRb.bodyType = RigidbodyType2D.Dynamic;
                    finnMovement.CanMove();
>>>>>>> main
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
<<<<<<< HEAD
=======
                    finnRb.bodyType = RigidbodyType2D.Static;
                    finnMovement.CanNotMove();
>>>>>>> main
                    // parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[]
                    {
                        "(affannato e morente) Maledizione….",
                        "(affannato e morente) Finalmente gli Dei hanno avuto quello che volevano"
                    });
                    break;

                case 1:
                    // parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "Non sono qui per ucciderti",
                        "Sono qui per la tua arma, e nel caso… del tuo aiuto"
                    });
                    break;

                case 2:
                    // parla achille
                    HandleDialogue(ref i, ref j, dialogueTriggerAchille, new[]
                    {
                        "(affannato e morente) Aiuto?",
                        "(affannato e morente) Ragazzo ma ti rendi conto che siamo in un’arena dove si combatte per la vita e la morte?",
                        "Se vorrai la mia arma dovrai uccidermi"
                    });
                    break;

                case 3:
                    // hit scriptato da parte di achille per finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "SCRIPT DA FARE",
                        ":)",
                        "<3"
                    });
                    break;

                case 4:
                    // parla finn e finisce la scena
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "…",
                        "Maledizione, un combattente come lui sarebbe stato d’aiuto",
                        "Poi cosa voleva dire con “Gli Dei ti hanno mandato per uccidermi“"
                    });
                    break;
                case 5:
                    //Finn ottiene la lancia
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
<<<<<<< HEAD
                        "Finn ottiene la lancia"
=======
                        "FINN OTTIENE LA LANCIA"
>>>>>>> main
                    });
                    break;
                case 6:
                    // parla finn
                    HandleDialogue(ref i, ref j, dialogueTriggerFinn, new[]
                    {
                        "E’ ora di tornare da Efesto"
                    });
                    break;
                case 7:
<<<<<<< HEAD
=======
                    finnRb.bodyType = RigidbodyType2D.Dynamic;
                    finnMovement.CanMove();
>>>>>>> main
                    DialogueSceneTileMap.k = 2;
                    isEnded = true;
                    break;
            }
        }
    }
}