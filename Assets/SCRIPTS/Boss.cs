using System.Collections;
using Script.Dialogue.SceneManager.AchilleBF___ToEnemy;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public GameObject dialogueSystemObject;
    private DialogueSystem dialogueSystem;
    public bool isDialogueEnded = false;

    void Start() {
    // Controllo per il componente Rigidbody2D
    Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
    if (rb2d == null) {
        Debug.LogWarning("Componente Rigidbody2D non trovato sul GameObject.");
    } else {
        // Imposta le constraints e isKinematic del Rigidbody2D
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.isKinematic = false;
    }

    // Controllo per il componente DialogueSystem, se presente
    if (dialogueSystemObject != null) {
        dialogueSystem = dialogueSystemObject.GetComponent<DialogueSystem>();
        if (dialogueSystem == null) {
            Debug.LogWarning("Componente DialogueSystem non trovato sul GameObject dialogueSystemObject.");
        }
    }
}

    void Update(){
        if(dialogueSystem != null && dialogueSystem.isEnded){
            isDialogueEnded = true;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void StopMovementCall(){
        StartCoroutine(StopMovement());
    }

    private IEnumerator StopMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
    }
}
