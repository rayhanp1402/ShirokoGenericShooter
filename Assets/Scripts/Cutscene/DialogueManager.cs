using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueLeftPrefab;
    public GameObject dialogueRightPrefab;
    public string dialogueLeftPrefabPath;
    public string dialogueRightPrefabPath;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public List<Dialogue> dialogues;

    private GameObject currentPrefab; // Reference to the current prefab
    private GameObject otherPrefab;   // Reference to the other (inactive) prefab

    private int currentDialogueIndex = 0;

    void Start()
    {
        sentences = new Queue<string>();
        LoadDialogues();
        StartNextDialogue();
    }

    void LoadDialogues()
    {
        // Initialize the dialogues list
        dialogues = new List<Dialogue>();

        // Dialogue 1
        Dialogue dialogue1 = new Dialogue();
        dialogue1.name = "Shiroko";
        dialogue1.sentences = new string[]
        {
            "What's up fellow kids? It's a great day isn' it?",
            "Today we're about to do what's called a pro gamer move",
            "Let's cut to the chase so we can immediately own these kids"
        };
        dialogues.Add(dialogue1);

        // Dialogue 2
        Dialogue dialogue2 = new Dialogue();
        dialogue2.name = "Hoshino";
        dialogue2.sentences = new string[]
        {
            "This is another character speaking.",
            "We can have different dialogues for different characters.",
            "Feel free to add more dialogues as needed."
        };
        dialogues.Add(dialogue2);

        // Add more dialogues as needed
    }

    void StartNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            // Determine which prefab to use based on the dialogue index
            currentPrefab = currentDialogueIndex % 2 == 0 ? dialogueLeftPrefab : dialogueRightPrefab;
            otherPrefab = currentPrefab == dialogueLeftPrefab ? dialogueRightPrefab : dialogueLeftPrefab;

            // Hide the other (inactive) prefab
            otherPrefab.SetActive(false);

            StartDialogue(dialogues[currentDialogueIndex]);
        }
        else
        {
            Debug.Log("No more dialogues to start.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText = currentPrefab.transform.Find("CharacterName").Find("CharacterText").GetComponent<TextMeshProUGUI>();
        dialogueText = currentPrefab.transform.Find("DialogueBox").Find("DialogueText").GetComponent<TextMeshProUGUI>();

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
        // Activate the other (inactive) prefab
        otherPrefab.SetActive(true);

        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Count)
        {
            StartNextDialogue();
        }
        else
        {
            Debug.Log("No more dialogues to start.");
        }
    }

}
