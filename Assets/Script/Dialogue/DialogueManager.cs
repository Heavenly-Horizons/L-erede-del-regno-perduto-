using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private const string kAlphaColorCode = "<color=#00000000>";
    private const float kMaxtTextTime = 0.1f;
    private const int kTimeSpeed = 2;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private CanvasGroup group;
    private Dialogue dialogue;
    public Queue<string> sentences;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        group.alpha = 1;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (var sentence in dialogue.sentences) sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        var originalText = sentence;
        var displayedText = "";
        var alphaCount = 0;

        foreach (var letter in sentence)
        {
            alphaCount++;
            dialogueText.text = originalText;
            displayedText = dialogueText.text.Insert(alphaCount, kAlphaColorCode);
            dialogueText.text = displayedText;
            yield return new WaitForSecondsRealtime(kMaxtTextTime / kTimeSpeed);
        }

        yield return null;
    }

    public void EndDialogue()
    {
        dialogue.isEnded = true;
        group.alpha = 0;
    }
}