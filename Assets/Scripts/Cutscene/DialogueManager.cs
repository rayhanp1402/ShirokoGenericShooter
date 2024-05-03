using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueLeftPrefab;
    public GameObject dialogueRightPrefab;
    public GameObject dialogueNarrationPrefab;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public List<Dialogue> dialogues;

    private GameObject currentPrefab;
    private GameObject otherPrefab;

    public Sprite shirokoSprite;
    public Sprite hoshinoSprite;
    public Sprite serikaSprite;

    // Dictionary to map character names to their respective images
    public Dictionary<string, Sprite> characterImages;

    private int currentDialogueIndex = 0;

    void Start()
    {
        sentences = new Queue<string>();

        characterImages = new Dictionary<string, Sprite>();
        characterImages["Shiroko"] = shirokoSprite;
        characterImages["Hoshino"] = hoshinoSprite;
        characterImages["Serika"] = serikaSprite;

        LoadDialogues();
        StartNextDialogue();
    }

    void LoadDialogues()
    {
        // Initialize the dialogues list
        dialogues = new List<Dialogue>();

        // Load dialogues for left and right prefab
        Dialogue dialogue1 = new Dialogue("Shiroko", dialogueLeftPrefab, new string[]
        {
            "What's up fellow kids? It's a great day isn' it?",
            "Today we're about to do what's called a pro gamer move",
            "Let's cut to the chase so we can immediately own these kids"
        });
        dialogues.Add(dialogue1);

        Dialogue dialogue2 = new Dialogue("Hoshino", dialogueRightPrefab, new string[]
        {
            "This is another character speaking.",
            "We can have different dialogues for different characters.",
            "Feel free to add more dialogues as needed."
        });
        dialogues.Add(dialogue2);

        // Add dialogues with "" name as DialogueNarration type
        Dialogue narrationDialogue = new Dialogue("", dialogueNarrationPrefab, new string[]
        {
            "This is a narration dialogue.",
            "It doesn't have a character name associated with it."
        });
        dialogues.Add(narrationDialogue);

        // Add more dialogues as needed
    }

    void StartNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            // Get the current dialogue
            Dialogue currentDialogue = dialogues[currentDialogueIndex];

            // If the name is empty, set the prefab to DialogueNarrationPrefab
            if (currentDialogue.name == "")
            {
                currentPrefab = dialogueNarrationPrefab;
            }
            else
            {
                currentPrefab = currentDialogue.prefab;
            }

            // Hide all other prefabs
            dialogueLeftPrefab.SetActive(false);
            dialogueRightPrefab.SetActive(false);
            dialogueNarrationPrefab.SetActive(false);

            // Activate the current prefab
            currentPrefab.SetActive(true);

            // Start the dialogue
            StartDialogue(currentDialogue);

            // Set the character image based on the character's name
            if (characterImages.ContainsKey(currentDialogue.name))
            {
                Image image = currentPrefab.GetComponentInChildren<Image>();
                if (image != null)
                {
                    image.sprite = characterImages[currentDialogue.name];
                }
            }
        }
        else
        {
            Debug.Log("No more dialogues to start.");
        }
    }


    public void StartDialogue(Dialogue dialogue)
    {
        // Find the nameText and dialogueText in the current prefab hierarchy
        nameText = currentPrefab.transform.Find("CharacterName")?.Find("CharacterText")?.GetComponent<TextMeshProUGUI>();
        dialogueText = currentPrefab.transform.Find("DialogueBox").Find("DialogueText").GetComponent<TextMeshProUGUI>();

        // Set the character name text if it exists
        if (nameText != null)
        {
            nameText.text = dialogue.name;
        }

        // Clear previous sentences and enqueue new sentences
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Display the next sentence
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

        // Display the sentence gradually
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // Clear the dialogue text
        dialogueText.text = "";

        // Display each letter of the sentence gradually
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        currentDialogueIndex++;

        StartNextDialogue();
    }
}
