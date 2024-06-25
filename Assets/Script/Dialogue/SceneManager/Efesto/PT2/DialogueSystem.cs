using UnityEngine;

namespace Script.Dialogue.SceneManager.Efesto.PT2
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTriggerFinn;
        [SerializeField] private DialogueTrigger dialogueTriggerEfesto;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject staminaBar;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private Rigidbody2D playerRb;
        public bool isEnded;
        private int _i;
        private byte _j;

        public void ResetDialogueTrigger()
        {
            dialogueTriggerEfesto.dialogue.isEnded = false;
            dialogueTriggerFinn.dialogue.isEnded = false;
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
                    player.CanNotMove();

                    HandleDialogue(ref _i, ref _j, dialogueTriggerEfesto, new[]
                    {
                        ""
                    });
                    break;
                default:
                    player.CanMove();
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    DialogueSceneTileMap.K = 1;
                    isEnded = true;
                    break;
            }
        }
    }
}