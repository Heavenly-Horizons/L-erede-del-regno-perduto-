using UnityEngine;

<<<<<<< HEAD
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
=======
namespace Script.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        public void TriggerDialogue()
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
        }
>>>>>>> main
    }
}