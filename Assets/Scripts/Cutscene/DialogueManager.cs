using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();

        // Create a new dialogue instance with specific data
        Dialogue dialogue = new Dialogue();
        dialogue.name = "Shiroko-chan";
        dialogue.sentences = new string[]
        {
            "What's up fellow kids? It's a great day isn' it?",
            "Today we're about to do what's called a pro gamer move",
            "Let's cut to the chase so we can immediately own these kids"
        };

        // Start the dialogue
        StartDialogue(dialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
